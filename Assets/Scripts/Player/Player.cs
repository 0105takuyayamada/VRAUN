using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    const float speed = 15, jumpHeight = 15;

    [SerializeField] public GameObject wind;
    [SerializeField] Camera_ PCcamera;
    [SerializeField] ViveCtrl leftCtrl;
    Rigidbody _rigidbody;
    AudioSource audioSource;
    bool isground, onground;
    float inputHorizontal, inputVertical;
    public int jumpCount;
    public float hp, fear;
    Vector3 normalVector, inputVelocity, holdVelocity,
        oldCtrlLpos, oldPos;

    RaycastHit hit;

    void Start()
    {
        normalVector = Vector3.zero;
        _rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        hp = 100;
    }

    void FixedUpdate()
    {
        switch (Manager.gametype)
        {
            case Manager.gameType.PC:
                inputHorizontal = Input.GetAxis("Horizontal");
                inputVertical = Input.GetAxis("Vertical");
                break;
            case Manager.gameType.Vive:
                inputHorizontal = leftCtrl.axis.x;
                inputVertical = leftCtrl.axis.y;
                /*
                inputVertical = 0;
                if (Mathf.Abs(Manager.viveLLocalPos.y - oldCtrlLpos.y) >= 0.5f * Time.fixedDeltaTime)
                {
                    inputVertical = Mathf.Abs(Manager.viveLLocalPos.y - oldCtrlLpos.y) * 0.5f / Time.fixedDeltaTime;
                }
                oldCtrlLpos = Manager.viveLLocalPos;
                // */
                break;
        }
    
        if (hp > 0)
        {
            //入力値を移動方向に変換
            inputVelocity = Vector3.zero;
            switch (Manager.gametype)
            {
                case Manager.gameType.PC:
                    inputVelocity += PCcamera.horRot.forward * inputVertical;
                    inputVelocity += PCcamera.horRot.right * inputHorizontal;
                    
                    break;
                case Manager.gameType.Vive:
                    inputVelocity += Quaternion.Euler(Manager.viveCameraRot) * Vector3.forward * inputVertical;
                    inputVelocity += Quaternion.Euler(Manager.viveCameraRot) * Vector3.right * inputHorizontal;
                    break;
            }
                        

            //足元確認
            if (Physics.Raycast(
                transform.position + Vector3.down * 0.9f,
                Vector3.down, out hit, 0.25f, LayerMask.GetMask(new string[] { "Default" })) &&
                hit.normal.y > 0.9f)
            {
                isground = true;
                jumpCount = 3;
                normalVector = hit.normal;
                //地面に吸着
                _rigidbody.velocity += normalVector * Time.fixedDeltaTime * -24.5f;
            }
            else
            {
                isground = false;
                if (jumpCount == 3) jumpCount = 2;
                normalVector = Vector3.up;
            }

            //地面に吸着
            _rigidbody.velocity += normalVector * Time.fixedDeltaTime * -24.5f;
            //地上or空中で設置していない時　移動
            if (isground || !onground)
                _rigidbody.velocity += new Vector3(inputVelocity.x, inputVelocity.y, inputVelocity.z) * Time.fixedDeltaTime * speed * 60;

            //移動制限
            if (new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z).magnitude >= 10)
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z).normalized * 10 + Vector3.up * _rigidbody.velocity.y;

            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, Mathf.Clamp(_rigidbody.velocity.y, -25, 50), _rigidbody.velocity.z);

            //空中停止した時の救済
            if (jumpCount == 0 && transform.position.y == oldPos.y)
                jumpCount++;
        }
        oldPos = transform.position;
    }

    void Update()
    {
        if (hp > 0)
        {
            //ジャンプ
            switch (Manager.gametype)
            {
                case Manager.gameType.PC:
                    if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
                    {
                        jumpCount--;
                        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, jumpHeight, _rigidbody.velocity.z);
                        audioSource.pitch = Random.Range(0.9f, 1.1f);
                        audioSource.Play();
                    }
                    break;
                case Manager.gameType.Vive:
                    if (Manager.viveLtrigger == 1 && jumpCount > 0)
                    {
                        jumpCount--;
                        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, jumpHeight, _rigidbody.velocity.z);
                        //_rigidbody.velocity = Quaternion.Euler(Manager.viveLrot) * Vector3.forward * (jumpHeight + 15);
                        audioSource.pitch = Random.Range(0.9f, 1.1f);
                        audioSource.Play();
                    }
                    break;
            }
        }

        //HP　無敵時間
        if (transform.position.y < 0) hp = 0;
        if (fear > 0) fear -= Time.deltaTime;
        else
        {
            if (hp >= 100) hp = 100;
            else if (hp > 0) hp += Time.deltaTime * 10;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            onground = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        onground = false;
    }
}
