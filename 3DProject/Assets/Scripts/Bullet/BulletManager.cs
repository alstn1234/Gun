using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager instance;
    [Header("Object")]
    public GameObject bulletPrefab;
    public GameObject[] _bulletPos;

    [Header("Stats")]
    public float speed = 10000f;
    public float shotgunSpread = 0.001f;

    const int ShotgunBulletCount = 9;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
    }

    public void Shoot()
    {
        GameObject bulletObj = CreateBullet();

        bulletObj.GetComponent<Bullet>().Shoot();
    }

    public void ShotgunShoot()
    {
        for (int i = 0; i < ShotgunBulletCount; i++)
        {
            GameObject bulletObj = CreateBullet();

            bulletObj.GetComponent<Bullet>().ShootShotgun(shotgunSpread);
        }

    }

    public GameObject CreateBullet()
    {
        int weaponIndex = (int)GameManager.instance.CurrentWeapon.weaponType;
        GameObject bulletObj = Instantiate(bulletPrefab, _bulletPos[weaponIndex].transform.position, _bulletPos[weaponIndex].transform.rotation);
        return bulletObj;
    }
}
