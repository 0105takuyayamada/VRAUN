using UnityEngine;

public class MoveBlock : MonoBehaviour {

    [SerializeField] Vector3 endPos;
    [SerializeField] float loopTime;

    float loopCount;
    Rigidbody rigidbody;
    Vector3 startPos;

    void Start ()
    {
        startPos = transform.position;
        endPos = startPos + endPos;
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {

        loopCount += Time.fixedDeltaTime;
        if (loopCount > loopTime * 2) loopCount -= loopTime * 2;

        if (loopCount < loopTime)
            rigidbody.MovePosition(Vector3.Lerp(endPos, startPos, (loopTime - loopCount) * (1 / loopTime)) );
        else
            rigidbody.MovePosition(Vector3.Lerp(startPos, endPos, (loopTime * 2 - loopCount) * (1 / loopTime)) );
    }
}