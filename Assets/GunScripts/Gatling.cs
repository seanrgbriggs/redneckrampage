﻿using UnityEngine;
using System.Collections;

public class Gatling : Gun
{

    // Use this for initialization
    public override void Start()
    {
        damage = 5;
        delay = 0.1f;
        range = 700;
        spreadAngle = 11;
        base.Start();
    }

    public override void Shoot()
    {
        if (curDelay > 0)
            return;
        curDelay = delay;
        base.Shoot();
        for (int i = 0; i < 8; i++)
        {
            SingleShot(new Ray(transform.position, transform.forward));
        }
    }

}
