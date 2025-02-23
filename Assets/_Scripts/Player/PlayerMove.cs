using UnityEngine;
using UnityEngine.EventSystems;

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
        HandleMove();
        HandleJump();
        Flip();
    }

    private void HandleMove()
    {
        moveDirection = new Vector2(playerInputHandler.Move.x * walkSpeed, rb2d.linearVelocity.y);
        rb2d.linearVelocity = moveDirection;
        if (playerInputHandler.Move.x != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }


    }

    private void HandleJump()
    {
        bool isGrounded = Physics2D.Raycast(ground.transform.position, Vector2.down, 0.2f, groundLayer);
        //Debug.DrawRay(ground.position, Vector2.down, Color.green);
        moveDirection = new Vector2(rb2d.linearVelocity.x, jumpForce);

        if (playerInputHandler.isJump && isGrounded)
        {
            //rb2d.AddForce(moveDirection, ForceMode2D.Impulse);
            rb2d.linearVelocity = moveDirection;
        }

    }

    public void Flip()
    {
        if (playerInputHandler.Move.x == -1)
        {
            sr.flipX = true;
        }
        else if (playerInputHandler.Move.x == 1)
        {
            sr.flipX = false;
        }
    }
}
