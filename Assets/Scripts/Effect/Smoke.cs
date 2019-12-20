using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : PoolObj<Smoke>
{
    AudioSource audioSource;
    [SerializeField] List<GameObject> smokes;
    [SerializeField] List<AudioClip> audioClips;
    [SerializeField] float lifeTime;

    public override void Init()
    {
        while (smokes.Count > 1)
        {
            int a = Random.Range(0, smokes.Count);
            Destroy(smokes[a]);
            smokes.RemoveAt(a);
        }
        gameObject.SetActive(true);
        lifeTime = 0;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }
    public override void Sleep()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime >= 1) Pool(GetComponent<Smoke>());
    }
}