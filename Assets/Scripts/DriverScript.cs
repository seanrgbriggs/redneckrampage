﻿using UnityEngine;
using System.Collections;

public class DriverScript : MonoBehaviour {

    UnityStandardAssets.Vehicles.Car.CarUserControl DrivingLogic;
    public float fuel;
    public float nitrous;

    Rigidbody rb;
    const float fuelUse = 0.001f;

    // Use this for initialization
    void Start () {
        DrivingLogic = GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>();
        fuel = 100f;
        nitrous = 100f;

        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        DrivingLogic.enabled = fuel > 0;
        fuel = Mathf.Clamp(fuel-Mathf.Abs(rb.velocity.magnitude*fuelUse), 0, 100);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}

    void OnCollisionEnter(Collision col) {
        switch (col.transform.tag) {
            case "Fuel":
                fuel = 100;
                Destroy(col.gameObject);
                break;
            case "Nitrous":
                nitrous = 100;
                Destroy(col.gameObject);
                break;
            case "Enemy":
                fuel -= 2;
                break;
        }
    }

}
