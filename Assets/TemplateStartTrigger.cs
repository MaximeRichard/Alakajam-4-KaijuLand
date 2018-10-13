using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateStartTrigger : MonoBehaviour {

    public TemplateManager template;
    bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered)
        {
            template.OnStart();
            triggered = true;
        }
            
    }

}
