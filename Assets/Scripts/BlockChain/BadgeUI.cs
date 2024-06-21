using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BadgeUI : MonoBehaviour
{
    [SerializeField] GameObject touchLockUI;
    [SerializeField] Sprite lockSprite, unlockSprite, loadingSprite;

    [SerializeField] RectTransform UIMain;

    [SerializeField] GameObject connectButton, disconnectButton;

    [SerializeField] Button[] mintButtons;
    [SerializeField] Image[] badgeImages;
    [SerializeField] Image[] mintLoadingIndicators;

    [SerializeField] TMP_Text walletAddressText, etherBalanceText;

    [SerializeField] GameObject withdrawEtherButton, developerMintButton;
    private void Awake()
    {
        Init();
    }
    private void Start()
    {
    }
    void Init()
    {
        connectButton.SetActive(true);
        disconnectButton.SetActive(false);
        withdrawEtherButton.SetActive(false);
        developerMintButton.SetActive(false);
        walletAddressText.text = string.Empty;
        etherBalanceText.text = string.Empty;
        for (int i = 0; i < 7; i++)
        {
            MintLock(i);
        }
    }
    public void SetWalletAddressText(string _address)
    {
        walletAddressText.text = $"Wallet: {_address.Substring(0, 6)}...{_address.Substring(_address.Length - 4)}";
    }
    public void SetEtherBalanceText(string _balance)
    {
        etherBalanceText.text = $"Ether: {_balance}";
    }
    public void Disconnect()
    {
        walletAddressText.text = string.Empty;
        etherBalanceText.text = string.Empty;
        connectButton.SetActive(true);
        disconnectButton.SetActive(false);
        for(int i = 0; i< 7; i++)
        {
            MintLock(i);
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
    public void OwnerButtonsToggle(bool _toggle)
    {
        withdrawEtherButton.SetActive(_toggle);
        developerMintButton.SetActive(_toggle);
    }
    public void ConnectButtonToggle(bool _toggle)
    {
        connectButton.SetActive(_toggle);
    }
    public void DisconnectButtonToggle(bool _toggle)
    {
        disconnectButton.SetActive(_toggle);
    }
    public void TouchLockUIToggle(bool _toggle)
    {
        touchLockUI.SetActive(_toggle);
    }
    public void MintLock(int _index)
    {
        badgeImages[_index].sprite = lockSprite;
        mintButtons[_index].gameObject.SetActive(false);
        mintLoadingIndicators[_index].gameObject.SetActive(false);
    }
    public void MintUnlock(int _index)
    {
        badgeImages[_index].sprite = unlockSprite;
        mintButtons[_index].gameObject.SetActive(true);
        mintLoadingIndicators[_index].gameObject.SetActive(false);
    }
    public void AlreadyMint(int _index)
    {
        mintButtons[_index].gameObject.SetActive(false);
        mintLoadingIndicators[_index].gameObject.SetActive(true);
        badgeImages[_index].sprite = loadingSprite;
    }
    public void SetBadgeImage(Sprite _sprite, int _index)
    {
        badgeImages[_index].sprite = _sprite;
        mintButtons[_index].gameObject.SetActive(false);
        mintLoadingIndicators[_index].gameObject.SetActive(false);
    }
    public async void TouckLock()
    {
        touchLockUI.SetActive(true);
        await Task.Delay(10000);
        touchLockUI.SetActive(false);
    }
}
