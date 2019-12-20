using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public enum gameType
    {
        PC,
        OcuGo,
        Vive,
        Android,
        AndroidOne
    }
    public static gameType gametype = gameType.Vive;

    public static Vector3 viveCameraRot, viveLLocalPos, viveRpos, viveLrot, viveRrot;
    public static int viveLtrigger, viveRtrigger;
    public static float viveLvive, viveRvive;

   void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) Application.Quit();

        //debug
        //if (Input.GetKey(KeyCode.J)) viveLtrigger++;
        //viveLpos += Vector3.up * Mathf.Sin(Time.time * 2) * 0.5f;
    }
}