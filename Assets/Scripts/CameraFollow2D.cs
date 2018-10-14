using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{

    public Transform target;

    public float offsetY = 1;
    public float maxDistance = 2;
    public float smoothFactorY = 0.25f;
    public float smoothFactorX = 0.5f;
    public Vector3 previousPos;
    public bool followX = false;


    private void FixedUpdate()
    {        
        float posY = Mathf.Lerp(transform.position.y, target.position.y + offsetY, smoothFactorY);
        float diffY = target.position.y - posY;
        float posX = Mathf.Lerp(transform.position.x, target.position.x, smoothFactorX);
        float diffX = target.position.y - posY;
        if (diffY < -maxDistance)
        {
            posY = target.position.y + maxDistance;
        }

        transform.position = new Vector3(followX ? posX : 0, posY, transform.position.z);
    }
    

}

