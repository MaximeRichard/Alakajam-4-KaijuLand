using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour {

    public float speed = 1f;
    public bool xAxis = true, yAxis = true, zAxis = true;
    Transform t;

    private void Awake()
    {
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update () {
        float time =Time.time;
        t.Rotate(new Vector3(
            xAxis ? Mathf.Cos(time) * speed * Time.deltaTime : 0,
            yAxis ? Mathf.Sin(time) * speed * Time.deltaTime : 0,
            zAxis ? Mathf.Cos(time) * speed * Time.deltaTime : 0), Space.Self);

	}
}
