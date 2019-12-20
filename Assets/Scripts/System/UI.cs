using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    [SerializeField] Image damagePanel, blackPanel, whitePanel;
    float lateAct, goal;

	void Update () {

        if (GameManager.playerPlayer.hp != 100 ||
            GameManager.toRespawnCount != 0 ||
            GameManager.toResultCount != 0) {
            lateAct = 0.1f;
        }

        if (lateAct > 0) {
            lateAct -= Time.deltaTime;

            damagePanel.color = new Color(1, 0, 0.25f, 0.5f - GameManager.playerPlayer.hp * 0.01f * 0.5f);
            blackPanel.color = new Color(0, 0, 0, GameManager.toRespawnCount);
            whitePanel.color = new Color(1, 1, 1, GameManager.toResultCount * 0.5f);

            if (GameManager.toResultCount >= 1.5f)
            {
                whitePanel.color = new Color(1, 1, 1, 1 - ((GameManager.toResultCount - 1.5f) * 2));
            }
            /*
            switch (Manager.gametype)
            {
                case Manager.gameType.PC:
                    damagePanelPC.color = new Color(1, 0, 0.25f, 0.5f - GameManager.playerPlayer.hp * 0.01f * 0.5f);
                    blackPanelPC.color = new Color(0, 0, 0, GameManager.toRespawnCount);
                    whitePanelPC.color = new Color(1, 1, 1, GameManager.toResultCount * 0.5f);

                    break;
                case Manager.gameType.Vive:
                    damagePanelVive.color = new Color(1, 0, 0.25f, 0.5f - GameManager.playerPlayer.hp * 0.01f * 0.5f);
                    blackPanelVive.color = new Color(0, 0, 0, GameManager.toRespawnCount);
                    if (GameManager.toResultCount <= 1.9f)
                        whitePanelVive.color = new Color(1, 1, 1, Mathf.Min(GameManager.toResultCount * 0.75f, 1));
                    else
                        whitePanelVive.color = new Color(1, 1, 1, 1 - (GameManager.toResultCount - 1.9f) * 10);
                    break;
            }
            // */
        }
    }
}
