using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateManager : MonoBehaviour {

    internal void OnEnd()
    {
        GameManager.Instance.OnTemplateEnd(this);
    }

    internal void OnStart()
    {
        GameManager.Instance.OnTemplateStart(this);
    }
}
