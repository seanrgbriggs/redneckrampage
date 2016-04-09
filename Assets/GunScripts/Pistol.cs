using UnityEngine;
using System.Collections;

public class Pistol : Gun{


    public override void Start()
    {
        clipSize = 2;
        damage = 5;
        delay = 1;
        range = 500;
        spreadAngle = 3;
        base.Start(); 
    }

    public override void Shoot() {
        base.Shoot();
        for (int i = 0; i < 3; i++) {
            SingleShot(new Ray(transform.position, transform.forward));
        }
    }
}
