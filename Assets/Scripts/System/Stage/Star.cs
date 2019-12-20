using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    [SerializeField] GameObject effect;
    [SerializeField] bool speed, rapid, accuracy;
    [SerializeField] float value;
    [SerializeField] bool goal, candidateGoal;

    void Update () {
        transform.Rotate(0, 270 * Time.deltaTime, 0);

        if (candidateGoal && 
            Input.GetKey(KeyCode.G))
        {
            transform.localScale = Vector3.one * 10;
            GetComponent<SphereCollider>().radius = 0.75f;
            goal = true;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (speed) GameManager.PowerUpSpeed(value);
            else if (rapid) GameManager.PowerUpRapid(value);
            else GameManager.PowerUpAccuracy(value);

            Instantiate(effect, transform.position, Quaternion.identity);

            if (goal)
            {
                GameManager.toResultCount += Time.fixedDeltaTime;
                GameManager.playerPlayer.fear = 2;
            }

            GameManager.getStars++;
            Destroy(gameObject);
        }
    }
}