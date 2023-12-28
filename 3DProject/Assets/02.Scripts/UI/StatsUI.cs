using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public static StatsUI instance;

    [Header("Text")]
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI spreadText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI weaponPriceText;

    [Header("PopUp")]
    public GameObject statsPopup;
    public GameObject weaponBuyWindow;
    public GameObject buyFailWindow;

    [Header("Button")]
    public Button attackUpButton;
    public Button attackSpeedUpButton;
    public Button speedUpButton;
    public Button spreadUpButton;

    private const int attackUpgradeMaxCount = 10;
    private const int attackSpeedUpgradeMaxCount = 10;
    private const int speedUpgradeMaxCount = 10;
    private const int spreadUpgradeMaxCount = 1;

    private WeaponType clickWeapon;

    private PlayerSO playerSO;

    private int attackUpgradeCount;
    private int attackSpeedUpgradeCount;
    private int speedUpgradeCount;
    private int spreadUpgradeCount;

    private int UpgradePrice;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        playerSO = ScriptableObject.CreateInstance<PlayerSO>();
    }

    public void UpdateText()
    {
        attackText.text = GameManager.instance.CurrentWeapon.attack + "(+" + GameManager.instance.playerCurrentStats.attack * 10 + "%)";
        attackSpeedText.text = GameManager.instance.CurrentWeapon.attackspeed + "(+" + GameManager.instance.playerCurrentStats.attackSpeed * 10 + "%)";
        moveSpeedText.text = GameManager.instance.playerCurrentStats.speed.ToString();
        spreadText.text = GameManager.instance.playerCurrentStats.shotgunSpread.ToString();
        goldText.text = GameManager.instance.playerCurrentStats.gold.ToString();
    }

    public void interact()
    {
        statsPopup.SetActive(true);
        UpdateText();
    }

    public void StatsPopupClose()
    {
        statsPopup.SetActive(false);
        PlayerController.instance.SetCursor(true);
    }

    public void ChangeWeapon(int weaponIdx)
    {
        clickWeapon = (WeaponType)weaponIdx;
        // 가지고 있는 무기중에 클릭한 무기가 없는 경우
        if (!GameManager.instance.playerCurrentStats.ownedWeapon.Contains(clickWeapon))
        {
            weaponPriceText.text = GameManager.instance.weaponList[(int)clickWeapon].price.ToString();
            weaponBuyWindow.SetActive(true);
            return;
        }
        GameManager.instance.ChangeWeapon(clickWeapon);
        AudioManager.instance.PlaySFX("Swap");
    }
    public void InitSO()
    {
        playerSO.attack = 0;
        playerSO.attackSpeed = 0;
        playerSO.speed = 0;
        playerSO.shotgunSpread = 0;
    }

    public bool GoldCheck(int upgradeCount)
    {
        UpgradePrice = 100 + upgradeCount;
        if (GameManager.instance.playerCurrentStats.gold < UpgradePrice) return false;
        GameManager.instance.playerCurrentStats.gold -= UpgradePrice;
        return true;
    }

    public void BuyWeaponButton()
    {
        weaponBuyWindow.SetActive(false);
        if (GameManager.instance.playerCurrentStats.gold < GameManager.instance.weaponList[(int)clickWeapon].price)
        {
            buyFailWindow.gameObject.SetActive(true);
            return;
        }
        GameManager.instance.playerCurrentStats.ownedWeapon.Add(clickWeapon);
        GameManager.instance.ChangeWeapon(clickWeapon);
        AudioManager.instance.PlaySFX("Swap");
    }

    public void NoBuyWeaponButton()
    {
        weaponBuyWindow.SetActive(false);
    }

    #region Upgrade
    public void AttackUpgrade()
    {
        if (!GoldCheck(attackUpgradeCount)) return;

        InitSO();
        playerSO.attack = 1;
        GameManager.instance.Upgrade(playerSO);

        attackUpgradeCount++;

        if (attackUpgradeCount == attackUpgradeMaxCount)
            attackUpButton.interactable = false;
    }

    public void AttackSpeedUpgrade()
    {
        if (!GoldCheck(attackSpeedUpgradeCount)) return;

        InitSO();
        playerSO.attackSpeed = 1;
        GameManager.instance.Upgrade(playerSO);

        attackSpeedUpgradeCount++;

        if (attackSpeedUpgradeCount == attackSpeedUpgradeMaxCount)
            attackSpeedUpButton.interactable = false;
    }

    public void SpeedUpgrade()
    {
        if (!GoldCheck(speedUpgradeCount)) return;

        InitSO();
        playerSO.speed = 1;
        GameManager.instance.Upgrade(playerSO);

        speedUpgradeCount++;

        if (speedUpgradeCount == speedUpgradeMaxCount)
            speedUpButton.interactable = false;
    }

    public void SpreadUpgrade()
    {
        if (!GoldCheck(spreadUpgradeCount)) return;

        InitSO();
        playerSO.shotgunSpread = 1;
        GameManager.instance.Upgrade(playerSO);

        spreadUpgradeCount++;

        if (spreadUpgradeCount == spreadUpgradeMaxCount)
            spreadUpButton.interactable = false;
    }
    #endregion
    
}
