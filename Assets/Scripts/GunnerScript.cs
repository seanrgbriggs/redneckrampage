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
 
    public float speed = 0.05f;
    public float sensitivity = 500000f;
    public float ShowHitTime;

    public Texture2D Crosshair;
    public Texture2D Hit;
    
    private Vector3 forward;

    private bool track = true;

    // Use this for initialization
    void Start()
    {
        transform.localPosition = offset;
         AssignGun(weapons[curWeap]);

        forward = transform.forward;
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
            shootyStick.Shoot();

        ShowHitTime -= Time.deltaTime;

        if (track) {
            transform.forward = forward;
            Vector3 l = transform.localEulerAngles;
            l.x = 0;
            l.z = 0;
            transform.localEulerAngles = l;
        }
        
  
        forward = transform.forward;

        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            if (curWeap < weapons.Length - 1) {
                curWeap++;
                AssignGun(weapons[curWeap]);
            }
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            if (curWeap > 0) {
                curWeap--;
                AssignGun(weapons[curWeap]);
            }
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
        /*shootyStick = Instantiate(g);
        shootyStick.transform.SetParent(cam);
        Vector3 gunOffset = Vector3.down * 0.3f + Vector3.right * .25f;
        shootyStick.transform.localPosition = (gunOffset);
        if (g is RocketLauncher) {
            shootyStick.transform.localEulerAngles = Vector3.zero;
        } else {
            shootyStick.transform.localEulerAngles = new Vector3(0, 180, 0);
        }*/

        
        if (shootyStick != null) {
            shootyStick.gameObject.SetActive(false);
        }
        g.gameObject.SetActive(true);
        shootyStick = g;

    }

}
