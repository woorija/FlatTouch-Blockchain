using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour
{
    Animator animator;
    BoxCollider2D boxCollider;
    [SerializeField] Vector3 standPos;
    [SerializeField] int index;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        IndexCheck();
        SelectStandingImage();
    }
    void IndexCheck()
    {
        if (index == 0)
        {
            index = Mathf.Clamp(GameManager.Instance.currentStage, 1, 4);
        }
    }
    void SelectStandingImage()
    {
        Vector2 collider_size = Vector2.zero;
        animator.SetInteger("NPC", index);
        switch (index)
        {
            case 1:
                collider_size = new Vector2(320f, 740f);
                break;
            case 2:
                collider_size = new Vector2(430f, 605f);
                break;
            case 3:
                collider_size = new Vector2(500f, 700f);
                break;
            case 4:
                collider_size = new Vector2(460f, 640f);
                break;
        }
        animator.gameObject.transform.position = standPos;
        boxCollider.size = collider_size;
        boxCollider.transform.position = standPos;
        boxCollider.offset = new Vector2(0, collider_size.y * 0.5f);
    }
}
