using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Android : MonoBehaviour {

    public PlayerMovement player;
    Rigidbody2D rbody;
    CanvasToggle canvTog;
    Text ptext;
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
        if ((trigger) && (Input.GetKeyDown(KeyCode.K)) && (canvTog.satisfied == true) && (canvTog.teddyBear.isOwned == true) && (canvTog.usb.isOwned == false))
        {
            canvTog.Show();
            ptext.text = "Android: ''Thank you for returning my property. You may find this useful.''\nYou traded the Teddy Bear for a USB Stick.\n[ Space ] Continue";
            canvTog.usb.isOwned = true;
            canvTog.teddyBear.isOwned = false;
        }
        else if ((trigger) && (Input.GetKeyDown(KeyCode.K)) && (canvTog.satisfied == false) && (canvTog.teddyBear.isOwned == true))
        {
            canvTog.Show();
            ptext.text = "Android: ''This is my property, but not in the condition I need. Please help restore its original condition.''\n[ Space ] Continue";
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
