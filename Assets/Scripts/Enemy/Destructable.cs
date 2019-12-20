using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

    [SerializeField] public float hp;
    [SerializeField] List<GameObject> set;
    [SerializeField] bool isEnemy, isTarget;

    public void DestroyConfirmation()
    {
        if (hp <= 0)
        {
            if (isEnemy)
            {
                GameManager.kills++;
            }
            if (isTarget) GameManager.targets++;
            Destroy(gameObject);

            for (int i = 0; i < set.Count; i++) {
                Destroy(set[0].gameObject);
            }
        }
    }
}