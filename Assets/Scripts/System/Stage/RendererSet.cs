using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererSet : MonoBehaviour {

    void Start()
    {
        List<GameObject> list = GetAllChildren.GetAll(gameObject);
        foreach (GameObject obj in list)
        {
            Destroy(obj.GetComponent<MeshFilter>());
            Destroy(obj.GetComponent<Renderer>());
        }
    }
}