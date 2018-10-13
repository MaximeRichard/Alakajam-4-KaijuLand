using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerY : MonoBehaviour {

    public Transform target;

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
    }
}
