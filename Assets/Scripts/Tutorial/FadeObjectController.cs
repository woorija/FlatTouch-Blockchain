using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FadeObjectController : MonoBehaviour
{
    [SerializeField] GameObject[] FadeObjects;
    /*    FadeObject 인덱스
     *    0:스코어
     *    1:현재패턴,다음패턴
     *    2:타이밍바 풀
     *    3:타이밍바 없
     *    4:판정구슬
     *    5:노말패턴 답안
     *    6:RGB패턴 답안
     *    7:무지개패턴 답안
     *    8:단일색상패턴 답안
     *    9:역무지개패턴 답안
     */
    void SetActiveTrue(int _index)
    {
        FadeObjects[_index].SetActive(true);
    }
    void SetActiveFalse(int _index)
    {
        FadeObjects[_index].SetActive(false);
    }

    public void SetActiveObject(int _index)
    {
        switch (_index)
        {
            case 1:
                SetActiveTrue(0);
                break;
            case 2:
                SetActiveFalse(0);
                SetActiveTrue(1);
                break;
            case 3:
                SetActiveFalse(1);
                break;
            case 5:
            case 10:
            case 16:
            case 22:
            case 28:
                SetActiveTrue(1);
                break;
            case 6:
                SetActiveFalse(1);
                SetActiveTrue(5);
                break;
            case 7:
                SetActiveTrue(2);
                break;
            case 8:
                SetActiveFalse(5);
                SetActiveFalse(2);
                break;
            case 11:
                SetActiveFalse(1);
                SetActiveTrue(4);
                SetActiveTrue(6);
                break;
            case 12:
                SetActiveTrue(3);
                SetActiveFalse(4);
                SetActiveFalse(6);
                break;
            case 13:
            case 19:
                SetActiveFalse(3);
                break;
            case 17:
                SetActiveFalse(1);
                SetActiveTrue(7);
                break;
            case 18:
                SetActiveFalse(7);
                SetActiveTrue(3);
                break;
            case 23:
                SetActiveFalse(1);
                SetActiveTrue(8);
                break;
            case 24:
                SetActiveFalse(8);
                SetActiveTrue(2);
                break;
            case 25:
                SetActiveFalse(2);
                break;
            case 29:
                SetActiveFalse(1);
                SetActiveTrue(9);
                break;
            case 30:
                SetActiveFalse(9);
                break;
            default:
                break;
        }
    }
}
