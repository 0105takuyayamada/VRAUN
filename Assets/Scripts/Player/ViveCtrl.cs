using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveCtrl : MonoBehaviour {

    [SerializeField] bool isRight, isCamera;

    public Vector3 axis;

    void Update () {

        if (isCamera)   //カメラ   
        {
            Manager.viveCameraRot = new Vector3(0, transform.rotation.eulerAngles.y, 0);
        }
        else if (isRight)
        {   //右コントローラー
            var trackedObject = GetComponent<SteamVR_TrackedObject>();
            var device = SteamVR_Controller.Input((int)trackedObject.index);

            Manager.viveRpos = transform.position;
            Manager.viveRrot = transform.rotation.eulerAngles;
            if (Manager.viveRtrigger == -1) Manager.viveRtrigger = 0;
            if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger)) Manager.viveRtrigger++;
            else Manager.viveRtrigger = -1;

            if (Manager.viveLvive > 0)
            {
                Manager.viveLvive -= Time.deltaTime;
                device.TriggerHapticPulse((ushort)(Manager.viveLvive * 2000));
            }
        }
        else
        {   //左コントローラー
            var trackedObject = GetComponent<SteamVR_TrackedObject>();
            var device = SteamVR_Controller.Input((int)trackedObject.index);

            //パッド入力
            axis = device.GetAxis();

            Manager.viveLLocalPos = transform.localPosition;
            Manager.viveLrot = transform.rotation.eulerAngles;
            if (Manager.viveLtrigger == -1) Manager.viveLtrigger = 0;
            if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger)) Manager.viveLtrigger++;
            else Manager.viveLtrigger = -1;

            if (Manager.viveLvive > 0)
            {
                Manager.viveLvive -= Time.deltaTime;
                device.TriggerHapticPulse((ushort)(Manager.viveLvive * 2000));
            }
        }
    }
}