﻿using UnityEngine;
using System.Collections;

public class DriverScript : MonoBehaviour {

    UnityStandardAssets.Vehicles.Car.CarUserControl DrivingLogic;
    public float fuel;
    public float nitrous;

    Rigidbody rb;
    const float fuelUse = 0.05f;
    const float nitrousUse = 5f;
    const float boostSpeed = 20f;

    public Texture2D MeterBase;
    public Texture2D FuelMeter;
    public Texture2D BeerMeter;
    public Texture2D LabelBeer;
    public Texture2D LabelFuel;
    public Texture2D BossBar;
    public Texture2D BossBarFill;
    public Texture2D Divider;
    public GUISkin Skin;

    public int Flags;
    public GameObject HouseBoom;
    public GameObject Boss;
    private bool bossSpawned;

    private GiantBoss bossInst;

    private ParticleSystem Exhaust;

    // Use this for initialization
    void Start () {
        DrivingLogic = GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>();
        fuel = 100f;
        nitrous = 100f;

        rb = GetComponent<Rigidbody>();
        Exhaust = transform.FindChild("Exhaust").GetComponent<ParticleSystem>();
        //rb.centerOfMass = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        DrivingLogic.enabled = fuel > 0;
        fuel = Mathf.Clamp(fuel - Mathf.Abs(rb.velocity.magnitude * Time.deltaTime * fuelUse), 0, 100);

        if (Input.GetButton("Boost") && nitrous > 0) {
            nitrous -= Time.deltaTime* nitrousUse;
            if (nitrous < 0) {
                nitrous = 0;
            }
            rb.AddForce(transform.forward*boostSpeed, ForceMode.Acceleration);
        }

        if (Flags >= 2 && !bossSpawned) {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("House")) {
                Instantiate(HouseBoom, obj.transform.position, Quaternion.identity);
                Destroy(obj);
            }

            bossInst = ((GameObject)Instantiate(Instantiate(Boss, new Vector3(250, 250, 250), Quaternion.Euler(270, 0, 0)))).GetComponent<GiantBoss>() ;
            bossSpawned = true;
        }

        Vector3 c = Camera.main.transform.localPosition;
        Vector3 a = Camera.main.transform.localEulerAngles;
        c.x = -Input.GetAxis("Horizontal") * 1;
        a.y = Input.GetAxis("Horizontal") * 20;

        Vector3 t = c;
        if (Input.GetButton("Boost") && nitrous > 0) {
            t.z = -7;
            Exhaust.enableEmission = true;
        } else {
            t.z = -5;
            Exhaust.enableEmission = false;
        }

        c = Vector3.MoveTowards(c, t, Time.deltaTime * 10);

        Camera.main.transform.localPosition = c;
        Camera.main.transform.localEulerAngles = a;
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

    void OnGUI() {
        GUI.DrawTexture(new Rect(50, 50, 128, 32), MeterBase);
        GUI.DrawTexture(new Rect(50, 50, 128f * fuel / 100f, 32), FuelMeter);
        GUI.DrawTexture(new Rect(50, 50, 128, 32), LabelFuel);
        GUI.DrawTexture(new Rect(50, 100, 128, 32), MeterBase);
        GUI.DrawTexture(new Rect(50, 100, 128f * nitrous / 100f, 32), BeerMeter);
        GUI.DrawTexture(new Rect(50, 100, 128, 32), LabelBeer);

        GUI.DrawTexture(new Rect(Screen.width / 2 - 8, 0, 16, Screen.height), Divider);

        if (bossSpawned) {
            GUI.DrawTexture(new Rect(Screen.width / 2 - 256, 50, 512, 32), BossBar);
        }

        GUI.skin = Skin;
        GUI.color = Color.black;
        GUI.Label(new Rect(50, 150, 128, 32), "Flags: " + Flags);
    }

}
