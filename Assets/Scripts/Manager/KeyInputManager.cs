using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputManager : MonoBehaviour
{
    [SerializeField] Flats flats;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Q))
        {
            flats.flat[0].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flats.flat[0].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.W))
        {
            flats.flat[1].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flats.flat[1].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.E))
        {
            flats.flat[2].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flats.flat[2].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.A))
        {
            flats.flat[3].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flats.flat[3].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.S))
        {
            flats.flat[4].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flats.flat[4].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.D))
        {
            flats.flat[5].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flats.flat[5].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Z))
        {
            flats.flat[6].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flats.flat[6].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.X))
        {
            flats.flat[7].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flats.flat[7].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.C))
        {
            flats.flat[8].FlatTouch();
            ClickEffect.Instance.Play_ClickEffect(flats.flat[8].transform.position);
        }
    }
}