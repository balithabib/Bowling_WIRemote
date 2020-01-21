using UnityEngine;
using System.Collections;

public class ResetBall : MonoBehaviour {

    public Vector3 _Position;


    // initialisation 
    void OnEnable()
    {
        _Position = gameObject.transform.position;
    }

    public void Reset(int _ball)
    {
        // reinitialiser ballon
        StartCoroutine(DelayBallReset(2));
    }

    IEnumerator DelayBallReset(float secs)
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.transform.position = _Position;

        yield return new WaitForSeconds(secs);
        gameObject.GetComponent<Renderer>().enabled = true;

    }
}
