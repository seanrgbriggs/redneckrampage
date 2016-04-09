using UnityEngine;
using System.Collections;

public class GunnerScript : MonoBehaviour {

    Transform truck;
    public Gun[] weapons;
    Gun shootyStick;

    public Vector3 offset = new Vector3(0,1,-0.8f);

    Vector3 relPos = Vector3.zero;
    Vector2 xbounds = new Vector2(-0.5f,0.5f);
    Vector2 zbounds = new Vector2(-0.7f,0.7f);
    Transform cam;

    public float speed = 0.05f;
    public float sensitivity =  500000f;

	// Use this for initialization
	void Start () {
        truck = GameObject.FindGameObjectWithTag("Driver").transform;
        transform.SetParent(truck);
        transform.localPosition = offset;
        cam = transform.GetChild(0);
        AssignGun(weapons[0]);
	}

    void Update() {
        if (Input.GetButtonDown("Fire1"))
            shootyStick.Shoot();

        

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0));   
        cam.Rotate(new Vector3(Input.GetAxis("Mouse Y") * sensitivity,0, 0));

    }
	void FixedUpdate () {

        transform.position += speed*(Input.GetAxis("HorizontalB") * transform.right + Input.GetAxis("VerticalB") * transform.forward);

        relPos = transform.localPosition - offset;
        relPos.x = Mathf.Clamp(relPos.x, xbounds.x, xbounds.y);
        relPos.y = 0;
        relPos.z = Mathf.Clamp(relPos.z, zbounds.x, zbounds.y);

        transform.localPosition = offset + relPos;
    }

    public void AssignGun(Gun g) {
        shootyStick = Instantiate(g);
        shootyStick.transform.SetParent(cam);
        Vector3 gunOffset = Vector3.down/2+Vector3.right/4;
        shootyStick.transform.localPosition=(gunOffset);

    }
}
