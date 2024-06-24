using Contracts;
using Dynamitey.DynamicObjects;
using evm.net;
using evm.net.Factory;
using evm.net.Models;
using MetaMask;
using MetaMask.Contracts;
using MetaMask.Models;
using MetaMask.Unity;
using System;
using System.Globalization;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Web3Manager : MonoBehaviour
{
    [SerializeField] BadgeUI badgeUI;

    MetaMaskWallet wallet;
    CustomContract customContract;
    static string contractAddress = "0x0c4f74549Ecf0564b01a6Ab9EcF43591bD12B731";

    [SerializeField] RawImage tempImage;
    Sprite[] badgeSprites;
    bool[] hasBadge;
    int badgeCount = 0;
    private void Awake()
    {
        MetaMaskUnity.Instance.Initialize();
        wallet = MetaMaskUnity.Instance.Wallet;
        hasBadge = new bool[7];
    }
    private async void Start()
    {
        await MetaMaskHelpers.SwitchEthereumChain(wallet, MetaMask.Models.ChainId.SepoliaTestnet);
        Contract.ContractFactory = new BackedTypeContractFactory();
        customContract = Contract.Attach<CustomContract>(wallet, contractAddress);
        wallet.WalletConnected += OnWalletConnected;
        wallet.WalletDisconnected += OnWalletDisconnected;
        badgeSprites = new Sprite[7];
        for(int i = 0; i < 7; i++)
        {
            badgeUI.MintLock(i);
        }
        UIUpdate();
    }
    private void OnDestroy()
    {
        if (wallet != null)
        {
            wallet.WalletConnected -= OnWalletConnected;
            wallet.WalletDisconnected -= OnWalletDisconnected;
        }
    }
    async void OnWalletConnected(object sender, EventArgs e)
    {
        badgeUI.SetWalletAddressText(wallet.SelectedAddress);
        string balance = await GetBalanceEther();
        badgeUI.SetEtherBalanceText(balance);
        badgeUI.ConnectButtonToggle(false);
        badgeUI.DisconnectButtonToggle(true);
        IsOwner();
        Debug.Log("batch start");
        await GetBalanceOfBatch();
        GameManager.Instance.LoadToBlockChain(badgeCount);
        Debug.Log("GetSprite start");
        await GetBadgeSprites();
        Debug.Log("SetSprite start");
        await SetBadgeImages();
        badgeUI.TouchLockUIToggle(false);
    }
    void OnWalletDisconnected(object sender, EventArgs e)
    {
        for(int i=0;i<hasBadge.Length;i++)
        {
            hasBadge[i] = false;
        }
        badgeCount = 0;
        badgeUI.Disconnect();
    }
    void IsOwner()
    {
        string ownerAddress = "0xdEa2885a4d87EeBac7e90965C4BA02d55288C56B"; //await customContract.Owner();
        bool isOwner = ownerAddress == wallet.SelectedAddress;
        badgeUI.OwnerButtonsToggle(isOwner);
    }
    async Task GetBadgeSprites()
    {
        string uri = await customContract.Uri(0);
        uri = uri.Substring(0, uri.ToString().LastIndexOf("{id}.json"));
        for (int i = 0; i < badgeSprites.Length; i++)
        {
            if (badgeSprites[i] == null)
            {
                Texture2D tempTexture = await ImportBadgeTexture(uri, (i + 1).ToString());
                if(tempTexture == null)
                {
                    i--;
                    continue;
                }
                tempImage.texture = tempTexture;

                badgeSprites[i] = Sprite.Create((Texture2D)tempImage.texture, new Rect(0, 0, tempImage.texture.width, tempImage.texture.height), new UnityEngine.Vector2(0.5f, 0.5f));
            }
        }
    }
    public void OnConnect()
    {
        badgeUI.TouckLock();
        MetaMaskUnity.Instance.Connect();
    }
    public void OnDisconnect()
    {
        MetaMaskUnity.Instance.Disconnect();
    }
    public async Task SetBadgeImages()
    {
        for(int i = 0; i < hasBadge.Length; i++)
        {
            if (hasBadge[i])
            {
                if (badgeSprites[i] == null)
                {
                    await Task.Delay(1000);
                    i--;
                    continue;
                }
                else
                {
                    badgeUI.SetBadgeImage(badgeSprites[i], i);
                }
            }
        }
    }
    public async void UIUpdate()
    {
        while (true)
        {
            for (int i = 0; i < hasBadge.Length; i++)
            {
                if (!badgeUI.IsEqualBadgeImage(i, badgeSprites[i]))
                {
                    if (i <= GameManager.Instance.stageCleared && !hasBadge[i])
                    {
                        await GetBalanceOf(i);
                    }
                }
                if (hasBadge[i])
                {
                    if (badgeSprites[i] == null)
                    {
                        badgeUI.AlreadyMint(i);
                    }
                    else
                    {
                        badgeUI.SetBadgeImage(badgeSprites[i], i);
                    }
                }
            }
            string balance = await GetBalanceEther();
            badgeUI.SetEtherBalanceText(balance);
            await Task.Delay(10000);
        }
    }
    #region Contract
    public async Task<BigInteger> GetBalance(IProvider provider, string address, string block)
    {
        string text = await provider.Request<string>("eth_getBalance", new object[2] { address, block });
        if (text.StartsWith("0x"))
        {
            text = text.Substring(2);
        }
        return BigInteger.Parse("0" + text, NumberStyles.HexNumber);
    }
    public async Task<string> GetBalanceEther()
    {
        BigInteger balance = await GetBalance(wallet, wallet.SelectedAddress, "latest");
        decimal ether = (decimal)balance / (decimal)BigInteger.Pow(10, 18);
        string etherText;
        if (ether < 10)
        {
            etherText = ether.ToString("F3");
        }
        else if (ether < 100)
        {
            etherText = ether.ToString("F2");
        }
        else if (ether < 1000)
        {
            etherText = ether.ToString("F1");
        }
        else
        {
            etherText = ether.ToString("F0");
        }
        return etherText;
    }
    public async void SendMintAllBadge()
    {
        badgeUI.TouckLock();
        await customContract.MintERC1155();
    }
    public async void SendWithdrawEther()
    {
        badgeUI.TouckLock();
        await customContract.WithdrawEther();;
    }
    public async void SendTransferBadge(int _index)
    {
        CallOptions callOptions = default;
        callOptions.Value = "0x2386F26FC10000"; // 0.01ether
        callOptions.Gas = "21402";
        try
        {
            badgeUI.TouckLock();
            await customContract.TransferBadge((ushort)_index, callOptions);
            string balance = await GetBalanceEther();
            badgeUI.SetEtherBalanceText(balance);
            badgeUI.SetBadgeImage(badgeSprites[_index], _index);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    async Task GetBalanceOf(int _index)
    {
        string account = wallet.SelectedAddress;
        var balance = await customContract.BalanceOf(account, _index + 1);
        if(balance != 0)
        {
            hasBadge[_index] = true;
        }
    }
    async Task GetBalanceOfBatch()
    {
        string account = wallet.SelectedAddress;
        BigInteger[] batchBalances = new BigInteger[7];
        for (int i = 0; i < batchBalances.Length; i++)
        {
            batchBalances[i] = await customContract.BalanceOf(account, i + 1);
            if (batchBalances[i] != 0)
            {
                badgeCount = i + 1;
                hasBadge[i] = true;
                if (badgeSprites[i] != null)
                {
                    badgeUI.SetBadgeImage(badgeSprites[i], i);
                }
                else
                {
                    badgeUI.AlreadyMint(i);
                }
            }
        }
        for(int i = 0;i < GameManager.Instance.stageCleared; i++)
        {
            if (batchBalances[i] == 0)
            {
                badgeUI.MintUnlock(i);
            }
        }
    }
    public async Task<Texture2D> ImportBadgeTexture(string _uri, string tokenId)
    {
        string uri = $"{_uri}{tokenId}.json";

        UnityWebRequest webRequest = UnityWebRequest.Get(uri);

        await webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error fetching JSON: {webRequest.error}");
            return null;
        }

        // Deserialize the data into the response class
        Response data = JsonUtility.FromJson<Response>(Encoding.UTF8.GetString(webRequest.downloadHandler.data));
        // Parse JSON to get image URI
        string imageUri = data.image;
        Debug.Log($"Image URI: {imageUri}");

        if (imageUri.StartsWith("ipfs://"))
        {
            imageUri = imageUri.Replace("ipfs://", "https://ipfs.io/ipfs/");
        }
        Debug.Log($"Revised URI: {imageUri}");

        // Fetch image and display in game
        UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(imageUri);

        await textureRequest.SendWebRequest();

        if (textureRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"Error fetching image: {textureRequest.error}");
            return null;
        }

        return DownloadHandlerTexture.GetContent(textureRequest);
    }

    public class Response
    {
        public string image;
    }
    #endregion
}
