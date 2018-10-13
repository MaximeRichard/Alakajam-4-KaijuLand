using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateEndTrigger : MonoBehaviour {

    public TemplateManager template;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TemplateManager.OnEnd();
    }
}
