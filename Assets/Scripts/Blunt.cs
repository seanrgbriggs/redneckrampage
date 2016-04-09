using UnityEngine;
using System.Collections;

public class Blunt : MonoBehaviour {
    public GameObject Explosion;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other) {
        DriverScript driver = other.collider.GetComponentInParent<DriverScript>();
        if (driver != null) {
            driver.fuel -= 5.0f;
        }
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
