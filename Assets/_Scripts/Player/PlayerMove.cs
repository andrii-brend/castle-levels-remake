using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Speed")]
    [SerializeField] private float walkSpeed = 6.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 16.0f;

    [Header("Ground Parameters")]
    [SerializeField] private Transform ground;
    [SerializeField] private LayerMask groundLayer;

    private InputHandler playerInputHandler;
    private Vector2 moveDirection;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private Animator anim;

    private bool isGrounded; // ������ ����� ��� �������� ����

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        playerInputHandler = InputHandler.instance;
    }

    private void Update()
    {
        isGrounded = Physics2D.Raycast(ground.position, Vector2.down, 0.2f, groundLayer);

        HandleMove();
        HandleJump();
        Flip();
        UpdateAnimation();
    }

    private void HandleMove()
    {
        moveDirection = new Vector2(playerInputHandler.Move.x * walkSpeed, rb2d.linearVelocity.y);
        rb2d.linearVelocity = moveDirection;
    }

    private void HandleJump()
    {
        if (playerInputHandler.isJump && isGrounded)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
        }
    }

    private void UpdateAnimation()
    {
        if (!isGrounded)
        {
            anim.SetBool("isJumping", true);
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isMoving", playerInputHandler.Move.x != 0);
        }
    }

    private void Flip()
    {
        if (playerInputHandler.Move.x < 0)
        {
            sr.flipX = true;
        }
        else if (playerInputHandler.Move.x > 0)
        {
            sr.flipX = false;
        }
    }
}
