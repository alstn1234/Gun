using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "DefaultPlayer")]
public class PlayerSO : ScriptableObject
{
    public int attack;
    public float attackspeed;
    public float speed;
    public float shotgunSpread;
    public int gold;
    public WeaponType[] ownedWeapon;
    public bool isReset;
}
