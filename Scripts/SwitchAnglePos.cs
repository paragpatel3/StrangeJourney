using UnityEngine;
using System.Collections;

public class SwitchAnglePos : MonoBehaviour {

    public Warp wrongPath;
    public Transform wrongTarget2;

    private void Start()
    {
        wrongPath = GameObject.FindGameObjectWithTag("WrongPath").GetComponent<Warp>();
        wrongTarget2 = GameObject.FindGameObjectWithTag("WrongTarget2").GetComponent<Transform>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player"))
        {
            wrongPath.target = wrongTarget2;
        }
    }


}
