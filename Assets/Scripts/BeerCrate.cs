using UnityEngine;
using System.Collections;

public class BeerCrate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col) {
        DriverScript driver = col.collider.GetComponentInParent<DriverScript>();
        if (driver != null) {
            driver.nitrous = 100;
            Destroy(gameObject);
        }
    }
}
