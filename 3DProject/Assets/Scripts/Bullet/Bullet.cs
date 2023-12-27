using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

    const float BULLETSPEED = 50000f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    public void Shoot()
    {
        _rigidbody.AddForce(transform.forward * BULLETSPEED);
    }
    public void ShootShotgun(float shotgunSpread)
    {
        var pos = transform.forward;
        pos.x += Random.Range(-shotgunSpread, shotgunSpread) / 2;
        pos.y += Random.Range(-shotgunSpread, shotgunSpread) / 2;
        pos.z += Random.Range(-shotgunSpread, shotgunSpread) / 2;
        _rigidbody.AddForce(pos * BULLETSPEED);
    }

    IEnumerator AutoDestroy(float time = 3f)
    {
        yield return new WaitForSeconds(time);
        BulletDestroy();
    }

    private void BulletDestroy()
    {
        Destroy(gameObject);
    }
}
