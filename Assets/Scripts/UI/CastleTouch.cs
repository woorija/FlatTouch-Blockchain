using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleTouch : MonoBehaviour
{
    [SerializeField] EndingManager endingmanager;
    private void OnMouseDown()
    {
        endingmanager.SceneChange();
    }
}
