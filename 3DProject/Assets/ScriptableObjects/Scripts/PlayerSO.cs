using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "DefaultPlayer")]
public class PlayerSO : ScriptableObject
{
    public int attack;
    public float attackSpeed;
    public float speed;
    public float runSpeed;
    public float shotgunSpread;
    public int gold;
    public List<WeaponType> ownedWeapon;
    public bool isReset;
}
