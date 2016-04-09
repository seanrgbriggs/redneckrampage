using UnityEngine;
using System.Collections;

public class Shotgun : Gun{


    public override void Start()
    {
        damage = 10;

        delay = 1.5f;
        range = 400;

        spreadAngle = 30;

        base.Start(); 
    }

    public override void Shoot() {
        if (curDelay > 0)
            return;
        curDelay = delay;
        base.Shoot();
        for (int i = 0; i < 25; i++) {
            SingleShot(new Ray(transform.position, transform.forward));
        }
    }
}
