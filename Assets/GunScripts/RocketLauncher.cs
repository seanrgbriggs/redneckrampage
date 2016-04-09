using UnityEngine;
using System.Collections;

public class RocketLauncher : Gun{

    public GameObject boomCapsule;

    public int clip;
    public float subDelay;

    public override void Start()
    {
        damage = 50;
        delay = 15;
        range = 500;
        spreadAngle = 15;

        clip = 3;
        subDelay = 0.5f;
        base.Start(); 
    }

    public override void Shoot() {
        if (clip <= 0)
        {
            base.Shoot();
            clip = 3;
        }
        else {
            if (subDelay == 0)
            {
                Instantiate(boomCapsule, transform.position + transform.forward, Quaternion.identity);
                subDelay = 0.5f;
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
}
