using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{

    public Transform target;

    public float offsetY = 1;
    public float maxDistance = 2;
    public float smoothFactor = 0.25f;


    private void FixedUpdate()
    {
        float posY = Mathf.Lerp(transform.position.y, target.position.y + offsetY, smoothFactor);
        float diff = target.position.y - posY;
        if (diff < -maxDistance)
        {
            posY = target.position.y + maxDistance;
        }
        transform.position = new Vector3(0, posY, transform.position.z);
    }
    

}

