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

    [Header("PopUp")]
    public GameObject gameOverPopup;

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
            playerCurrentStats.attackSpeed = playerbaseStats.attackSpeed;
            playerCurrentStats.speed = playerbaseStats.speed;
            playerCurrentStats.runSpeed = playerbaseStats.runSpeed;
            playerCurrentStats.shotgunSpread = playerbaseStats.shotgunSpread;
            playerCurrentStats.gold = playerbaseStats.gold;
            playerCurrentStats.ownedWeapon = playerbaseStats.ownedWeapon.ToList();
        }
        CurrentWeapon = weaponList[(int)WeaponType.Pistol];
        SetWeapon();
    }

    public void ChangeWeapon(WeaponType weaponType)
    {
        CurrentWeapon = weaponList[(int)weaponType];
        SetWeapon();
    }

    public float GetAttackSpeed()
    {
        return CurrentWeapon.attackspeed / (1 + playerCurrentStats.attackSpeed / 10);
    }
    
    public int GetAttack()
    {
        return CurrentWeapon.attack + CurrentWeapon.attack * playerCurrentStats.attack / 10;
    }

    public void SetWeapon()
    {
        foreach (Transform child in weaponPivot.transform)
        {
            child.gameObject.SetActive(false);
            weaponPivot.transform.GetChild((int)CurrentWeapon.weaponType).gameObject.SetActive(true);
        }
        PlayerController.instance.ChangeAnimator();
    }

    public void Upgrade(PlayerSO playerSO)
    {
        playerCurrentStats.attack += playerSO.attack;
        playerCurrentStats.attackSpeed += playerSO.attackSpeed;
        playerCurrentStats.speed += playerSO.speed;
        playerCurrentStats.runSpeed += playerSO.speed;
        playerCurrentStats.shotgunSpread += playerSO.shotgunSpread;
        UpdateStatsText();
    }

    public void UpdateStatsText()
    {
        StatsUI.instance.UpdateText();
    }

    public void GameOver()
    {
        // 게임오버
        Time.timeScale = 0f;
        PlayerController.instance.SetCursor(false);
        gameOverPopup.SetActive(true);
    }
}
