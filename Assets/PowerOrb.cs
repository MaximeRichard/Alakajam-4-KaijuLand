using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOrb : MonoBehaviour {


    public AnimationCurve LightIntensityCurve = AnimationCurve.Linear(1,1,1,1);
    public float maxIntensity, minIntensity;
    public Light light;
    public GameObject particlePrefabPickedUp;
    public int powerToAdd = 10;
	
	// Update is called once per frame
	void Update () {
        light.intensity = LightIntensityCurve.Evaluate(Time.time) * (maxIntensity - minIntensity) + minIntensity;

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerManager>().AddPower(powerToAdd);
            Instantiate(particlePrefabPickedUp).transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
