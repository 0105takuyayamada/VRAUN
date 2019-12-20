using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChangeEnvironment : MonoBehaviour {

    [SerializeField] Vector3 highOrLow;
    [SerializeField] List<GameObject> activeObjectList, destroyObjectList;
    [SerializeField] Material skyboxMat;
    [SerializeField] Color fogColor;
    [SerializeField] float fogDensity;

    bool changed;

    public static void changeEnvironment(Material skyboxMat, Color fogColor, float fogDensity)
    {
        if (skyboxMat) RenderSettings.skybox = skyboxMat;
        if (fogColor != Color.black) RenderSettings.fogColor = fogColor;
        if (fogDensity != 0) RenderSettings.fogDensity = fogDensity;
    }

    void Update () {
		if (!changed && (
            highOrLow.x == 0 ||
            highOrLow.x > 0 && GameManager.player.transform.position.x > transform.position.x * highOrLow.x ||
            highOrLow.x < 0 && GameManager.player.transform.position.x < transform.position.x * highOrLow.x &&
            highOrLow.y == 0 ||
            highOrLow.y > 0 && GameManager.player.transform.position.y > transform.position.y * highOrLow.y ||
            highOrLow.y < 0 && GameManager.player.transform.position.y < transform.position.y * highOrLow.y &&
            highOrLow.z == 0 ||
            highOrLow.z > 0 && GameManager.player.transform.position.z > transform.position.z * highOrLow.z ||
            highOrLow.z < 0 && GameManager.player.transform.position.z < transform.position.z * highOrLow.z ))
        {
            changed = true;

            for (int i = 0; i < activeObjectList.Count; i++)
            {
                activeObjectList[i].SetActive(true);
            }

            for (int i = 0; i < destroyObjectList.Count; i++)
            {
                Destroy(destroyObjectList[i]);
            }

            changeEnvironment(skyboxMat, fogColor, fogDensity);
        }
	}
}
