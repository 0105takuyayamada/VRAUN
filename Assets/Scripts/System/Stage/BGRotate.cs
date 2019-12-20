using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRotate : MonoBehaviour {

    [SerializeField] Vector3 rotate;

	void Update () {
        transform.Rotate(rotate * Time.deltaTime);
	}
}
