using UnityEngine;
using DG.Tweening;

public class SphereBot : Destructable
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float shootRate, shootRateRandom, shootSpeed, shootRange, moveTime, moveTimeRandom;
    float shootCount, moveCount;
    Vector3 startPos, nextPos;
    [SerializeField] Vector3 rangeVector;


    void Start()
    {
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        shootCount += Time.fixedDeltaTime;
        if (shootCount >= shootRate)
        {
            shootCount -= shootRate;

            if (Vector3.Distance(transform.position, GameManager.player.transform.position) < shootRange)
            {
                GameObject instant = Instantiate(bulletPrefab, transform.position + transform.forward * 4, transform.rotation);
                instant.GetComponent<Rigidbody>().velocity = transform.forward * shootSpeed;
            }
        }
    }

    void Update()
    {
        moveCount -= Time.deltaTime;
        if (moveCount <= 0)
        {
            moveCount += moveTime + Random.Range(0, moveTimeRandom);
            nextPos = startPos + new Vector3(
                rangeVector.x * Random.Range(0, 1f),
                rangeVector.y * Random.Range(0, 1f),
                rangeVector.z * Random.Range(0, 1f))
                - rangeVector * 0.5f;

           transform.DOMove(nextPos, moveCount).SetEase(Ease.InOutSine);
        }
        transform.LookAt(GameManager.player.transform.position + Vector3.up * 0.5f);

        DestroyConfirmation();
    }
}