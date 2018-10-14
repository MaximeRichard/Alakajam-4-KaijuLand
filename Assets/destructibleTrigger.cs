using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructibleTrigger : MonoBehaviour
{
    public GameObject destroyedVersion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if(collision.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerManager>().isDashing)
            {
                GameObject go = Instantiate(destroyedVersion, transform.position, transform.rotation, transform.parent);
                go.transform.localScale = transform.localScale;
                Destroy(gameObject);
                Destroy(go, 3f);
            }
        }
    }
}