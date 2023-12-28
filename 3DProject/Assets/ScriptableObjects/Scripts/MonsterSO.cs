using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MonsterSO", menuName = "DefaultMonster")]
public class MonsterSO : ScriptableObject
{
    public int health;
    public float speed;
    public int gold;
}
