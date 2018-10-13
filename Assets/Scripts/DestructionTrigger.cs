using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Destructible")
        {
            Destroy(gameObject);
        }
    }
}
