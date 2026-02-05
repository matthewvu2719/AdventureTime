using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool canDoubleJump;
    private Animator anim;
    private bool canWallSlide;
    private bool isWallSliding;
    

    [Header("Move info")]
    public float moveSpeed;
    public float jumpForce;
    private float movingInput;
    public float doubleJumpForce;
    public Vector2 wallJumpDirection;
    private bool canMove;
    private float defaultJumpForce;

    [SerializeField] private float bufferJumpTime;
    private float bufferJumpTimer;

    [SerializeField] private float cayoteJumpTime;
    private float cayoteJumpTimer;
    private bool canHaveCayoteJump;


    [Header("Collision info")]
    public LayerMask ground;
    public float groundCheckDistance;
    private bool isGrounded;
    public float wallCheckDistance;
    private bool wallDetected;

    private bool facingRight = true;
    private int facingDirection =1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        defaultJumpForce = jumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        AnimationController();
        FlipController();
        CollisionCheck();
        InputCheck();
       
        bufferJumpTimer -=Time.deltaTime;
        cayoteJumpTimer -=Time.deltaTime;

        if (isGrounded)
        {
            canDoubleJump = true;
            canMove = true;
            if(bufferJumpTimer > 0)
            {
                bufferJumpTimer = -1;
                Jump();
            }
            canHaveCayoteJump = true;
        }
        else
        {
            if (canHaveCayoteJump)
            {
                canHaveCayoteJump = false;
                cayoteJumpTimer = cayoteJumpTime;
            }

        }

        if (canWallSlide)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y*0.1f);
        }

        Move(); 

    }

    private void InputCheck()
    {
        movingInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetAxis("Vertical") < 0)
        {
            canWallSlide = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            JumpButton();
        }
    }

    private void AnimationController()
    {

        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded",isGrounded);
        anim.SetBool("wallDetected",wallDetected);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetFloat("yVelocity",rb.velocity.y);
        
    }

    private void JumpButton()
    {
        if (!isGrounded)
        {
            bufferJumpTimer = bufferJumpTime;
        }
        if (isWallSliding)
        {
            WallJump();
            canDoubleJump= true;
        }

        else if (isGrounded || cayoteJumpTimer >0)
        {
            Jump();
        }
        else if (canDoubleJump)
        {
            canMove= true;
            canDoubleJump = false;
            jumpForce = doubleJumpForce;
            Jump();
            jumpForce = defaultJumpForce;
        }
        canWallSlide=false;
    }

    private void Move()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(moveSpeed * movingInput, rb.velocity.y);
        }
    }

    private void WallJump()
    {
        canMove = false;
        rb.velocity = new Vector3(wallJumpDirection.x * -facingDirection, wallJumpDirection.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void FlipController()
    {
        if(facingRight && rb.velocity.x < 0)
        {   
            Debug.Log(rb.velocity.x);
            Flip();
        }
        else if(!facingRight && rb.velocity.x > 0f)
        {
            Debug.Log(rb.velocity.x);
            Flip();
        }
    }
    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void CollisionCheck()
    {
        wallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, ground);
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, ground);

        if(wallDetected && rb.velocity.y < 0)
        {
            canWallSlide = true;
        }

        if (!wallDetected)
        {
            canWallSlide = false;
            isWallSliding = false;
        }
    }
}
