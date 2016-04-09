using UnityEngine;
using System.Collections;
using EZObjectPools;
using EZEffects;

public abstract class Gun : MonoBehaviour {

    public GameObject gunner;

    public float delay;
    public float curDelay;

    public float damage;
    public float range;

    public float spreadAngle;
    
    public EffectImpact ImpactEffect;
    public EffectMuzzleFlash MuzzleEffect;
    public EffectTracer TracerEffect;
    public ParticleSystem CasingEffect;

    public AudioClip[] sounds;

    public virtual void Start(){
        gunner = GameObject.FindGameObjectWithTag("Gunner");
        curDelay = 0;
    }

    public void Update() {
        curDelay = Mathf.Max(0, curDelay - Time.deltaTime);
        CasingEffect.enableEmission = Input.GetButton("Fire1");
    }

    public virtual void Shoot() {
        if (curDelay > 0)
            return;
        curDelay = delay;
        if (sounds.Length > 0) AudioSource.PlayClipAtPoint(sounds[0], transform.position);
    }

    protected void SingleShot(Ray direction)
    {
        Transform cam = gunner.GetComponentInChildren<Camera>().transform.FindChild("GunBarrell");
        direction.direction = Quaternion.Euler(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0) * cam.forward;
        RaycastHit info = new RaycastHit();
        MuzzleEffect.ShowMuzzleEffect(cam, true, null);
        if (Physics.Raycast(direction, out info, range)) {
            GameObject target = info.transform.gameObject;

            TracerEffect.ShowTracerEffect(cam.position + direction.direction, direction.direction, info.distance);

            ImpactEffect.ShowImpactEffect(info.point, info.normal);

            if (target.GetComponent<DamageScript>() == null)
                return;
            if (target.CompareTag("Enemy"))
                target.GetComponent<DamageScript>().damage(damage);



        } else {

            TracerEffect.ShowTracerEffect(cam.position + direction.direction, direction.direction, 100);
        }

    }

}
