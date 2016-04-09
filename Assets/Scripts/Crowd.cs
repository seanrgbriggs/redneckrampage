using UnityEngine;
using System.Collections;

public class Crowd : MonoBehaviour {
    private float Offset;

	// Use this for initialization
	void Start () {
        Offset = Random.RandomRange(0, 2 * Mathf.PI);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.up * 0.01f * Mathf.Sin(Time.time * 10 + Offset);
	}
}
