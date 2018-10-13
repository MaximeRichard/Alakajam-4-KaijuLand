using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateStartTrigger : MonoBehaviour {

    public TemplateManager template;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        template.OnStart();
    }

}
