using UnityEngine;
using System.Collections;

public class GiantBoss : MonoBehaviour {
    private Rigidbody rb;
    private GameObject Target;
    private Transform Guns;
    private float FireTime;

    private Transform LeftBarrel;
    private Transform RightBarrel;

    public GameObject Blunt;
    private float BeamCharge;
    private bool UseBeam;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        Target = GameObject.FindGameObjectWithTag("Driver");
        Guns = transform.FindChild("Guns");
        FireTime = 1f;

        LeftBarrel = Guns.FindChild("ShootLeft");
        RightBarrel = Guns.FindChild("ShootRight");
	}
	
	// Update is called once per frame
	void Update () {
        FireTime -= Time.deltaTime;

        bool Beam = false;
        LineRenderer Tofu = RightBarrel.GetComponent<LineRenderer>();

        if (UseBeam && Vector3.Angle(- Guns.up, Target.transform.position - Guns.position) < 20) {
            
            Beam = true;
            BeamCharge -= Time.deltaTime * 2;

            Tofu.SetPosition(0, RightBarrel.transform.position);

            RaycastHit hit;
            if (Physics.Raycast(RightBarrel.position, -Guns.up, out hit)) {
                Tofu.SetPosition(1, hit.point);
                Tofu.material.mainTextureScale = new Vector2(hit.distance / 3, 1);
            } else {
                Tofu.SetPosition(1, RightBarrel.position - Guns.up * 200);
                Tofu.material.mainTextureScale = new Vector2(70, -1);
            }
            
            Tofu.material.mainTextureOffset = new Vector2(-Time.time * 5, 1);

            if (BeamCharge <= 0) {
                BeamCharge = 0;
                UseBeam = false;
            }
        }

        if (FireTime <= 0 && Vector3.Angle(-Guns.up, Target.transform.position - Guns.position) < 20) {
            FireTime = 3;
            GameObject shot = (GameObject)Instantiate(Blunt, LeftBarrel.position, LeftBarrel.rotation);
            shot.GetComponent<Rigidbody>().velocity = (Target.transform.position - shot.transform.position).normalized * 30;
            shot.transform.forward = -shot.GetComponent<Rigidbody>().velocity;
        }

        Tofu.enabled = Beam;

        if (!Beam) {
            BeamCharge += Time.deltaTime;
            if (BeamCharge >= 5) {
                BeamCharge = 5;
                UseBeam = true;
            }
        }
	}

    void OnCollisionEnter(Collision other) {
        DriverScript driver = other.collider.GetComponentInParent<DriverScript>();
        if (driver != null) {
            driver.fuel -= 100f;
        }
    }

    void FixedUpdate() {
        Vector3 dir = Vector3.Normalize(Target.transform.position - transform.position);
        Vector3 up = -Vector3.RotateTowards(-transform.up, dir, Time.deltaTime * 0.3f, 0);
        Vector3 forward = Vector3.up;

        rb.MoveRotation(Quaternion.LookRotation(forward, up));
        
        rb.MovePosition(transform.position - transform.up * Time.fixedDeltaTime * 10);

        float pitch = Mathf.Rad2Deg * Mathf.Atan2(Guns.position.y - Target.transform.position.y,  Vector3.Distance(transform.position, Target.transform.position));
        Guns.transform.localEulerAngles = new Vector3(pitch, 0, 0);
    }
}
