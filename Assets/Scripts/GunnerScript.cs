using UnityEngine;
using System.Collections;

public class GunnerScript : MonoBehaviour {

    Transform truck;
    Gun shootyStick;

    Vector3 offset = new Vector3(0,1,-0.8f);

    Vector3 relPos = Vector3.zero;
    Vector2 xbounds = new Vector2(-0.5f,0.5f);
    Vector2 zbounds = new Vector2(-0.7f,0.7f);

    public float speed = 0.05f;


	// Use this for initialization
	void Start () {
        truck = GameObject.FindGameObjectWithTag("Driver").transform;
        transform.SetParent(truck);
        transform.localPosition = offset;
	}

    void Update() {
        if (Input.GetButtonDown("Fire1"))
            shootyStick.Shoot();

        Transform cam = transform.GetChild(0);
        cam.forward = transform.forward;
           
    }
	void FixedUpdate () {

        transform.position += speed*(Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward);

        relPos = transform.localPosition - offset;
        relPos.x = Mathf.Clamp(relPos.x, xbounds.x, xbounds.y);
        relPos.y = 0;
        relPos.z = Mathf.Clamp(relPos.z, zbounds.x, zbounds.y);

        transform.localPosition = offset + relPos;
    }

    
}
