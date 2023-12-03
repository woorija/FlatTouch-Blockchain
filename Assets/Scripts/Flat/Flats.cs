using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flats : MonoBehaviour
{
    public Flat[] flat;
    [SerializeField] FatternManager fatternManager;
    private void Start()
    {
        SetFatternManager();
    }
    void SetFatternManager()
    {
        for(int i = 0; i < flat.Length; i++)
        {
            flat[i].SetFatternManager(fatternManager);
        }
    }
    public void PlayAnimation()
    {
        for(int i = 0; i < flat.Length; i++)
        {
            flat[i].PlayChangeAnimation();
        }
    }

    public void ChangeAllColor(Color _color)
    {
        for (int i = 0; i < flat.Length; i++)
        {
            flat[i].ColorChange(_color);
        }
    }
}
