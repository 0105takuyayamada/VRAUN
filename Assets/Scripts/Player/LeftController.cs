using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LeftController : MonoBehaviour {

    public Vector3 axis;

	void Update () {

        var trackedObject = GetComponent<SteamVR_TrackedObject>();
        axis = SteamVR_Controller.Input((int)trackedObject.index).GetAxis();
    }
}