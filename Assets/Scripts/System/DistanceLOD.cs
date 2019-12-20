using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceLOD : MonoBehaviour {

    List<Collider> list;

    private void Start()
    {
        foreach (GameObject obj in GetAllChildren.GetAll(gameObject))
        {
            if (obj.GetComponent<Collider>()) Destroy(obj.GetComponent<Collider>());
        }
    }
}
