using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jumper : MonoBehaviour {

    Vector3 startPos, nextPos, posB, posC;
    AudioSource audioSource, CAS, windAS;
    [SerializeField] Material changeSkybox;
    [SerializeField] float changeSkyboxTime;

    [SerializeField] AudioClip introClip, loopClip;
    [SerializeField] AudioClip startFloat, startExplosion, landingExplosion;
    [SerializeField] Transform transB, transC;
    [SerializeField] float floatTime, jumpTime, loadScenePoint, unLoadScenePoint;
    [SerializeField] GameObject line, Orbit;
    [SerializeField] string loadSceneName, unLoadSceneName;
    [SerializeField] List<float> windVolumeList, windVolumeTimingList;
    float count, floatTimeDiv, jumpTimeDiv, obitCount;
    int intCount;
    bool isStartCoroutine, isStartCoroutineUnLoad;

    //
    GameObject oldObject;

	void Start () {
        posB = transB.transform.position;
        posC = transC.transform.position;
        audioSource = GetComponent<AudioSource>();
        CAS = transC.gameObject.transform.GetChild(0).GetComponent<AudioSource>();

        floatTimeDiv = (1 / floatTime);
        jumpTimeDiv = (1 / jumpTime);

        //
        oldObject = gameObject;
    }
	
	void LateUpdate () {
		if (count > 0) {
            GameManager.player.transform.position = nextPos;

            //プレイヤージャンプ
            if (count < floatTime) {    //浮遊

                if (!audioSource.isPlaying)
                {
                    audioSource.clip = startFloat;
                    audioSource.Play((ulong)Time.deltaTime);
                }
                nextPos =
                    Vector3.Lerp(startPos, transform.position + Vector3.up * 5, Mathf.Sin( count * floatTimeDiv * 1.570796f));
            }
            else if (count - floatTime <= jumpTime)
            {
                if (!GameManager.isPlayerSuperJumping)
                {
                    BGM.audioSources[1].clip = introClip;
                    BGM.audioSources[1].Play();
                    StartCoroutine("LoadScene");
                }

                GameManager.isPlayerSuperJumping = true;

                nextPos =
                    Vector3.Lerp( Vector3.Lerp(
                        transform.position + Vector3.up * 5, posB, (count - floatTime) * jumpTimeDiv
                        ),
                        posC, (count - floatTime) * jumpTimeDiv
                        );
                
                //
                obitCount++;

                if (!windAS.isPlaying) windAS.Play();
                audioSource.clip = startExplosion;
                audioSource.Play((ulong)Time.deltaTime);

                for (int i = 1; i < windVolumeTimingList.Count; i++)
                {
                    if (windVolumeTimingList[i] > count - floatTime)
                    {
                        windAS.volume = Mathf.Lerp(
                            windVolumeList[i - 1],
                            windVolumeList[i],
                            (count - floatTime - windVolumeTimingList[i - 1]) * (1 / (windVolumeTimingList[i] - windVolumeTimingList[i - 1]))
                            );
                        break;
                    }
                }

                //SkyBox切替
                if ((count - floatTime) * jumpTimeDiv > changeSkyboxTime)
                    ChangeEnvironment.changeEnvironment(changeSkybox, new Color(0, 0.625f, 0.5f), 0.002f);

                GameManager.playerPlayer.wind.transform.localPosition = (GameManager.player.transform.position - nextPos).normalized;
            }

            /*
            if (intCount < count)
            {
                intCount++;
                GameObject instant = Instantiate(Orbit, GameManager.player.transform.position + Vector3.up, Quaternion.identity);
                instant.GetComponent<Renderer>().material.color = Color.red;
                instant.transform.localScale = Vector3.one * 5;
                instant.gameObject.name = intCount.ToString();
                instant.transform.LookAt(oldObject.transform.position);
                oldObject = instant;
            }

            if (obitCount > 10)
            {
                obitCount = 0;
                GameObject instant = Instantiate(Orbit, GameManager.player.transform.position + Vector3.up,
                    Quaternion.Euler((line.transform.position - GameManager.player.transform.position).normalized) );
                instant.transform.localScale = Vector3.one * 5;
            }
            // */

            //一秒先表示
            if ((count + 1) < floatTime) {      //浮遊
                line.transform.position =
                    Vector3.Lerp(startPos, transform.position + Vector3.up * 5, Mathf.Sin((count + 1) * floatTimeDiv * 1.570796f));
            }
            else if ((count + 1) <= jumpTime)
            {
                line.transform.position =
                    Vector3.Lerp( Vector3.Lerp(
                        transform.position + Vector3.up * 5, posB, (count + 1) * jumpTimeDiv
                        ),
                        posC, (count + 1) * jumpTimeDiv
                        );
            }

            count += Time.deltaTime;

            //ジャンプ完了
            if (count - floatTime > jumpTime)
            {
                count = 0;
                GameManager.isPlayerSuperJumping = false;
                GameManager.player.GetComponent<Rigidbody>().isKinematic = false;
                GameManager.playerPlayer.fear = 0;
                GameManager.playerPlayer.hp = 100;
                line.SetActive(false);
                Destroy(transform.parent.gameObject, 2);
                windAS.Stop();
                CAS.Play((ulong)Time.deltaTime);

                BGM.audioSources[0].clip = loopClip;
                BGM.audioSources[0].volume = 0.5f;
                BGM.audioSources[0].Play();
            }

            //シーン切り替え
            if (!isStartCoroutine && (count - floatTime) * (1/jumpTime) >= loadScenePoint)
            {
                isStartCoroutine = true;
            }

            if (!isStartCoroutineUnLoad && (count - floatTime) * (1 / jumpTime) >= unLoadScenePoint)
            {
                isStartCoroutineUnLoad = true;
                StartCoroutine("unLoadScene");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            windAS = GameManager.playerPlayer.wind.GetComponent<AudioSource>();

            startPos = other.transform.position;
            nextPos = startPos;
            GameManager.player.GetComponent<Rigidbody>().isKinematic = true;
            GameManager.playerPlayer.fear = jumpTime;
            GameManager.playerPlayer.hp = 100;
            line.SetActive(true);
            count += Time.deltaTime;

            BGM.audioSources[0].Stop();
        }
    }

    IEnumerator LoadScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Additive);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f && isStartCoroutine)
        {
            yield return null;
        }
        async.allowSceneActivation = true;
        GameManager.areaName = loadSceneName;
    }

    IEnumerator unLoadScene()
    {
        DontDestroyOnLoad(GameManager.player);
        AsyncOperation async = SceneManager.UnloadSceneAsync(unLoadSceneName);

        while (async.progress < 0.9f)
        {
            yield return null;
        }
    }
}
