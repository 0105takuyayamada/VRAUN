using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultCanvas : MonoBehaviour {

    [SerializeField] Text scoreText, rankText;
    AudioSource audioSource;
    float count;
    int rankValue;

	void Start () {
        audioSource = GetComponent<AudioSource>();

        scoreText.text = "Kill" + System.Environment.NewLine +
            GameManager.kills.ToString() + System.Environment.NewLine +
            "Death" + System.Environment.NewLine +
            GameManager.deaths.ToString();
        rankText.text = "-";

    }
	
	void Update () {
        count += Time.deltaTime;

        switch (rankValue)
        {
            case 0:
                if (count >= 2)
                {
                    rankText.text = "E";
                    rankText.color = new Color(0.5f, 0.5f, 0.5f);
                    rankValue++;
                    audioSource.pitch = 0.8f;
                    audioSource.Play();
                }
                break;
            case 1:
                if (count >= 2.4f && GameManager.getStars >= 3)
                {
                    rankText.text = "D";
                    rankText.color = new Color(0.8125f, 0.8125f, 0.8125f);
                    rankValue++;
                    audioSource.pitch = 0.9f;
                    audioSource.Play();
                }
                break;
            case 2:
                if (count >= 2.8f && (GameManager.getStars >= 5 || GameManager.kills > 10))
                {
                    rankText.text = "C";
                    rankText.color = new Color(0, 0.25f, 1);
                    rankValue++;
                    audioSource.pitch = 1;
                    audioSource.Play();
                }
                break;
            case 3:
                if (count >= 3.2f && GameManager.getStars >= 7)
                {
                    rankText.text = "B";
                    rankText.color = new Color(1, 0.25f, 0);
                    rankValue++;
                    audioSource.pitch = 1.1f;
                    audioSource.Play();
                }
                break;
            case 4:
                if (count >= 3.8f && GameManager.deaths <= 15 &&
                    GameManager.kills > 15)
                {
                    rankText.text = "A";
                    rankText.color = new Color(0.25f, 1, 1);
                    rankValue++;
                    audioSource.pitch = 1.2f;
                    audioSource.Play();
                }
                break;
            case 5:
                if (count >= 4.6f && GameManager.deaths <= 5 &&
                    GameManager.kills > 25)
                {
                    rankText.text = "S";
                    rankText.color = new Color(1, 1, 0.25f);
                    rankValue++;
                    audioSource.pitch = 1.3f;
                    audioSource.Play();
                }
                break;
        }
    }
}
