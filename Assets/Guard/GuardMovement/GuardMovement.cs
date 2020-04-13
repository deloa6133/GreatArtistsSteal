using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour {

    //look distance of guard to determine if chasing or not
    /*public float lookDistance = 30f;
    public LayerMask hitting;//determines what to hit with the raycast
    public Transform originPoint;
    private Vector2 direction = new Vector2(-1, 0);*/


    //determine sprite direction
    bool faceRight = true;

    //left and right wall check
    bool wallCheckL = false;
    bool wallCheckR = false;
    public LayerMask whatIsGuardWall;//determines what is considered a wall for the guard
    float wallRadius = .2f;
    public Transform wallNearL;
    public Transform wallNearR;
    bool movingRight = true;

    bool Patrolling = true;
    bool playerCaught = false;
    bool Incapacitated = false;
    bool chasing = false;

    public Transform incapacitatedTrigger;
    public Transform chasingTrigger;
    public Transform caughtTrigger;

    //player componenets to be called from script
    public Transform player;
    Animator otherAnimator;
    public Transform playerObject;
    public GameObject otherObject;

    //animator
    Animator anim;

    //guard move speed
    public float walkSpeed = 3f;

    private void Awake()
    {
        incapacitatedTrigger = this.gameObject.transform.GetChild(4);
        caughtTrigger = this.gameObject.transform.GetChild(2);
        chasingTrigger = this.gameObject.transform.GetChild(3);
        player = GameObject.Find("Player").transform;
        
        otherAnimator = otherObject.GetComponent<Animator>();
    }

    public void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool( "Patrolling",Patrolling);
    }

    private void FixedUpdate()
    {
        wallCheckL = Physics2D.OverlapCircle(wallNearR.position, wallRadius, whatIsGuardWall);
        wallCheckR = Physics2D.OverlapCircle(wallNearL.position, wallRadius, whatIsGuardWall);

        if(Patrolling == true && Incapacitated == false && chasing == false && playerCaught == false)
        {
            switch (movingRight)
            {
                case true:
                    moveRight();
                    if (wallCheckR)
                    {
                        Flip();
                        movingRight = false;
                        faceRight = false;
                    }
                    break;

                case false:
                    moveLeft();
                    if (wallCheckL)
                    {
                        Flip();
                        movingRight = true;
                        faceRight = true;
                    }
                    break;
            }
        }
        else if (Incapacitated == true && Patrolling == false)
        {
            Patrolling = false;
        }
        else if (Incapacitated == false && chasing == true)
        {
            if (player.transform.localPosition.x > transform.localPosition.x)
            {      
                transform.position += transform.right * (walkSpeed * 2) * Time.deltaTime;  
            }
            else if (player.transform.localPosition.x < transform.localPosition.x)
            {
                transform.position += -transform.right * (walkSpeed * 2) * Time.deltaTime;    
            }
        }
        else if (Incapacitated == false && playerCaught == true)
        {
            otherAnimator.SetBool("PlayerCaught", playerCaught);
        }
        anim.SetBool("Incapacitated", Incapacitated);
        anim.SetBool("Chasing", chasing);

    }

    private void Update()
    {
        /*Debug.DrawRay(originPoint.position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(originPoint.position, direction, lookDistance);

        if(hit == true)
        {
            if (hit.collider.tag == ("Player"))
            {
                Debug.Log("Player Hit");
            }
            if (hit.collider.tag == ("Wall"))
            {
                direction *= -1;
            }
        }

        anim.SetBool("Caught", playerCaught);*/

    }

    //movement of the guard position
    public void moveLeft()
    {
        transform.position += transform.right * walkSpeed * Time.deltaTime;
        
        Patrolling = true;
    }
    public void moveRight()
    {
        transform.position += -transform.right * walkSpeed * Time.deltaTime;
        Patrolling = true;
    }

    //flips the direction of the sprite depending on which way the player is facing/moving
    void Flip()
    {
        SpriteRenderer guardSprite = gameObject.GetComponent<SpriteRenderer>();
        
        if (guardSprite.flipX == true)
        {
            guardSprite.flipX = false;
        }
        else
        {
            guardSprite.flipX = true;
        }

    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            Debug.Log("Guard incapacitated");
            Incapacitated = true;
            //anim.SetTrigger("IncapacitatedOnce");
            gameObject.GetComponent<Collider2D>().enabled = false;

            //moves the guard back in space to allow the player sprite to not clip
            //gameObject.transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y - 1, 1);
        }
    }*/

    public void GuardChasing()
    {
        Debug.Log("GuardChasing");
        chasing = true;
    }
    public void GuardCuaght()
    {
        chasing = false;
        Debug.Log("GuardCaught");
        playerCaught = true;
        otherAnimator.GetComponent<Animator>().SetTrigger("Caught");
    }
    public void GuardIncapacitated()
    {
        Debug.Log("Guard incapacitated");
        Incapacitated = true;
        //anim.SetTrigger("IncapacitatedOnce");
        gameObject.GetComponent<Collider2D>().enabled = false;

        //moves the guard back in space to allow the player sprite to not clip
        //gameObject.transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y - 1, 1);
    }
}
