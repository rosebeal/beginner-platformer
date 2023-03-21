using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 1;
    public float jumpForce;
    public float moveInput;
    public Rigidbody2D rb;
    public bool isGrounded;
    public int jumps;
    public int maxJumps = 2;
    public float normalGrav = 1;
    public float wallGrav = 0.1f;
    public bool isWallAttached;
    private float wallJumpDirectionMod;
    public float groundSpeed = 5;
    public float airSpeed = 5;
    public float maxSpeed;

    public bool canMove;
    
        
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        
        rb = GetComponent<Rigidbody2D>();
        //this.maxJumps = 2;
      
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (isWallAttached == false)
        {
           Jump();
        }
        else if (isWallAttached == true) 
        {
            WallJump();
        }
        

    }

    void FixedUpdate()
    {
        ApplyGravity();
        //SetSpeed();
        if(canMove == true) 
        { 
            Move(); 
        }

        
        
    }

    public void Jump()
    {
        /*
        if (Gamepad.current.xButton.wasPressedThisFrame)
        {
            Debug.Log("Pressed");
        }
        */
        if (Input.GetKeyDown(KeyCode.Space)  && (jumps > 0)) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            

            if(isGrounded== false) 
            {
                jumps--;
            }

        }
    }

    private void WallJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            canMove= false;
            rb.velocity = new Vector2(wallJumpDirectionMod * jumpForce, jumpForce);

            StartCoroutine(ReenableInput());
            



        }
    }

    public void Move()
    {

        float xVelocity = rb.velocity.x + (moveInput * moveSpeed);
        if (xVelocity > maxSpeed)
        {
            xVelocity= maxSpeed;
        }
        else if (xVelocity < -maxSpeed)
        {
            xVelocity= -maxSpeed;
        }
         
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
            //rb.velocity = new Vector2(rb.velocity.x+(moveInput * moveSpeed), rb.velocity.y);
        
        
    }

    /// <summary>
    /// Detects when player has collided with an object, and checks what tag the collidor object has to determine what to do 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (isGrounded== false) 
            {
                var direction = transform.InverseTransformPoint(collision.transform.position);
                isWallAttached = true;
                if (direction.x > 0f)        //collided right, jump left
                {
                    wallJumpDirectionMod = -1f;
                    Debug.Log(direction.x);
                    print(">0");
                }
                else if (direction.x < 0f)    //collided left, jump right
                {
                    wallJumpDirectionMod = 1f;
                    Debug.Log(direction.x);
                    print("<0");
                }
            }
            
        }
        */
        
        /*
        else if (collision.gameObject.CompareTag("Floor"))
        {
            
            if (isWallAttached == false)
            {
                isGrounded = true;
                jumps = 0;
            }
        }
        */
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {

            if (isWallAttached == false)
            {
                isGrounded = true;
                jumps = maxJumps;
            }
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            if (isGrounded == false)
            {
                var direction = transform.InverseTransformPoint(collision.transform.position);
                isWallAttached = true;
                if (direction.x > 0f)        //collided right, jump left
                {
                    wallJumpDirectionMod = -1f;
                    Debug.Log(direction.x);
                    print(">0");
                }
                else if (direction.x < 0f)    //collided left, jump right
                {
                    wallJumpDirectionMod = 1f;
                    Debug.Log(direction.x);
                    print("<0");
                }
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isWallAttached = false;

        }
        else if (collision.gameObject.tag == "Floor")
        {
            if (isWallAttached == false)
            {
                isGrounded = false;
                jumps--;
            }
            
        }

    }
/*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {

            if (isWallAttached == false)
            {
                isGrounded = true;
                jumps = 0;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
*/


    private void ApplyGravity()
    {
        if ((isWallAttached == true) && (rb.velocity.y <0))
        {
            rb.gravityScale = wallGrav;
        }

        else
        {
            rb.gravityScale = normalGrav;
        }
    }

    

    IEnumerator ReenableInput()
    {
        

        yield return new WaitForSeconds(0.3f);
        canMove=true;
    }

    
    


}
