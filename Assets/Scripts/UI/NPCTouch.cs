using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTouch : MonoBehaviour
{
    [SerializeField] StoryManager storyManager;
    private void OnMouseDown()
    {
        storyManager.NpcTouch();
    }
}
