﻿using UnityEngine;
using System.Collections;

public abstract class Gun : MonoBehaviour {

    public GameObject gunner;

    public int clipSize;
    public int ammo;

    public float delay;
    public float curDelay;

    public float damage;
    public float range;

    public float spreadAngle;

    public void Start(){
        gunner = GameObject.FindGameObjectWithTag("Gunner");
        ammo = clipSize;
        curDelay = 0;
    }

    public void Update() {
        curDelay = Mathf.Max(0, curDelay - Time.deltaTime);
    }

    public void Shoot() {
        if (ammo <= 0) {
            Reload();
            return;
        }
        if (curDelay > 0)
            return;
        ammo--;
    }

    protected void SingleShot(Ray direction)
    {
        direction.direction = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0) * direction.direction;
        RaycastHit info = new RaycastHit();
        if (Physics.Raycast(direction, out info, range))
        {
            GameObject target = info.transform.gameObject;
            if (target.GetComponent<DamageScript>()==null)
                return;
            float rangemult = 1 - Mathf.Pow(((info.distance) / range),2);
            if (target.CompareTag("Enemy"))
                target.GetComponent<DamageScript>().damage(damage * rangemult);

        }

    }


    public void Reload()
    {
        ammo = clipSize;
    }
}
