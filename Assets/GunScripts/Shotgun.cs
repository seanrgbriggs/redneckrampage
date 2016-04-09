using UnityEngine;
using System.Collections;

public class Shotgun : Gun{


    public new void Start()
    {
        clipSize = 1;
        damage = 3;

        delay = 2;
        range = 400;

        spreadAngle = 30;

        base.Start(); 
    }

    public override void Shoot() {
        base.Shoot();
        for (int i = 0; i < 25; i++) {
            SingleShot(new Ray(transform.position, transform.forward));
        }
    }
}
