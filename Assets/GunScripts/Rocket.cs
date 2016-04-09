using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

    Rigidbody rb;
    const float speed = 25f;
    const float boom = 10f;
    const float boomRad = 25f;
    const float boomDmg = 500f;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.AddForce(speed * transform.up, ForceMode.Acceleration);
	}

    void OnCollisionEnter(Collision col) {
        rb.AddExplosionForce(boom, transform.position, boomRad);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject target in enemies)
            if ((transform.position - target.transform.position).magnitude <= boomRad)
                target.GetComponent<DamageScript>().damage(boomDmg);
        
        Destroy(gameObject);
    }
}
