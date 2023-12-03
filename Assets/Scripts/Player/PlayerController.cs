using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    int speed;

    private void Awake()
    {
        speed = 700;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    public void MoveLeft()
    {
        spriteRenderer.flipX = true;
        rigid.velocity = Vector2.left * speed;
    }
    public void MoveRight()
    {
        spriteRenderer.flipX = false;
        rigid.velocity = Vector2.right * speed;
    }
    public void StopMove()
    {
        rigid.velocity = Vector2.zero;
    }
    private void Update()
    {
        PlayerAnimationCheck();
    }

    void PlayerAnimationCheck()
    {
        if (rigid.velocity.Equals(Vector2.zero))
        {
            animator.SetBool("IsWalk", false);
        }
        else
        {
            animator.SetBool("IsWalk", true);
        }
    }
}
