using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour {

    public enum naviCount {
        def,
        firstBlock,
        walk,
        jump
    }

    public naviCount count;
    [SerializeField] GameObject firstBlock;

	void Start () {
        count = naviCount.firstBlock;

    }
	
	void Update () {
		switch (count)
        {
            case naviCount.def:
                //ジャンプの説明開始
                if (GameManager.player.transform.position.z > 62) count = naviCount.jump;
                break;
            case naviCount.firstBlock:
                if (!firstBlock) count = naviCount.walk;
                break;
        }
	}
}
