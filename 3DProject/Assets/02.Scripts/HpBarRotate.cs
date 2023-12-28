using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarRotate : MonoBehaviour
{
    private Transform camTf;

    private void Awake()
    {
        camTf = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(transform.position + camTf.rotation * Vector3.forward, camTf.rotation * Vector3.up);
    }
}
