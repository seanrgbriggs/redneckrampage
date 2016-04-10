using UnityEngine;
using System.Collections;

public class RocketLauncher : Gun{

    public GameObject boomCapsule;

    public int clip;
    public float subDelay;

    public Texture2D AmmoImage;

    public override void Start()
    {
        damage = 50;
        delay = 7.5f;
        range = 500;
        spreadAngle = 15;

        clip = 3;
        subDelay = 0.2f;
        base.Start(); 
    }

    public override void Shoot() {
        if (clip <= 0)
        {
            if (curDelay > 0)
                return;
            curDelay = delay;
            clip = 3;
        }
        else if (curDelay == 0) {
            if (subDelay == 0) {
                base.Shoot();
                GameObject shot = (GameObject)Instantiate(boomCapsule, transform.position + transform.forward, Quaternion.identity);
                shot.transform.up = transform.forward;
                shot.GetComponent<Rigidbody>().velocity = shot.transform.up * 60;
                subDelay = 0.2f;
                clip--;
            }
        }
    }

    public override void Update()
    {
        base.Update();
        if (subDelay > 0)
            subDelay = Mathf.Max(subDelay - Time.deltaTime, 0);
    }

    public override void HudGUI() {
        if (curDelay <= 0) {
            float x = Screen.width / 2 + 50;
            for (int i = 0; i < clip; i++) {
                Vector3 pos = new Vector3(x + i * 50, 50);
                GUI.DrawTexture(new Rect(pos, new Vector2(32, 64)), AmmoImage);
            }
        }
    }
}
