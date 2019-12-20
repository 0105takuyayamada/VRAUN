using UnityEngine;

public class CubeBot : Destructable {

    [SerializeField] Mesh bulletMesh;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float shootRate, shootSpeed, shootRange;
    [SerializeField] bool isLock;
    float shootCount;

	void FixedUpdate () {
        if (isLock) transform.LookAt(GameManager.player.transform.position + Vector3.up * 0.5f);

        shootCount += Time.fixedDeltaTime;
        if (shootCount >= shootRate)
        {
            shootCount -= shootRate;

            if (Vector3.Distance(transform.position, GameManager.player.transform.position) < shootRange)
            {
                EnemyBullet.SetOriginal(bulletPrefab);
                EnemyBullet instant = EnemyBullet.Create();
                instant.mesh = bulletMesh;
                instant.transform.position = transform.position + transform.forward * 2 + transform.up;
                instant.transform.rotation = transform.rotation;
                instant.rb.velocity = transform.forward * shootSpeed;
            }
        }

        DestroyConfirmation();
    }
}