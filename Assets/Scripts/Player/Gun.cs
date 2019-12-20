using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    float shootCount, temp;
    [SerializeField] GameObject pointer;
    public GameObject bulletPrefab;
    AudioSource audioSource;
    [SerializeField] List<AudioClip> shootAudios;

    Rigidbody playerRb;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerRb = GameManager.player.GetComponent<Rigidbody>();

        switch (Manager.gametype)
        {
            case Manager.gameType.Vive:
                pointer.transform.localScale = Vector3.one * 0.2f;
                break;
        }
    }

        void FixedUpdate () {
        switch (Manager.gametype)
        {
            case Manager.gameType.PC:

                transform.LookAt(GameManager.aimPos);

                if (Input.GetMouseButton(0))
                {
                    shootCount += GameManager.playerShotRapid;
                    if (shootCount == 2 ||
                        shootCount == 4)
                    {
                        if (shootCount == 4) shootCount = 0;
                        audioSource.clip = shootAudios[Random.Range(0, shootAudios.Count)];
                        audioSource.pitch = Random.Range(0.9f, 1.1f);
                        audioSource.Play();

                        Bullet.SetOriginal(bulletPrefab);
                        Bullet instant = Bullet.Create();
                        instant.transform.position =
                            transform.position + (transform.forward * 0.75f) + (transform.forward * (shootCount - 2) * 0.5f) * 1.2f + 
                            transform.up * Random.Range(-GameManager.playerShotAccuracy, GameManager.playerShotAccuracy) +
                            transform.right * Random.Range(-GameManager.playerShotAccuracy, GameManager.playerShotAccuracy);
                        instant.transform.LookAt(GameManager.aimPos);
                        instant.GetComponent<Rigidbody>().velocity = transform.forward * GameManager.playerShotSpeed + playerRb.velocity;
                    }
                }
                else
                {
                    shootCount = 0;
                }
                break;
            case Manager.gameType.OcuGo:

                break;
            case Manager.gameType.Vive:
                if (Manager.viveRtrigger > 0)
                {
                    shootCount += GameManager.playerShotRapid;
                    if (shootCount == 2 ||
                        shootCount == 4)
                    {
                        if (shootCount == 4) shootCount = 0;
                        audioSource.clip = shootAudios[Random.Range(0, shootAudios.Count)];
                        audioSource.pitch = Random.Range(0.9f, 1.1f);
                        audioSource.Play();

                        Bullet.SetOriginal(bulletPrefab);
                        Bullet instant = Bullet.Create();
                        instant.transform.position =
                            transform.position + (transform.forward * 0.75f) + (transform.forward * (shootCount - 2) * 0.5f) * 1.2f +
                            transform.up * Random.Range(-GameManager.playerShotAccuracy, GameManager.playerShotAccuracy) +
                            transform.right * Random.Range(-GameManager.playerShotAccuracy, GameManager.playerShotAccuracy);
                        instant.transform.rotation = transform.rotation;
                        instant.GetComponent<Rigidbody>().velocity = transform.forward * GameManager.playerShotSpeed + playerRb.velocity;
                    }
                }
                else
                {
                    shootCount = 0;
                }
                break;
        }
    }
    private void LateUpdate()
    {
        transform.position = Manager.viveRpos;
        transform.rotation = Quaternion.Euler(Manager.viveRrot);
    }

    /*
    void LateUpdate()
    {
        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();

        switch (Manager.gametype)
        {
            case Manager.gameType.PC:

                ray = new Ray(transform.position, GameManager.aimPos - transform.position);
                if (Physics.Raycast(ray, out hit, GameManager.playerShotSpeed * 0.4f, LayerMask.GetMask(new string[] { "Default", "Enemy" })))
                    pointer.transform.position = hit.point;
                else pointer.transform.position = GameManager.aimPos;

                if (GameManager.isPlayerSuperJumping) pointer.SetActive(false);
                else pointer.SetActive(true);
                break;

            case Manager.gameType.Vive:

                transform.position = Manager.viveRpos;
                transform.rotation = Quaternion.Euler(Manager.viveRrot);

                ray = new Ray(transform.position, transform.position + transform.forward);
                if (Physics.Raycast(ray, out hit, GameManager.playerShotSpeed * 0.4f, LayerMask.GetMask(new string[] { "Default", "Enemy" })))
                    pointer.transform.position = hit.point;
                else pointer.transform.position = transform.position + transform.forward * 50;

                if (GameManager.isPlayerSuperJumping) pointer.SetActive(false);
                else pointer.SetActive(true);
                break;
        }
    }
    // */
}
