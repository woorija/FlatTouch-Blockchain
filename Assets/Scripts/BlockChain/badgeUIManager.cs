using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Unity.VisualScripting;
using ChainSafe.Gaming.Web3;

public class badgeUIManager : MonoBehaviour
{
    [SerializeField] Sprite lockSprite;
    [SerializeField] Sprite unlockSprite;

    [SerializeField] GameObject UIMain;
    [SerializeField] Button[] mintButtons;
    [SerializeField] Image[] mintBadgeImages;
    [SerializeField] Button withdrawEtherButton;
    [SerializeField] Button realMintButton;

    [field: SerializeField] public Button connectButton { get; private set; }
    [field: SerializeField] public Button disconnectButton { get; private set; }
    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("Address"))
        {
            connectButton.gameObject.SetActive(false);
            disconnectButton.gameObject.SetActive(true);
        }
        else
        {
            connectButton.gameObject.SetActive(true);
            disconnectButton.gameObject.SetActive(false);
        }
        UIClose();
        Web3singleton.Instance.onIsOwner += IsOwner;
        Web3singleton.Instance.onUnlock += MintUnlock;
        Web3singleton.Instance.onLock += MintLock;
        Web3singleton.Instance.onAlready += AlreadyMint;
        for(int i=0;i<mintButtons.Length;i++)
        {
            int index = i + 1;
            mintButtons[i].onClick.AddListener(() => TransferBadge(index));
        }
        withdrawEtherButton.onClick.AddListener(WithdrawEther);
        realMintButton.onClick.AddListener(MintAllBadge);
    }
    private void OnDestroy()
    {
        Web3singleton.Instance.onIsOwner -= IsOwner;
    }
    public void IsOwner()
    {
        CallIsOwner();
    }
    public async void CallIsOwner()
    {
        var owner = await Web3singleton.Instance.GetOwner();
        var address = PlayerPrefs.GetString("Address");
        bool isOwner = owner == address;
        if (isOwner)
        {
            withdrawEtherButton.gameObject.SetActive(true);
            realMintButton.gameObject.SetActive(true);
        }
        else
        {
            withdrawEtherButton.gameObject.SetActive(false);
            realMintButton.gameObject.SetActive(false);
        }
    }
    public void UIOpen()
    {
        UIMain.SetActive(true);
    }
    public void UIClose()
    {
        UIMain.SetActive(false);
    }
    public void MintLock(int _index)
    {
        mintBadgeImages[_index].sprite = lockSprite;
        mintButtons[_index].gameObject.SetActive(false);
    }
    public void MintUnlock(int _index)
    {
        mintBadgeImages[_index].sprite = unlockSprite;
        mintButtons[_index].gameObject.SetActive(true);
    }
    public void AlreadyMint(int _index)
    {
        mintButtons[_index].gameObject.SetActive(false);
    }

    public void Init()
    {
        for(int i=0;i<mintButtons.Length;i++)
        {
            MintLock(i);
        }
        connectButton.gameObject.SetActive(true);
        disconnectButton.gameObject.SetActive(false);
        withdrawEtherButton.gameObject.SetActive(false);
        realMintButton.gameObject.SetActive(false);
    }
    public void BadgeImageSetting(Sprite _sprite, int _index)
    {
        mintBadgeImages[_index].sprite = _sprite;
    }
    public void Disconnect()
    {
        PlayerPrefs.DeleteKey("Address");
        connectButton.gameObject.SetActive(true);
        disconnectButton.gameObject.SetActive(false);
        Init();
    }
    public void MintAllBadge()
    {
        try
        {
            Web3singleton.Instance.SendMintAllBadge();
            realMintButton.onClick.RemoveAllListeners();
        }
        catch(Web3Exception e)
        {
            Debug.LogError(e.Message);
            realMintButton.onClick.AddListener(MintAllBadge);
        }
    }
    public void WithdrawEther()
    {
        try
        {
            Web3singleton.Instance.SendWithdrawEther();
            withdrawEtherButton.onClick.RemoveAllListeners();
        }
        catch (Web3Exception e)
        {
            Debug.LogError(e.Message);
            withdrawEtherButton.onClick.AddListener(WithdrawEther);
        }
    }
    public void TransferBadge(int _index)
    {
        try
        {
            Web3singleton.Instance.SendTransferBadge(_index);
            mintButtons[_index].onClick.RemoveAllListeners();
        }
        catch (Web3Exception e)
        {
            Debug.LogError(e.Message);
            mintButtons[_index].onClick.AddListener(() => TransferBadge(_index + 1));
        }
    }

}
