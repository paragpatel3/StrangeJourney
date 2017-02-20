using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Rock : MonoBehaviour {

    public PlayerMovement player;
    Rigidbody2D rbody;
    CanvasToggle canvTog;
    Text ptext;
    public GameObject rock;
    public GameObject remains;
    GameObject remains2;
    private bool trigger;

    // Use this for initialization
    void Start () {

        canvTog = GameObject.FindGameObjectWithTag("belowMsg").GetComponent<CanvasToggle>();
        ptext = GameObject.FindGameObjectWithTag("panelTxt").GetComponent<Text>();
        rock = GameObject.FindGameObjectWithTag("Rock");
        remains = GameObject.FindGameObjectWithTag("Remains");
        remains2 = remains;
        remains.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if ((trigger) && (Input.GetKeyDown(KeyCode.K)))
        {
            if (canvTog.laser.isOwned == false)
            {
                canvTog.Show();
                ptext.text = "A huge rock is blocking the way. There's a constant buzzing sound coming from the room. \n[ Space ] Continue";
            }
            else if (canvTog.laser.isOwned == true)
            {
                rock.SetActive(false);
                remains2.SetActive(true);
                canvTog.Show();
                ptext.text = "You use the Laser to cut a path through the huge rock. \n[ Space ] Continue";
            }
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
