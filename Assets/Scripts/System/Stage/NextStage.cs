using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour {

    [SerializeField] List<Transform> respownPoses;

	void Update () {
        if (Input.GetKeyDown(KeyCode.N))
        {
            for (int i = 0; i < respownPoses.Count - 1; i++)
            {
                if (GameManager.respawnPoint == respownPoses[i].position)
                {
                    GameManager.playerPlayer.hp = 0;
                    GameManager.respawnPoint = respownPoses[i + 1].position;
                    break;
                }
            }
        }
	}
}
