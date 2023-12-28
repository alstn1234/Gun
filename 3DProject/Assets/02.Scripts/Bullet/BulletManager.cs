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
    public GameObject _bulletPos;

    [Header("Stats")]
    public float shotgunSpread = 0.001f;

    const int ShotgunBulletCount = 9;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void Shoot()
    {
        GameObject bulletObj = CreateBullet();
        bulletObj.GetComponent<Bullet>().Shoot();
        switch (GameManager.instance.CurrentWeapon.weaponType)
        {
            case WeaponType.Rifle:
                AudioManager.instance.PlaySFX("RifleAttack");
                break;
            case WeaponType.Pistol:
                AudioManager.instance.PlaySFX("PistolAttack");
                break;
        }
    }

    public void ShotgunShoot()
    {
        for (int i = 0; i < ShotgunBulletCount; i++)
        {
            GameObject bulletObj = CreateBullet();

            bulletObj.GetComponent<Bullet>().ShootShotgun(shotgunSpread);
            AudioManager.instance.PlaySFX("ShotgunAttack");
        }

    }

    public GameObject CreateBullet()
    {
        int weaponIndex = (int)GameManager.instance.CurrentWeapon.weaponType;
        GameObject bulletObj = Instantiate(bulletPrefab, _bulletPos.transform.position, _bulletPos.transform.rotation);
        return bulletObj;
    }
}
