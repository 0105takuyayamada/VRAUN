using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolObj<Bullet> {

    [SerializeField] GameObject impactPrefab, smokePrefab;
    Collider collider;
    float lifeTime;

    public override void Init()
    {
        gameObject.SetActive(true);
        if (collider) collider.enabled = false;
        lifeTime = 0;
    }
    public override void Sleep()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        collider = GetComponent<Collider>();
    }

    void Update () {
        if (lifeTime != 0) collider.enabled = true;
         lifeTime += Time.deltaTime;
        if (lifeTime >= 0.4f) Pool(gameObject.GetComponent<Bullet>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Destructable>().hp -= 100;
            Impact.SetOriginal(impactPrefab);
            Impact instant = Impact.Create();
            instant.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        }
        else
        {
            Smoke.SetOriginal(smokePrefab);
            Smoke instant = Smoke.Create();
            instant.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        Pool(gameObject.GetComponent<Bullet>());
    }
}
