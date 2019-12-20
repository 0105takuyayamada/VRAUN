using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpact : PoolObj<EnemyImpact> {

    AudioSource audioSource;
    [SerializeField] List<GameObject> impacts;
    [SerializeField] List<AudioClip> audioClips;
    [SerializeField] float lifeTime;

    public override void Init()
    {
        while (impacts.Count > 1) {
            int a = Random.Range(0, impacts.Count);
            Destroy(impacts[a]);
            impacts.RemoveAt(a);
        }
        gameObject.SetActive(true);
        lifeTime = 0;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
        audioSource.pitch = Random.Range(0.8f, 1f);
        audioSource.Play();
    }
    public override void Sleep()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= 1) Pool(GetComponent<EnemyImpact>());
    }
}