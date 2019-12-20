using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : PoolObj<EnemyBullet>
{
    [SerializeField] GameObject impactPrefab;
    float lifeTime;
    public Rigidbody rb;
    public Mesh mesh;
    public Material material;

    public override void Init()
    {
        gameObject.SetActive(true);
        lifeTime = 0;
    }
    public override void Sleep()
    {
        gameObject.SetActive(false);

    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mesh = GetComponent<MeshFilter>().mesh;
        material = GetComponent<Material>();
    }

    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= 4) Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() &&
            GameManager.playerPlayer.hp > 0 &&
            GameManager.playerPlayer.fear <= 0)
        {
            GameManager.playerPlayer.hp -= 75;
            GameManager.playerPlayer.fear = 1;
            Manager.viveRvive = 0.25f;
            Manager.viveLvive = 0.25f;
        }

        EnemyImpact.SetOriginal(impactPrefab);
        EnemyImpact instant = EnemyImpact.Create();
        instant.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        Destroy(this.gameObject);
    }
}
