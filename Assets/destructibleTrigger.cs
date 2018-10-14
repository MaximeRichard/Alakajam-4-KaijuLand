using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructibleTrigger : MonoBehaviour
{
    public GameObject destroyedVersion;
    void OnMouseDown()
    {
        GameObject go = Instantiate(destroyedVersion, transform.position, transform.rotation, transform.parent);
        go.transform.localScale = transform.localScale;
        Destroy(gameObject);
    }
}