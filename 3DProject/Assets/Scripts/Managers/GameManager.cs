using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Stats")]
    public PlayerSO playerbaseStats;
    public PlayerSO playerCurrentStats;

    [Header("Weapon")]
    public WeaponSO[] weaponList;
    public GameObject weaponPivot;
    [HideInInspector]
    public WeaponSO CurrentWeapon;
    public Dictionary<WeaponType, bool> ownedWeapon;


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (playerbaseStats.isReset)
        {
            playerCurrentStats.attack = playerbaseStats.attack;
            playerCurrentStats.attackspeed = playerbaseStats.attackspeed;
            playerCurrentStats.speed = playerbaseStats.speed;
            playerCurrentStats.shotgunSpread = playerbaseStats.shotgunSpread;
            playerCurrentStats.ownedWeapon = playerbaseStats.ownedWeapon;
        }
        CurrentWeapon = weaponList[(int)WeaponType.Rifle];
    }

    public void ChangeWeapon(WeaponType weaponType)
    {
        CurrentWeapon = weaponList[(int)weaponType];
        foreach (Transform child in weaponPivot.transform)
        {
            child.gameObject.SetActive(false);
        }
        weaponPivot.transform.GetChild((int)weaponType).gameObject.SetActive(true);
        PlayerController.instance.ChangeAnimator();
    }

    public float GetAttackSpeed()
    {
        return CurrentWeapon.attackspeed + CurrentWeapon.attackspeed * playerCurrentStats.attackspeed / 10;
    }
}
