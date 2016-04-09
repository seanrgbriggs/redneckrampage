using UnityEngine;
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

    AudioSource[] sounds;

    public virtual void Start(){
        gunner = GameObject.FindGameObjectWithTag("Gunner");
        ammo = clipSize;
        curDelay = 0;
        sounds = GetComponents<AudioSource>();
    }

    public void Update() {
        curDelay = Mathf.Max(0, curDelay - Time.deltaTime);
    }

    public virtual void Shoot() {
        if (ammo <= 0) {
            Reload();
            return;
        }
        if (curDelay > 0)
            return;
        ammo--;
        curDelay = delay;
        if (sounds[0] != null) sounds[0].Play();
    }

    protected void SingleShot(Ray direction)
    {
        direction.direction = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0) * gunner.GetComponent<GunnerScript>().cam.forward;
        RaycastHit info = new RaycastHit();
        if (Physics.Raycast(direction, out info, range))
        {
            GameObject target = info.transform.gameObject;

            if (target.GetComponent<DamageScript>()==null)
                return;
            if (target.CompareTag("Enemy"))
                target.GetComponent<DamageScript>().damage(damage);

        }

    }


    public void Reload()
    {
        ammo = clipSize;
    }
}
