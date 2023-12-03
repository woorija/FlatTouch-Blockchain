using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionObject : MonoBehaviour
{
    [SerializeField] Image DecisionImage;
    public void ColorChange(Color _color)
    {
        DecisionImage.color = _color;
    }
}
