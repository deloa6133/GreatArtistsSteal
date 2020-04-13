using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //reference to the PlayerAnimatorController
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    //How fast the player moves (sneaking)
    public float topSpeed = 4f;
    bool sprint = false;

    //determine sprite direction
    bool faceRight = true;

    //checks if player is grounded (if player is grounded, can jump, if not, can not)
    bool grounded = false;//not grounded
    public Transform groundCheck;//transform at player feet to check if grounded or not
    float groundRadius = 0.2f;//how large the circle is when checking distance to ground
    public float jumpForce = 300f;//how high the player can jump
    public LayerMask whatIsGround;//checks which layer is currently considered the ground
    public float gravityForce = -40f;

    //checks if the player is close to a wall on either the left or right side
    //if the player is close to the wall, stop x movement
    //bool wallLeft = false;//not near left wall
    //bool wallRight = false;//not near right wall
    //public Transform wallCheckLeft;//tansform at left side of player to check if near wall
    //public Transform wallCheckRight;//tansform at right side of player to check if near wall
    //float wallRadius = 0.2f;
    //public LayerMask whatIsWall;//determines what is considered a wall layer (what the player cannot interact with)

    //physics in fixed update
    private void FixedUpdate()
    {
        //true or false output for whether the wall transforms are hitting the whatIsWall with the wallRadius
        //wallLeft = Physics2D.OverlapCircle(wallCheckLeft.position, wallRadius, whatIsWall);
        //wallRight = Physics2D.OverlapCircle(wallCheckRight.position, wallRadius, whatIsWall);

        //true or false output for whether the ground transform hit the whatIsGround with the groundRadius
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);//tell the animator that the player is grounded

        //get movement direction of player
        float move = Input.GetAxis("Horizontal");

        //create movement in the player horizontal direction
        //if ((wallLeft) && (Input.GetKeyDown(KeyCode.A)))//if player is near a wall, unable to move to that direction
        //{
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * topSpeed, GetComponent<Rigidbody2D>().velocity.y);
        //}
        //else
        //{
            //GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        //}
        
        anim.SetFloat("Speed", Mathf.Abs(move));

        //if facing negative direction and not facing the right FLIP
        if (move > 0 && !faceRight)
        {
            Flip();
        }
        else if (move < 0 && faceRight)//re-flips the sprite to the original direction if going back
        {
            Flip();
        }
    }

    private void Update()
    {
        if(grounded && Input.GetKeyDown(KeyCode.Space))//determines whether or not the player is near the ground and can jump or not
        {
            //adding jumpforce to the y-axis of the rigidBody
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }
        if (!grounded)//adding faster fall speed to character when player is not grounded
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, gravityForce));
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprint = true;
            topSpeed = 10f;
            
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            topSpeed = 4f;
            sprint = false;
        }
        anim.SetBool("Sprint", sprint);
    }

    //flips the direction of the sprite depending on which way the player is facing/moving
    void Flip()
    {
        //face the opposite direction
        faceRight = !faceRight;
        //get local scale
        Vector3 theScale = transform.localScale;
        //flip the sprite on the x axis
        theScale.x *= -1;
        //apply the flip to the local scale of the player
        transform.localScale = theScale;
    }

}
