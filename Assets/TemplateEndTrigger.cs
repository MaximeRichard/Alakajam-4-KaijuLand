using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateEndTrigger : MonoBehaviour {

    public TemplateManager template;
    bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered)
        {
            template.OnEnd();
            triggered = true;
        }

    }
}
