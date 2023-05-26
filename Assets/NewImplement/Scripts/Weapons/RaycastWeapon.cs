using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public TrailRenderer tracer;
        public int bounce;
    }

    public ActiveWeapon.WEAPONSLOT weaponSlot;
    public bool isFiring = false;
    public int fireRate = 25;
    public float bulletSpeed = 1000.0f;
    public float bulletDrop = 0.0f;
    public int maxBounces = 0;
    public ParticleSystem[] muzzleFlash;
    public Transform rayCastOrigin;
    public Transform rayCastDestination;
    public ParticleSystem hitEffect;
    public TrailRenderer trailRenderer;
    public AnimationClip weaponAnimation;
    public WeaponRecoil recoil;
    public GameObject magazine;
    public string weaponName;

    public int ammoCount;
    public int clipSize;

    Ray ray;
    RaycastHit hitInfo;
    float accumulatedTime;
    List<Bullet> bullets = new List<Bullet>();
    float maxLifeTime = 3.0f;

    private void Awake()
    {
        recoil = GetComponent<WeaponRecoil>();
    }

    Vector3 GetPosition(Bullet bullet)
    {
        Vector3 gravity = Vector3.down * bulletDrop;
        return bullet.initialPosition + bullet.initialVelocity * bullet.time + 0.5f * gravity * bullet.time * bullet.time;
    }

    Bullet CreateBullet(Vector3 position, Vector3 velocity)
    {
        Bullet bullet = new Bullet();
        bullet.initialPosition = position;
        bullet.initialVelocity = velocity;
        bullet.time = 0.0f;
        bullet.tracer = Instantiate(trailRenderer, position, Quaternion.identity);
        bullet.tracer.AddPosition(ray.origin);
        bullet.bounce = maxBounces;
        return bullet;
    }

    public void UpdateBullet(float deltaTime)
    {
        SimulateBullets(deltaTime);
        DestroyBullets();
    }

    void SimulateBullets(float deltaTime)
    {
        bullets.ForEach(bullet =>
        {
            Vector3 p0 = GetPosition(bullet);
            bullet.time += deltaTime;
            Vector3 p1 = GetPosition(bullet);
            RaycastSegment(p0, p1, bullet);
        });
    }

    void DestroyBullets()
    {
        bullets.RemoveAll(bullet => bullet.time >= maxLifeTime);
    }

    void RaycastSegment(Vector3 start, Vector3 end, Bullet bullet)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        ray.origin = start;
        ray.direction = direction;
        if (Physics.Raycast(ray, out hitInfo, distance))
        {
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1);
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);

            bullet.tracer.transform.position = hitInfo.point;
            bullet.time = maxLifeTime;

            // bullet ricochet
            if(bullet.bounce > 0)
            {
                bullet.time = 0;
                bullet.initialPosition = hitInfo.point;
                bullet.initialVelocity = Vector3.Reflect(bullet.initialVelocity, hitInfo.normal);
                bullet.bounce--;
            }

            // Collision impulse
            var rb2d = hitInfo.collider.GetComponent<Rigidbody>();
            if(rb2d)
            {
                rb2d.AddForceAtPosition(ray.direction * 20, hitInfo.point, ForceMode.Impulse);
            }

            if (hitInfo.transform.gameObject.GetComponent<IHealth>() != null)
            {
                hitInfo.transform.gameObject.GetComponent<IHealth>().OnDamage(1, (hitInfo.transform.position - rayCastOrigin.position).normalized * 10);
            }
        }
        else
        {
            if(bullet.tracer)
                bullet.tracer.transform.position = end;
        }
    }

    public void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 0.0f;
        recoil.Reset();
        FireBullet();
        //foreach(var particle in muzzleFlash)
        //    particle.Emit(1);

        //ray.origin = rayCastOrigin.position;
        //ray.direction = rayCastDestination.position - rayCastOrigin.position;

        //var tracer = Instantiate(trailRenderer, ray.origin, Quaternion.identity);
        //tracer.AddPosition(ray.origin);

        //if(Physics.Raycast(ray, out hitInfo))
        //{
        //    //Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1);
        //    hitEffect.transform.position = hitInfo.point;
        //    hitEffect.transform.forward = hitInfo.normal;
        //    hitEffect.Emit(1);

        //    tracer.transform.position = hitInfo.point;

        //    if(hitInfo.transform.gameObject.GetComponent<IHealth>() != null)
        //    {
        //        hitInfo.transform.gameObject.GetComponent<IHealth>().OnDamage(1, (hitInfo.transform.position - rayCastOrigin.position).normalized * 10);
        //    }
        //}
    }

    private void FireBullet()
    {
        if (ammoCount <= 0) return;
        ammoCount--;

        foreach (var particle in muzzleFlash)
            particle.Emit(1);

        Vector3 velocity = (rayCastDestination.position - rayCastOrigin.position).normalized * bulletSpeed;
        var bullet = CreateBullet(rayCastOrigin.position, velocity);
        bullets.Add(bullet);

        recoil.GenerateRecoil(weaponName);
    }

    public void UpdateFiring(float deltaTime)
    {
        accumulatedTime += Time.deltaTime;
        float fireInterval = 1.0f / fireRate;
        while(accumulatedTime >= 0.0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        }
    }

    public void UpdateWeapon(float deltaTime)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartFiring();
        }
        UpdateBullet(deltaTime);
        if (isFiring)
        {
            UpdateFiring(deltaTime);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopFiring();
        }
    }
    
    public void StopFiring()
    {
        isFiring = false;
    }
}
