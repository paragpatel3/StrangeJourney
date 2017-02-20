using UnityEngine;
using System.Collections;

public class SwitchAnglePos2 : MonoBehaviour {

    public Warp wrongPath;
    public Transform wrongTarget;

    private void Start()
    {
        wrongPath = GameObject.FindGameObjectWithTag("WrongPath").GetComponent<Warp>();
        wrongTarget = GameObject.FindGameObjectWithTag("WrongTarget").GetComponent<Transform>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player"))
        {
            wrongPath.target = wrongTarget;
        }
    }

}
