using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Car;

public class DamageScript : MonoBehaviour {

	public float health;
    public GameObject Explosion;

	// Use this for initialization
	void Start () {
		if (health <= 0)
			health = 1;
        GetComponent<CarAIControl>().SetTarget(GameObject.FindGameObjectWithTag("Driver").transform);
	}
	
	// Update is called once per frame
	void Update () {
		checkLiving ();
	}

	void checkLiving(){
		if (health <= 0)
			Die ();
	}

	public void damage(float dmg){
		health -= dmg;
	}

	void Die() {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy (this.gameObject);
	}

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Driver") {
            Die();
        }
    }
}
