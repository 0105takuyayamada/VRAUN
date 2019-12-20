using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PotroBot : Destructable
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public int shootAmount;
    [SerializeField] public float shootRate, shootSpeed, shootRange;
    float shootCount, rotateCount;
    Quaternion startQuaternion;

    void FixedUpdate()
    {
        shootCount += Time.fixedDeltaTime;
        if (shootCount >= shootRate)
        {
            shootCount -= shootRate;
            //向きを保存
            startQuaternion = transform.rotation;

            if (Vector3.Distance(transform.position, GameManager.player.transform.position) < shootRange)
            {
                for (int i = 0; i < shootAmount; i++)
                {
                    //弾の生成・速度を渡す
                    GameObject instant = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    instant.GetComponent<PotroBullet>().speedRate = (1 / shootSpeed);
                    //ランダムな位置に弾を配置
                    instant.transform.position = transform.position + 
                        transform.right * Random.Range(0.25f, 1f) * (Random.Range(0, 2) * 2 - 1) + 
                        transform.up * Random.Range(0.25f, 1f);
                    //弾を見る方向を弾の向きにする
                    transform.LookAt(instant.transform.position);
                    instant.transform.rotation = transform.rotation;
                    instant.GetComponent<PotroBullet>().posB = transform.position + transform.forward * 50;
                    //弾を戻して動かす
                    instant.transform.position = transform.position;
                    instant.transform.DOMove(transform.position + transform.forward  * 5, 20 * (1/shootSpeed) ).SetEase(Ease.OutSine);
                }
                transform.rotation = startQuaternion;
            }
        }

        //回転
        transform.Rotate(0, 0, 180 * Time.fixedDeltaTime);

        DestroyConfirmation();
    }
}
