using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

    public Transform target;
    Camera mycam;

	// Use this for initialization
	void Start () {

        mycam = GetComponent<Camera>();

	}
	

	// Update is called once per frame
	void Update () {

        mycam.orthographicSize = (Screen.height / 100f) / 3f;

        if (target)
        {
            //linearly interpolate 0-1 (from, to, how fast 10%)
            transform.position = Vector3.Lerp(transform.position, target.position, 0.1f) + new Vector3(0, 0, -10);
        }

	}
}
