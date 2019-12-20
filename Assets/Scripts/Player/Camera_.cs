using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_ : MonoBehaviour {

    Camera camera;
    [SerializeField] GameObject pointer;
    [SerializeField]
    public Transform verRot, horRot, defaultPointerPos;
    int flipX = 1, flipY = 1;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameManager._camera = gameObject;
        camera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        float X_Rotation = Input.GetAxis("Mouse X") * flipX;
        float Y_Rotation = Input.GetAxis("Mouse Y") * flipY;
        verRot.transform.Rotate(-Y_Rotation * 1.75f, 0, 0);
        horRot.transform.Rotate(0, X_Rotation * 1.75f, 0);
        //transform.RotateAround(verRot.transform.position, transform.right, Y_Rotation * 1.75f);

        switch (Manager.gametype)
        {
            case Manager.gameType.PC:
                Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 1);
                Vector3 targetPos = camera.ScreenPointToRay(center).direction * 500;

                Ray ray = new Ray(GameManager.player.transform.position + Vector3.up * 1.6f, targetPos);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 500, LayerMask.GetMask(new string[] { "Default", "Enemy" }))) {
                    GameManager.aimPos = hit.point;
                    pointer.transform.position = GameManager.aimPos;
                }
                else
                {
                    GameManager.aimPos = transform.position + camera.ScreenPointToRay(center).direction * 50;
                    pointer.transform.position = GameManager.aimPos;
                }

                if (Input.GetKeyDown(KeyCode.X)) flipX -= flipX * 2;
                if (Input.GetKeyDown(KeyCode.Z)) flipY -= flipY * 2;
                break;
            case Manager.gameType.OcuGo:

                break;
            case Manager.gameType.Vive:

                break;
        }

    }
}