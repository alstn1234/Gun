using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private void Start()
    {
        AutoDestroy(3f);
    }

    private void Update()
    {
        transform.position += new Vector3(0f, 0.1f, 0f);
    }

    IEnumerator AutoDestroy(float dealyTime)
    {
        yield return new WaitForSeconds(dealyTime);
        Destroy(gameObject);
    }
}
