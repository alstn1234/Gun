using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigidbody;

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
        _rigidbody.AddForce(transform.forward * GameManager.instance.CurrentWeapon.shotSpeed);
    }
    public void ShootShotgun(float shotgunSpread)
    {
        var pos = transform.forward;
        var sperad = GameManager.instance.playerCurrentStats.shotgunSpread;
        pos.x += Random.Range(-shotgunSpread, shotgunSpread) / sperad;
        pos.y += Random.Range(-shotgunSpread, shotgunSpread) / sperad;
        pos.z += Random.Range(-shotgunSpread, shotgunSpread) / sperad;
        _rigidbody.AddForce(pos * GameManager.instance.CurrentWeapon.shotSpeed);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            StartCoroutine(other.GetComponent<Monster>().TakeDamage(GameManager.instance.GetAttack()));
            BulletDestroy();
        }
    }
}
