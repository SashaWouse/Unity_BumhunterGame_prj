using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //how fast the player can move
    public float topSpeed = 10f;

    //tell the sprite which direction it is pointing
    bool facingRight = true;

    //get reference to annimator
    Animator anim;

    //not grounded
    bool grounded = false;

    //transform at Jack foot to see if he is touching the ground
    public Transform groundCheck;

    //how big the circle is going to be when we check distance to the ground
    float groundRadius = 0.2f;

    //force of the jump
    public float jumpForce = 700f;

    //what layer is concidered ground
    public LayerMask whatIsGround;

    //variable to check double jump
    bool doubleJump = false;

    public Transform muzzle;

    public GameObject bullet;

    AudioManager audioManager;

    [HideInInspector]
    public bool usingLadder = false;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;

        anim = GetComponent<Animator>();

        //sanity check to make sure player is not dead
        anim.SetBool("isDead", false);
    }

    void Update()
    {
        GetInputMovement();
    }

    void GetInputMovement()
    {
        if ((grounded || !doubleJump) && Input.GetButtonDown("Jump"))
        {
            //not on the ground
            anim.SetBool("Ground", false);

            //add jump force to the Y axis of the rigidbody
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));

            if (!doubleJump && !grounded)
            {
                doubleJump = true;
            }
        }
        else if (!grounded || doubleJump && Input.GetButtonDown("Jump"))
        {
           anim.SetBool("DoubleJumping", doubleJump);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject mBullet = Instantiate(bullet, muzzle.position, muzzle.rotation);

            audioManager.PlaySound("Shoot Sound");

            mBullet.transform.parent = GameObject.Find("GameManager").transform;

            mBullet.GetComponent<Renderer>().sortingLayerName = "Player";

            anim.SetBool("isShooting", true);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("isShooting", false);
        }
    }

    //physics in fixed update
    void FixedUpdate()
    {
        //true or false did the ground transform hit the whatIsGround with the groundRadius
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        //tell the animator that we are grounded 
        anim.SetBool("Ground", grounded);

        //reset double jump
        if (grounded)
        {
            doubleJump = false;
        }

        //get how fast we are moving up or down from the rigidbody
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        //get move direction
        float move = Input.GetAxis("Horizontal");

        //add velocity to the rigidbody in the move direction * our speed
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * topSpeed, GetComponent<Rigidbody2D>().velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(move));

        //if we are face the negative direction and not facing right, flip
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        //saying we are facing the oposite directions
        facingRight = !facingRight;

        //get the local scale
        Vector3 theScale = transform.localScale;

        //flip on X axis
        theScale.x *= -1;

        //apply that to the local scale
        transform.localScale = theScale;
    }

}
