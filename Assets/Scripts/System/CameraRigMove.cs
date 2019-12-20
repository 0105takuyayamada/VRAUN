using UnityEngine;

public class CameraRigMove : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) transform.position += Vector3.up * 0.1f;
        if (Input.GetKeyDown(KeyCode.Q)) transform.position += Vector3.down * 0.1f;
        if (Input.GetKeyDown(KeyCode.W)) transform.position += Vector3.forward * 0.1f;
        if (Input.GetKeyDown(KeyCode.A)) transform.position += Vector3.left * 0.1f;
        if (Input.GetKeyDown(KeyCode.S)) transform.position += Vector3.back * 0.1f;
        if (Input.GetKeyDown(KeyCode.D)) transform.position += Vector3.right * 0.1f;
    }
}
