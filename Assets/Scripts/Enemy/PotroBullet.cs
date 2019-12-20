using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotroBullet : Destructable
{

    [SerializeField] GameObject impactPrefab;
    public Vector3 startPos, posB, posC;
    public float lifeTime, speedRate;
    bool setStartPos;

    void Update () {

        lifeTime += Time.deltaTime;

        if (lifeTime >= 20 * speedRate)
        {
            if (!setStartPos) {
                setStartPos = true;
                startPos = transform.position;
                posC = GameManager.player.transform.position;
            }

            float tmp = 1 - Mathf.Sin( Mathf.PI * 0.5f + (lifeTime - (20 * speedRate)) * 20 * speedRate * Mathf.PI * 0.5f);

            transform.position =
                Vector3.Lerp(Vector3.Lerp(
                startPos, posB, tmp
                ),
                posC, tmp
                );

            if (tmp >= 1) {
                EnemyImpact.SetOriginal(impactPrefab);
                EnemyImpact instant = EnemyImpact.Create();
                instant.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() &&
            GameManager.playerPlayer.hp > 0 &&
            GameManager.playerPlayer.fear <= 0)
        {
            GameManager.playerPlayer.hp -= 75;
            GameManager.playerPlayer.fear = 1;

            EnemyImpact.SetOriginal(impactPrefab);
            EnemyImpact instant = EnemyImpact.Create();
            instant.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            Destroy(gameObject);
        }
    }
}
