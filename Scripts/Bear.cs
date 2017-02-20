using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bear : MonoBehaviour {


    public PlayerMovement player;
    Rigidbody2D rbody;
    CanvasToggle canvTog;
    Text ptext;
    public GameObject bear;
    public Collider2D bearcol;
    private bool trigger;

    // Use this for initialization
    void Start()
    {

        canvTog = GameObject.FindGameObjectWithTag("belowMsg").GetComponent<CanvasToggle>();
        ptext = GameObject.FindGameObjectWithTag("panelTxt").GetComponent<Text>();
        bear = GameObject.FindGameObjectWithTag("Bear");
        bearcol = GameObject.FindGameObjectWithTag("bearcol").GetComponent<PolygonCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if ((trigger) && (Input.GetKeyDown(KeyCode.K)) && (canvTog.teddyBear.isOwned == false))
        {
            canvTog.teddyBear.isOwned = true;
            bear.SetActive(false);
            bearcol.isTrigger = true;
            canvTog.Show();
            ptext.text = "You found a Teddy Bear. \n[ Space ] Continue";
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
