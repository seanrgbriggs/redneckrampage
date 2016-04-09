using UnityEngine;
using System.Collections;

public class BeerCrate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col) {
        DriverScript driver = col.GetComponentInParent<DriverScript>();
        if (driver != null) {
            driver.nitrous = 100;
            Destroy(gameObject);
        }
    }
}
