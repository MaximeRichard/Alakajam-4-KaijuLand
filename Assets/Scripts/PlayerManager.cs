using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public float speed;
    public int life = 3;
    public float invincibilityTimer;

    private Rigidbody rb;
    private bool isTouched;
    private Color playerRendererColor;
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



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerRendererColor = gameObject.GetComponent<Renderer>().material.color;
        //sets the jumpCounter to whatever we set our jumptime to in the editor
        jumpTimeCounter = jumpTime;
        isTouched = false;
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
    }

    void Update()
    {

        //***************Input Movement Left Right****************//

        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f);

        // Move the object forward along its z axis 1 unit/second.
        transform.Translate(movement * Time.deltaTime * speed);

        //***************Input Jump Processing*******************//

        //determines whether our bool, grounded, is true or false by seeing if our groundcheck overlaps something on the ground layer
        Physics.OverlapSphere(groundCheck.position, groundCheckRadius, whatIsGround);

        //if we are grounded...
        if (Physics.OverlapSphere(groundCheck.position, groundCheckRadius, whatIsGround).Length > 0)
        {
            grounded = true;
            //the jumpcounter is whatever we set jumptime to in the editor.
            jumpTimeCounter = jumpTime;
        }
        else
        {
            grounded = false;
        }
    }

    void TakeDamage()
    {
        life--;
        if (life > 0)
        {
            
            playerRendererColor.a = 0.5f;
            gameObject.GetComponent<Renderer>().material.color = playerRendererColor;
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
        gameObject.GetComponent<Renderer>().material.color = playerRendererColor;
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
