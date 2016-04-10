using UnityEngine;
using System.Collections;

public class OilCan : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter(Collision col) {
        DriverScript driver = col.collider.GetComponentInParent<DriverScript>();
        if (driver != null) {
            driver.fuel += 10;
            driver.fuel = Mathf.Clamp(driver.fuel + 10, 0, 100);
            Destroy(gameObject);
        }
    }
}
