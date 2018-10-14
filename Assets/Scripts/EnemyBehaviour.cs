using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public float speed, edgeCheckRadius;
    public Transform leftCheck, rightCheck;
    public LayerMask platformMask;
    public int powerBonus = 10;

    private bool isRight,endOfPlatform;
    private Vector3 movement;
    private Transform currentCheck;
    private Transform absoluteLeft, absoluteRight;
    
    // Use this for initialization
    void Start () {
        isRight = true;
        movement = Vector3.right;
        currentCheck = rightCheck;
	}
	
	// Update is called once per frame
	void Update () {

        // Move the object forward along its z axis 1 unit/second.
        transform.Translate(movement * Time.deltaTime * speed);

        endOfPlatform = Physics2D.OverlapCircle(currentCheck.position, edgeCheckRadius, platformMask);
        if (!endOfPlatform)
        {
            Flip();
        }
    }

    void Flip()
    {
        if (isRight)
        {
            isRight = false;
            movement = Vector3.left;
            currentCheck = rightCheck;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            isRight = true;
            movement = Vector3.right;
            currentCheck = rightCheck;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<PlayerManager>().grounded)
            {
                //TODO add force to blow back player
                //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
                collision.gameObject.GetComponent<PlayerManager>().AddPower(powerBonus);
                if (GetComponent<Animator>()) GetComponent<Animator>().SetTrigger("Hit");
                else Destroy(gameObject);
            }
        }
    }
}
