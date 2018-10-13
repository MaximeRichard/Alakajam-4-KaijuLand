using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public float speed, edgeCheckRadius;
    public Transform leftCheck, rightCheck;
    public LayerMask platformMask;

    private bool isRight;
    private Vector3 movement;
    private Transform currentCheck;
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

        //if we are grounded...
        if (Physics.OverlapSphere(currentCheck.position, edgeCheckRadius, platformMask).Length == 0)
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
            currentCheck = leftCheck;
        }
        else
        {
            isRight = true;
            movement = Vector3.right;
            currentCheck = rightCheck;
        }
    }
}
