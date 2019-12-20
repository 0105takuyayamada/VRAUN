using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultStar : MonoBehaviour {

    [SerializeField] float myNumber;

    void Start () {
        if (GameManager.getStars < myNumber) Destroy(gameObject);
	}
	
	void Update () {
        transform.Rotate(
            Time.deltaTime * (180 + myNumber * 25.71429f),
            Time.deltaTime * (myNumber * 25.71429f),
            Time.deltaTime * (180 + myNumber * 25.71429f)
            );
	}
}