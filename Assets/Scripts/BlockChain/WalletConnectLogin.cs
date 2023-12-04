using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChainSafe.Gaming.Unity;
using ChainSafe.Gaming.UnityPackage;
using ChainSafe.Gaming.WalletConnect;
using ChainSafe.Gaming.WalletConnect.Models;
using ChainSafe.Gaming.Web3.Build;
using Newtonsoft.Json;
using Scenes;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngine.UI;
using WalletConnectSharp.Core;
using WalletConnectSharp.Core.Controllers;
using WalletConnectSharp.Events;
using WalletConnectSharp.Events.Model;
using WalletConnectSharp.Sign.Models;
using WalletConnectSharp.Sign.Models.Engine;
using WalletConnectSharp.Sign.Models.Engine.Methods;

public class WalletConnectLogin : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] public Button loginButton;
    [SerializeField] GameObject DisconnectButton;
    [SerializeField] GameObject UITouchLock;

    [Header("Wallet Connect")]
    [SerializeField]
    private string projectId;

    [SerializeField] public string projectName;

    [SerializeField] public string baseContext;

    [SerializeField] private Metadata metadata;

    [SerializeField] private WalletConnectUI walletConnectModal;

    // user isn't required to select wallet to redirect to wallet
    // this is true for android platform since it natively supports WC protocol

    private WalletConnectConfig walletConnectConfig;
    private Dictionary<string, WalletConnectWalletModel> supportedWallets;

    private void OnDestroy()
    {
        if (walletConnectConfig != null)
        {
            walletConnectConfig.OnConnected -= WalletConnected;

            walletConnectConfig.OnSessionApproved -= SessionApproved;
        }
    }
    private IEnumerator Start()
    {
        yield return Initialize();
    }

    protected IEnumerator Initialize()
    {

#if UNITY_ANDROID

        if (!Application.isEditor)
        {
            // user doesn't need to select wallet before login for redirection since Android supports the WC protocol
            isRedirectionWalletAgnostic = true;
        }

#endif
        yield return FetchSupportedWallets();
        loginButton.onClick.AddListener(LoginClicked);
    }

    private async void LoginClicked()
    {
        UITouchLock.SetActive(true);
        await TryLogin();
    }

    protected async Task TryLogin()
    {

        try
        {
            Web3Builder web3Builder = new Web3Builder(ProjectConfigUtilities.Load())
                .Configure(Web3singleton.Instance.ConfigureCommonServices);

            web3Builder = ConfigureWeb3Services(web3Builder);

            Web3singleton.Instance.GlobalWeb3 = await web3Builder.LaunchAsync();
        }

        catch (Exception)
        {
            Debug.Log("Login failed, please try again\n(see console for more details)");
            throw;
        }

        Web3singleton.Instance.erc1155Contract = Web3singleton.Instance.GlobalWeb3.ContractBuilder.Build("ERC1155");

        var address = await Web3singleton.Instance.GlobalWeb3.Signer.GetAddress();
        PlayerPrefs.SetString("Address", address);
        loginButton.gameObject.SetActive(false);
        DisconnectButton.SetActive(true);
        Web3singleton.Instance.onIsOwner.Invoke();
        Web3singleton.Instance.GetBadgeSprites();
        Web3singleton.Instance.BadgeCheck();
        walletConnectModal.Disable();
    }

    protected Web3Builder ConfigureWeb3Services(Web3Builder web3Builder)
    {
        return web3Builder.Configure(services =>
        {
            // Build config to use.
            BuildWalletConnectConfig();

            // Use wallet connect providers
            services.UseWalletConnect(walletConnectConfig)
                .UseWalletConnectSigner()
                .UseWalletConnectTransactionExecutor();
        });
    }

    private void BuildWalletConnectConfig()
    {
        // build chain
        var projectConfig = ProjectConfigUtilities.Load();

        ChainModel chain = new ChainModel(ChainModel.EvmNamespace, projectConfig.ChainId, projectConfig.Network);

        WalletConnectWalletModel defaultWallet = supportedWallets.Values.ToArray()[0];

        walletConnectConfig = new WalletConnectConfig
        {
            ProjectId = projectId,
            ProjectName = projectName,
            BaseContext = baseContext,

            Chain = chain,
            Metadata = metadata,
            // try and get saved value
            SavedSessionTopic = walletConnectConfig?.SavedSessionTopic,
            SupportedWallets = supportedWallets,
            // save file closer to assets when in editor, more accessible
            StoragePath = Application.isEditor ? Application.dataPath : Application.persistentDataPath,
            RedirectToWallet = false,
            KeepSessionAlive = false,
            DefaultWallet = defaultWallet,
        };

        //subscribe to WC events
        walletConnectConfig.OnConnected += WalletConnected;

        walletConnectConfig.OnSessionApproved += SessionApproved;
    }
    private void WalletConnected(ConnectedData data)
    {
        // already redirecting to wallet
        if (walletConnectConfig.RedirectToWallet)
        {
            return;
        }

        // might be null in case of auto login
        if (!string.IsNullOrEmpty(data.Uri))
        {
            // display QR and copy to clipboard
            walletConnectModal.WalletConnected(data);
        }
    }

    private void SessionApproved(SessionStruct session)
    {
        // save/persist session
        if (walletConnectConfig.KeepSessionAlive)
        {
            walletConnectConfig.SavedSessionTopic = session.Topic;

            PlayerData.Instance.WalletConnectConfig = walletConnectConfig;

            PlayerData.Save();
        }

        else
        {
            // reset if any saved config
            PlayerData.Instance.WalletConnectConfig = null;

            PlayerData.Save();
        }

        Debug.Log($"{session.Topic} Approved");
    }
    private IEnumerator FetchSupportedWallets()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://registry.walletconnect.org/data/wallets.json"))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error Getting Supported Wallets: " + webRequest.error);

                yield return null;
            }

            else
            {
                var json = webRequest.downloadHandler.text;

                supportedWallets = JsonConvert.DeserializeObject<Dictionary<string, WalletConnectWalletModel>>(json);

                // make sure supported wallet is also supported on platform
                supportedWallets = supportedWallets
                    .Where(w => w.Value.IsAvailableForPlatform(UnityOperatingSystemMediator.GetCurrentPlatform()))
                    .ToDictionary(p => p.Key, p => p.Value);

                Debug.Log($"Fetched {supportedWallets.Count} Supported Wallets.");
            }
        }
    }
}
