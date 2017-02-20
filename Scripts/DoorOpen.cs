using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorOpen : MonoBehaviour {

    Rigidbody2D rbody;
    CanvasToggle canvTog;
    Text ptext;
    BoxCollider2D bird;
    private bool trigger;
    public bool unlocked = false;
    BirdMove birdMove;
    Warp warper;
    RectTransform target;


    // Use this for initialization
    void Start () {

        canvTog = GameObject.FindGameObjectWithTag("belowMsg").GetComponent<CanvasToggle>();
        ptext = GameObject.FindGameObjectWithTag("panelTxt").GetComponent<Text>();
        birdMove = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdMove>();
        warper = GameObject.FindGameObjectWithTag("door").GetComponent<Warp>();
        target = GameObject.FindGameObjectWithTag("target").GetComponent<RectTransform>();
        
    }
	
	// Update is called once per frame
	void Update () {
	
        if ((trigger) && (Input.GetKeyDown(KeyCode.K)) && (unlocked == false))
        {
            canvTog.Show();
            ptext.text = "The door is locked but it doesn't have a keyhole. Above the door is a panel. \n[Space to continue]";
        }

	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trigger = true;
        }

        if (collision.gameObject.tag == "BirdObj")
        {
            unlocked = true;
            canvTog.Show();
            ptext.text = "The panel lights up. \n[Space to continue]";
            birdMove.ok = false;
            warper.target = target;
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
