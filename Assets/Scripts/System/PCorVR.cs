using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCorVR : MonoBehaviour {

    [SerializeField] Manager.gameType gametype;
    [SerializeField] bool startActive, isDestroy;
    [SerializeField] List<GameObject> ActiveObjectList;

    void Awake () {

        if (gametype != Manager.gametype && isDestroy) Destroy(gameObject);

        if (gametype == Manager.gametype)
        {
            for (int i = 0; i < ActiveObjectList.Count; i++)
            {
                ActiveObjectList[i].SetActive(true);
            }
        }
    }
}