using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public GameObject Enemy;
    private GameObject Player;
    private float TimeToSpawn = 0;
    public float SpawnTime;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Driver");
	}
	
	// Update is called once per frame
	void Update () {
        TimeToSpawn -= Time.deltaTime;
	    if (TimeToSpawn <= 0 && Vector3.SqrMagnitude(Player.transform.position - transform.position) > 50 * 50) {
            TimeToSpawn = SpawnTime;
            Instantiate(Enemy, transform.position, Quaternion.identity);
        }
	}
}
