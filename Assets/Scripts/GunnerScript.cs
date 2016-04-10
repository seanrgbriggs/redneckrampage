using UnityEngine;
using System.Collections;

public class GunnerScript : MonoBehaviour
{

    public Gun[] weapons;
    public int curWeap;
    Gun shootyStick;

    public Vector3 offset = new Vector3(0, 1, -0.8f);

    Vector3 relPos = Vector3.zero;
    Vector2 xbounds = new Vector2(-0.5f, 0.5f);
    Vector2 zbounds = new Vector2(-0.7f, 0.7f);
    public Transform cam;

    public float speed = 0.05f;
    public float sensitivity = 500000f;
    public float ShowHitTime;

    public Texture2D Crosshair;
    public Texture2D Hit;

    //private float pitch;
    private float yaw;

    private bool track = true;

    // Use this for initialization
    void Start()
    {
        transform.localPosition = offset;
        cam = GetComponentInChildren<Camera>().transform;
        AssignGun(weapons[curWeap]);

        yaw = transform.eulerAngles.y;
        //pitch = cam.eulerAngles.x;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
            shootyStick.Shoot();

        ShowHitTime -= Time.deltaTime;

        if (track) {
            transform.eulerAngles = new Vector3(0, yaw, 0);
            Vector3 l = transform.localEulerAngles;
            l.x = 0;
            l.z = 0;
            transform.localEulerAngles = l;
        }

        //cam.eulerAngles = new Vector3(pitch, 0, 0);
        //l = cam.localEulerAngles;
        //l.y = 0;
        //l.z = 0;
        //cam.localEulerAngles = l;

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * sensitivity, 0));
        cam.Rotate(new Vector3(Input.GetAxis("Mouse Y") * sensitivity, 0, 0));
        
        yaw = transform.eulerAngles.y;
        //pitch = cam.eulerAngles.x;

        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            Destroy(shootyStick.gameObject);
            AssignGun(weapons[0]);
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            Destroy(shootyStick.gameObject);
            AssignGun(weapons[1]);
        }

        if (Input.GetMouseButtonDown(1)) {
            track = !track;
        }
    }
    void FixedUpdate()
    {

        transform.position += speed * (Input.GetAxis("HorizontalB") * transform.right + Input.GetAxis("VerticalB") * transform.forward);

        relPos = transform.localPosition - offset;
        relPos.x = Mathf.Clamp(relPos.x, xbounds.x, xbounds.y);
        relPos.y = 0;
        relPos.z = Mathf.Clamp(relPos.z, zbounds.x, zbounds.y);

        transform.localPosition = offset + relPos;
    }

    public void AssignGun(Gun g)
    {
        shootyStick = Instantiate(g);
        shootyStick.transform.SetParent(cam);
        Vector3 gunOffset = Vector3.down * 0.3f + Vector3.right * .25f;
        shootyStick.transform.localPosition = (gunOffset);
        if (g is RocketLauncher) {
            shootyStick.transform.localEulerAngles = Vector3.zero;
        } else {
            shootyStick.transform.localEulerAngles = new Vector3(0, 180, 0);
        }

    }

    void OnGUI()
    {
        Camera cam = GetComponentInChildren<Camera>();
        Vector3 center = cam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        GUI.DrawTexture(new Rect(center - new Vector3(128, 128), new Vector2(256, 256)), Crosshair);

        if (ShowHitTime > 0) {
            GUI.DrawTexture(new Rect(center - new Vector3(64, 64), new Vector2(128, 128)), Hit);
        }

        shootyStick.HudGUI();
    }
}
