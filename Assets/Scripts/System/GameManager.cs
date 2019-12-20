using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] public static GameObject player;
    [SerializeField]
    private List<GameObject> reticlesTemp;
    public static List<GameObject> reticles;
    public static string areaName = "Area1";
    public static GameObject _camera;
    public static Player playerPlayer;
    public static Vector3 aimPos, respawnPoint;
    public static float startCount, toRespawnCount, toResultCount, toResultOrder,
        deaths = 0, kills = 0, targets, getStars;
    public static bool isPlayerSuperJumping;
    public static float playerShotSpeed = 80, playerShotRapid = 0.25f, playerShotAccuracy = 0.3f;
    //120,1,0

    void Awake()
    {
        if (SceneManager.sceneCount < 2) SceneManager.LoadScene("Area1", LoadSceneMode.Additive);
        player = GameObject.Find("Player");
        playerPlayer = player.GetComponent<Player>();
        respawnPoint = Vector3.zero;
        reticles = reticlesTemp;
    }

    void Update () {

        //debug
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerPlayer.hp = 0;
        }
        if (Input.GetKey(KeyCode.I))
        {
            playerPlayer.hp = 100;
            playerPlayer.fear = 2;
        }

        if (startCount < 2) startCount += Time.deltaTime;

        //リスポンから復活
        if (toRespawnCount > 1)
        {
            toRespawnCount = 0;
            deaths++;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.transform.position = respawnPoint + Vector3.up;
            playerPlayer.hp = 100;
            playerPlayer.fear = 2;
        }
        if (toRespawnCount > 0) toRespawnCount += Time.deltaTime;

        //ゴールへ移動
        if (toResultOrder == 0 && toResultCount > 1.5f)
        {
            toResultOrder = 1;
            SceneManager.LoadSceneAsync("Result", LoadSceneMode.Additive);
            player.transform.position = Vector3.up;
            SceneManager.UnloadSceneAsync(areaName);
        }
        if (toResultCount > 0)
        {
            player.GetComponent<Rigidbody>().isKinematic = true;
            toResultCount += Time.deltaTime;
            BGM.audioSources[0].volume -= Time.deltaTime * 0.25f;
            BGM.audioSources[1].volume -= Time.deltaTime * 0.25f;
        }

        //死亡時
        if (playerPlayer.hp <= 0) {
            toRespawnCount += Time.deltaTime;
        }
    }

    public static void PowerUpSpeed(float value)
    {
        playerShotSpeed = value;

        for (int i = 0; i < reticles.Count; i++)
        {
            reticles[i].transform.localPosition = new Vector3(
                reticles[i].transform.localPosition.x,
                reticles[i].transform.localPosition.y,
                0.4f * value
                );
        }
    }

    public static void PowerUpRapid(float value)
    {
        playerShotRapid = value;
    }

    public static void PowerUpAccuracy(float value)
    {
        playerShotAccuracy = value;

        for (int i = 0; i < reticles.Count; i++)
        {
            reticles[i].transform.localPosition = new Vector3(
                reticles[i].transform.localPosition.x == 0 ? 0 : Mathf.Sign(reticles[i].transform.localPosition.x) * value * 2.5f,
                reticles[i].transform.localPosition.y == 0 ? 0 : Mathf.Sign(reticles[i].transform.localPosition.y) * value * 2.5f,
                reticles[i].transform.localPosition.z
                );
        }
    }
}