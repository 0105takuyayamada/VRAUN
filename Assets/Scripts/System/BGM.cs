using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour {

    public static List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField] AudioClip loop;
    [SerializeField] List<AudioClip> layers;
    public static int layerCount = -1;


    void Start () {
        for (int i = 0; i < 2; i++)
        {
            audioSources.Add(gameObject.AddComponent<AudioSource>());
            audioSources[i].playOnAwake = false;
            audioSources[i].volume = 0.5f;
        }
        audioSources[0].loop = true;
        audioSources[0].clip = loop;
        audioSources[0].Play();
    }

    void Update()
    {
        switch (layerCount)
        {
            case 0:
                if (GameManager.player.transform.position.z >= 2.5f)
                {
                    audioSources[0].clip = layers[layerCount];
                    layerCount++;
                }
                break;
            case 1:
                if (GameManager.player.transform.position.z >= 65f &&
                    GameManager.player.transform.position.y >= 10)
                {
                    audioSources[0].clip = layers[layerCount];
                    layerCount++;
                }
                break;
            case 2:
                if (GameManager.player.transform.position.z >= 110)
                {
                    audioSources[0].clip = layers[layerCount];
                    layerCount++;
                }
                break;
        }
    }
}
