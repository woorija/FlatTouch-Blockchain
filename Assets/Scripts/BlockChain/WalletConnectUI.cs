using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WalletConnectSharp.Sign.Models.Engine;
using ZXing.QrCode;
using ZXing;

public class WalletConnectUI : MonoBehaviour
{
    [SerializeField] private Image _qrCodeImage;
    [SerializeField] private Button _backButton;

    [SerializeField] private Transform _container;
    [SerializeField] GameObject UITouchLock;

    private void Start()
    {
        _backButton.onClick.AddListener(Disable);
    }

    public void WalletConnected(ConnectedData data)
    {
        // enable display
        _container.gameObject.SetActive(true);

        string uri = data.Uri;

        GenerateQrCode(uri);
    }


    private static Color32[] Encode(string textForEncoding, int width, int height)
    {
        var writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions { Height = height, Width = width }
        };
        return writer.Write(textForEncoding);
    }

    private void GenerateQrCode(string text)
    {
        var encoded = new Texture2D(256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();

        // Convert the texture into a sprite and assign it to our QR code image
        var qrCodeSprite = Sprite.Create(encoded, new Rect(0, 0, encoded.width, encoded.height),
            new Vector2(0.5f, 0.5f), 100f);
        _qrCodeImage.sprite = qrCodeSprite;
    }

    public void Disable()
    {
        _container.gameObject.SetActive(false);
        UITouchLock.SetActive(false);
    }
}
