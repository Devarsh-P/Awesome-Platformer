using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D myBody;
    private Animator anim;

    public Transform groundCheckPosition; //Decalring the Transform type game object. 
    public LayerMask groundLayer; //Decalring the ground layer variable.
   

    private bool isgrounded;
    private bool jumped;

    public float jumpPower = 5f;

    void Awake() 
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckIfGrounded();
        PlayerJump();
    }

    

    void FixedUpdate()
    {
        PlayerWalk();
    }

    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if(h>0)
        {
            myBody.velocity = new Vector2 (speed , myBody.velocity.y);

            ChangeDirection(1);
        }
        else if(h<0)
        {
            myBody.velocity = new Vector2 (-speed , myBody.velocity.y);

            ChangeDirection(-1);
        }
        else
        {
            myBody.velocity = new Vector2 (0f , myBody.velocity.y);

        }
        
        anim.SetInteger ("Speed" ,Mathf.Abs((int)myBody.velocity.x));

    }

    void ChangeDirection(int direction)                 //To change the scale of the player when the direction of the movement is changed.
    {
        Vector3 tempScale = transform.localScale;       //Getting the local scale value into the variable from the original refernse point.
        tempScale.x = direction;                        //Changing the x scale of the player.
        transform.localScale = tempScale;               //Giving back the local scale value to the referense point. 
    }

    void CheckIfGrounded()
    {
        isgrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        if (isgrounded)
        {
            if (jumped)
            {
                jumped = false;

                anim.SetBool("Jump", false);
            }
        }
    }

    void PlayerJump()
    {
        if(isgrounded)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
                
                anim.SetBool("Jump",true);

            }
        }
    }
    

}//Class
