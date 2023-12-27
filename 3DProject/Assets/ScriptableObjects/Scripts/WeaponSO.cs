using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponSO", menuName ="DefaultWeapon")]

public class WeaponSO : ScriptableObject
{
    public WeaponType weaponType;
    public float attackspeed;
    public int attack;
    public int price;

}
