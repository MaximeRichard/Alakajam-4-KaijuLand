using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPanel : MonoBehaviour {

    EventSystem eventSys;
    public GameObject defaultSelectedObject;
    public void Init(EventSystem eventSys)
    {
        this.eventSys = eventSys;
        Hide();
    }

	public virtual void Show()
    {
        if(defaultSelectedObject != null) eventSys.SetSelectedGameObject(defaultSelectedObject);
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
