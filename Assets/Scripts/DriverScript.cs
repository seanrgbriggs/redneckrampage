using UnityEngine;
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
    public GUISkin Skin;

    public int Flags;

    // Use this for initialization
    void Start () {
        DrivingLogic = GetComponent<UnityStandardAssets.Vehicles.Car.CarUserControl>();
        fuel = 100f;
        nitrous = 100f;

        rb = GetComponent<Rigidbody>();
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

        GUI.skin = Skin;
        GUI.color = Color.black;
        GUI.Label(new Rect(50, 150, 128, 32), "Flags: " + Flags);
    }

}
