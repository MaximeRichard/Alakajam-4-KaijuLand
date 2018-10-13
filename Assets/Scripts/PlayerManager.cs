using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    //****** Jump Parameters ******//
    /*these floats are the force you use to jump, the max time you want your jump to be allowed to happen,
     * and a counter to track how long you have been jumping*/
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    /*this bool is to tell us whether you are on the ground or not
     * the layermask lets you select a layer to be ground; you will need to create a layer named ground(or whatever you like) and assign your
     * ground objects to this layer.
     * The stoppedJumping bool lets us track when the player stops jumping.*/
    public bool grounded;
    public LayerMask whatIsGround;
    public bool stoppedJumping;

    /*the public transform is how you will detect whether we are touching the ground.
     * Add an empty game object as a child of your player and position it at your feet, where you touch the ground.
     * the float groundCheckRadius allows you to set a radius for the groundCheck, to adjust the way you interact with the ground*/

    public Transform groundCheck;
    public float groundCheckRadius;

    //********** Other **********//

    public float speed;
    public float maxSpeed = 100f;
    public int life = 3;
    public float invincibilityTimer;
    public float timeToCharge;
    public float basePowerRate = 1f;
    public float rotationTreshold = 0.01f;
    public Renderer playerRenderer;

    private Rigidbody2D rb;
    private bool isTouched,isFacingRight;
    private Color playerRendererColor;
    private float chargeTimeCounter;
    private float cachedGravityScale;

    [SerializeField]
    private float currentPower;

    public float CurrentPower
    {
        get
        {
            return currentPower;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRendererColor = playerRenderer.material.color;
        //sets the jumpCounter to whatever we set our jumptime to in the editor
        jumpTimeCounter = jumpTime;
        isTouched = false;
        cachedGravityScale = rb.gravityScale;
        chargeTimeCounter = 0f;
        isFacingRight = true;
    }

    void FixedUpdate()
    {
        //I placed this code in FixedUpdate because we are using phyics to move.

        //if you press down the mouse button...
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //and you are on the ground...
            if (grounded)
            {
                //jump!
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                stoppedJumping = false;
            }
        }

        //if you keep holding down the mouse button...
        if (Input.GetKey(KeyCode.UpArrow) && !stoppedJumping)
        {
            //and your counter hasn't reached zero...
            if (jumpTimeCounter > 0)
            {
                //keep jumping!
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
                jumpTimeCounter -= Time.deltaTime;
            }
        }


        //if you stop holding down the mouse button...
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            //stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the update function.
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.gravityScale = cachedGravityScale * 1.2f;
        }
        else if (Input.GetKeyUp(KeyCode.Space)) {
            rb.gravityScale = cachedGravityScale;
            chargeTimeCounter = 0;
        }
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }

    void Update()
    {

        //***************Input Movement Left Right****************//

        float moveHorizontal = Input.GetAxis("Horizontal");
        
        if (moveHorizontal > 0 && !isFacingRight)
        {
            GetComponent<Animator>().SetBool("Moving", true);
            Debug.Log("Rotate Right " + isFacingRight);
            FlipCharacter(true);
        }
        else if (moveHorizontal < 0 && isFacingRight)
        {
            GetComponent<Animator>().SetBool("Moving", true);
            Debug.Log("Rotate Left");
            FlipCharacter(false);
        }
        else if (moveHorizontal == 0) GetComponent<Animator>().SetBool("Moving", false);
        if (!isFacingRight) moveHorizontal = -moveHorizontal;
        Vector3 movement = new Vector3(0.0f, 0.0f, moveHorizontal);

        // Move the object forward along its z axis 1 unit/second.
        transform.Translate(movement * Time.deltaTime * speed);

        //***************Input Jump Processing*******************//

        //determines whether our bool, grounded, is true or false by seeing if our groundcheck overlaps something on the ground layer


        //determines whether our bool, grounded, is true or false by seeing if our groundcheck overlaps something on the ground layer
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (Input.GetKey(KeyCode.Space))
        {
            chargeTimeCounter += Time.deltaTime;
            if(chargeTimeCounter > timeToCharge)
            {
                currentPower += basePowerRate;
            }
        }

            //if we are grounded...
            if (grounded)
        {
            //the jumpcounter is whatever we set jumptime to in the editor.
            chargeTimeCounter = 0f;
            jumpTimeCounter = jumpTime;
        }
    }

    private void FlipCharacter(bool v)
    {
        isFacingRight = v;
        if (isFacingRight)
        {
            transform.RotateAround(transform.position, Vector3.up, 180f);
        }
        else
        {
            transform.RotateAround(transform.position, Vector3.up, -180f);
        }
    }

    void TakeDamage()
    {
        life--;
        if (life > 0)
        {
            
            playerRendererColor.a = 0.5f;
            playerRenderer.material.color = playerRendererColor;
            StartCoroutine(InvincibilityCooldown());
        }
        //TODO Implement invincibility cooldown
        else
        {
            //TODO Trigger Death Animation and end game
            Debug.Log("Dead");
        }
    }

    IEnumerator InvincibilityCooldown()
    {
        yield return new WaitForSeconds(invincibilityTimer);
        playerRendererColor.a = 1f;
        playerRenderer.material.color = playerRendererColor;
        isTouched = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" && !isTouched)
        {
            isTouched = true;
            TakeDamage();
        }
    }
}
