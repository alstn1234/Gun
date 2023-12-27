using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI spreadText;

    public GameObject statsPopup;

    private void FixedUpdate()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        attackText.text = GameManager.instance.playerCurrentStats.attack + "(+" + GameManager.instance.CurrentWeapon.attack + ")";
        attackSpeedText.text = GameManager.instance.playerCurrentStats.attackspeed + "(+" + GameManager.instance.CurrentWeapon.attackspeed + ")";
        moveSpeedText.text = GameManager.instance.playerCurrentStats.speed.ToString();
        spreadText.text = GameManager.instance.playerCurrentStats.shotgunSpread.ToString();
    }

    public void interact()
    {
        statsPopup.SetActive(true);
    }

    public void StatsPopupClose()
    {
        statsPopup.SetActive(false);
        PlayerController.instance.SetCursor(true);
    }

    public void ChangeWeapon(int weaponIdx)
    {
        GameManager.instance.ChangeWeapon((WeaponType)weaponIdx);
    }
}
