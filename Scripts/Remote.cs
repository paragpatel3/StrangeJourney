using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Remote : MonoBehaviour {

    public PlayerMovement player;
    Rigidbody2D rbody;
    CanvasToggle canvTog;
    Text ptext;
    public GameObject bear;
    private bool trigger;

    // Use this for initialization
    void Start()
    {
        canvTog = GameObject.FindGameObjectWithTag("belowMsg").GetComponent<CanvasToggle>();
        ptext = GameObject.FindGameObjectWithTag("panelTxt").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((trigger) && (Input.GetKeyDown(KeyCode.K)) && (canvTog.remote.isOwned == false))
        {
            canvTog.remote.isOwned = true;
            canvTog.Show();
            ptext.text = "You found a Remote. \n[ Space ] Continue";
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trigger = true;
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trigger = false;
        }
    }
}
