using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Unity.VisualScripting;
using ChainSafe.Gaming.Web3;
using TMPro;

public class badgeUIManager : MonoBehaviour
{
    [SerializeField] Sprite lockSprite;
    [SerializeField] Sprite unlockSprite;
    [SerializeField] Sprite lodingSprite;

    [SerializeField] RectTransform UIMain;
    [SerializeField] Button[] mintButtons;
    [SerializeField] Image[] mintBadgeImages;
    [SerializeField] Image[] mintBadgeLoadingIndicators;
    [SerializeField] Button withdrawEtherButton;
    [SerializeField] Button realMintButton;
    [SerializeField] TMP_Text walletAddress;
    [SerializeField] TMP_Text etherBalance;

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
        Web3singleton.Instance.onSetImage += BadgeImageSetting;
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
        Web3singleton.Instance.onUnlock -= MintUnlock;
        Web3singleton.Instance.onLock -= MintLock;
        Web3singleton.Instance.onAlready -= AlreadyMint;
        Web3singleton.Instance.onSetImage -= BadgeImageSetting;
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
        UIMain.anchoredPosition = Vector2.zero;
    }
    public void UIClose()
    {
        UIMain.anchoredPosition = new Vector2(2000, 0);
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
        mintBadgeLoadingIndicators[_index].gameObject.SetActive(true);
    }

    public void Init()
    {
        for(int i=0;i<mintButtons.Length;i++)
        {
            MintLock(i);
            mintBadgeLoadingIndicators[i].gameObject.SetActive(false);
        }
        connectButton.gameObject.SetActive(true);
        disconnectButton.gameObject.SetActive(false);
        withdrawEtherButton.gameObject.SetActive(false);
        realMintButton.gameObject.SetActive(false);
    }
    public void BadgeImageSetting(Sprite _sprite, int _index)
    {
        mintBadgeLoadingIndicators[_index].gameObject.SetActive(false);
        mintBadgeImages[_index].sprite = _sprite;
    }
    public void Disconnect()
    {
        PlayerPrefs.DeleteKey("Address");
        connectButton.gameObject.SetActive(true);
        disconnectButton.gameObject.SetActive(false);
        walletAddress.text = "";
        etherBalance.text = "";
        Init();
    }
    public void MintAllBadge()
    {
        Web3singleton.Instance.SendMintAllBadge();
        onClickMintAllBadge();
    }
    private async void onClickMintAllBadge()
    {
        realMintButton.onClick.RemoveAllListeners();
        await Task.Delay(10000);
        realMintButton.onClick.AddListener(MintAllBadge);
    }
    public void WithdrawEther()
    {
        Web3singleton.Instance.SendWithdrawEther();
        OnClickWithdrawEther();
    }
    private async void OnClickWithdrawEther()
    {
        withdrawEtherButton.onClick.RemoveAllListeners();
        await Task.Delay(10000);
        withdrawEtherButton.onClick.AddListener(WithdrawEther);
    }
    public void TransferBadge(int _index)
    {
        Web3singleton.Instance.SendTransferBadge(_index);
        OnClickTransferBadge(_index);
    }
    private async void OnClickTransferBadge(int _index)
    {
        mintButtons[_index].onClick.RemoveAllListeners();
        await Task.Delay(10000);
        mintButtons[_index].onClick.AddListener(() => TransferBadge(_index + 1));
    }
}
