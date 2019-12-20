using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    Vector3 center = Vector3.forward * 50;
    float rotateSpeed;

    void Start()
    {
        rotateSpeed = (Random.Range(0, 2) * 2 - 1) * Random.Range(0.1f, 2f);
    }

    void FixedUpdate()
    {
        transform.RotateAround(center, Vector3.up, rotateSpeed * 0.02f);
    }
}