using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Monster")]
    public GameObject[] monsterPrefabs;
    public MonsterSO baseMonsterSO;

    [Header("Spawn")]
    public GameObject[] monsterSpawnPostions;

    [Header("Timer")]
    public TextMeshProUGUI timeText;

    private int monsterSpawnPosCount;
    private int monsterCount;
    private float currentTime;
    private float lastSpawnTime;
    private float spawnIntervalTime = 3f;
    private float defaultSpawnIntervalTime = 3f;

    private int currentHealth;
    private float currentspeed;
    private int currentgold;

    private void Awake()
    {
        monsterCount = monsterPrefabs.Length;
        monsterSpawnPosCount = monsterSpawnPostions.Length;
    }

    private void Update()
    {
        timeText.text = currentTime.ToString("N2");
        currentTime += Time.deltaTime;
        if(currentTime - lastSpawnTime >= spawnIntervalTime)
        {
            lastSpawnTime = currentTime;
            UpdateGameSettings();
            int monsterIndex = Random.Range(0, monsterCount);
            int monsterSpawnPosIndex = Random.Range(0, monsterSpawnPosCount);

            GameObject monsterObj = Instantiate(monsterPrefabs[monsterIndex]);
            monsterObj.transform.position = monsterSpawnPostions[monsterSpawnPosIndex].transform.position;

            monsterObj.GetComponent<Monster>().SetStat(currentHealth, currentspeed, currentgold);

        }
    }

    private void UpdateGameSettings()
    {
        currentHealth = Mathf.Clamp(baseMonsterSO.health + ((int)currentTime / 10), 10, 100);
        currentspeed = Mathf.Clamp(baseMonsterSO.speed + (currentTime / 20), 10f, 50f);
        currentgold = Mathf.Clamp(baseMonsterSO.gold + ((int)currentTime / 5), 10, 30);
        spawnIntervalTime = Mathf.Clamp(defaultSpawnIntervalTime - (currentTime / 20) * 0.2f, 0.2f, 3f);
    }


}
