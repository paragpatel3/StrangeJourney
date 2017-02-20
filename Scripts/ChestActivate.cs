using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChestActivate : MonoBehaviour {

    CanvasToggle canvTog;
    Text ptext;
    PlayerMovement player;
    private bool trigger;

    // Use this for initialization
    void Start () {

        canvTog = GameObject.FindGameObjectWithTag("belowMsg").GetComponent<CanvasToggle>();
        ptext = GameObject.FindGameObjectWithTag("panelTxt").GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }
	
	// Update is called once per frame
	void Update () {

        if ((trigger) && (Input.GetKeyDown(KeyCode.K)) && (canvTog.laser.isOwned == false))
        {
            canvTog.Show();
            ptext.text = "You found a Laser. \n[ Space ] Continue";
            canvTog.laser.isOwned = true;
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
