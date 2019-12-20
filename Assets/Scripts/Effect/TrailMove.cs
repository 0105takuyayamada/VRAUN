using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMove : MonoBehaviour {

    float waitCount, startMoveCount = 24;
    Vector3 startPos, moveVector;
    [SerializeField] Vector3 RangeVector;

	void Start () {
        GetComponent<TrailRenderer>().enabled = false;

        startPos = transform.localPosition;
        transform.localPosition = startPos + new Vector3(
            Random.Range(-RangeVector.x, RangeVector.x),
            Random.Range(-RangeVector.y, RangeVector.y),
            Random.Range(-RangeVector.z, RangeVector.z));

        GetComponent<TrailRenderer>().enabled = true;
    }

	void Update () {

        if (startMoveCount > 0)
        {
            waitCount = Random.Range(0.5f, 2);
            startMoveCount -= waitCount;

            switch (Random.Range(0, 3))
            {
                case 0: moveVector = Vector3.right * Random.Range(25, 75) * (Random.Range(0, 2) * 2 - 1); break;
                case 1: moveVector = Vector3.up * Random.Range(25, 75) * (Random.Range(0, 2) * 2 - 1); break;
                case 2: moveVector = Vector3.forward * Random.Range(25, 75) * (Random.Range(0, 2) * 2 - 1); break;
            }

            transform.localPosition = new Vector3(
                Mathf.Clamp(transform.localPosition.x + moveVector.x * waitCount, startPos.x - RangeVector.x, startPos.x + RangeVector.x),
                Mathf.Clamp(transform.localPosition.y + moveVector.y * waitCount, startPos.y - RangeVector.y, startPos.y + RangeVector.y),
                Mathf.Clamp(transform.localPosition.z + moveVector.z * waitCount, startPos.z - RangeVector.z, startPos.z + RangeVector.z));
        }
        else
        {
            waitCount -= Time.deltaTime;

            if (waitCount <= 0)
            {
                waitCount = Random.Range(0.5f, 2);

                switch (Random.Range(0, 3))
                {
                    case 0: moveVector = Vector3.right * Random.Range(25, 75) * (Random.Range(0, 2) * 2 - 1); break;
                    case 1: moveVector = Vector3.up * Random.Range(25, 75) * (Random.Range(0, 2) * 2 - 1); break;
                    case 2: moveVector = Vector3.forward * Random.Range(25, 75) * (Random.Range(0, 2) * 2 - 1); break;
                }
            }

            transform.localPosition = new Vector3(
                Mathf.Clamp(transform.localPosition.x + moveVector.x * Time.deltaTime, startPos.x - RangeVector.x, startPos.x + RangeVector.x),
                Mathf.Clamp(transform.localPosition.y + moveVector.y * Time.deltaTime, startPos.y - RangeVector.y, startPos.y + RangeVector.y),
                Mathf.Clamp(transform.localPosition.z + moveVector.z * Time.deltaTime, startPos.z - RangeVector.z, startPos.z + RangeVector.z));
        }
	}
}
