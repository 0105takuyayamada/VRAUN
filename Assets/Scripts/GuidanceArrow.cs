using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GuidanceArrow : MonoBehaviour {

    [SerializeField]
    private int count;
    [SerializeField]
    private List<Transform> respawnses;

    [SerializeField]
    private Transform here, arrow;
    Vector3 hereVector;

    [SerializeField]
    private AudioSource audioSource;

    void Start () {
        arrow.DOLocalRotate(Vector3.up * 360, 1, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        arrow.DOLocalMoveY(2, 1.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
	}
	
	void Update() {
        if (count >= respawnses.Count)
        {
            //次のリスポン地点がなければ破壊
            DOTween.To(() => audioSource.volume, x => audioSource.volume = x, 0, 0.5f);
            transform.DOScale(0.01f, 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                Destroy(gameObject);
            });
            return;
        }
        hereVector = new Vector3(here.position.x - GameManager.player.transform.position.x, 0, here.position.z - GameManager.player.transform.position.z);
        here.rotation = Quaternion.FromToRotation(Vector3.forward, hereVector);

        if (GameManager.respawnPoint == respawnses[count].position)
        {
            count++;
            //次のリスポン地点へ移動
            if (count < respawnses.Count) transform.DOMove(respawnses[count].position, 2).SetEase(Ease.InOutSine);
        }
    }
}
