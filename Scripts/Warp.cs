using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {


    public Transform target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = target.position;
        Camera.main.transform.position = target.position;
    }



}
