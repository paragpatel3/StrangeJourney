using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MsgBy2 : MonoBehaviour
{

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

        if ((trigger) && (Input.GetKeyDown(KeyCode.K)))
        {
            canvTog.Show();
            ptext.text = "Sprites used: www.opengameart.org,\nwww.gameart2d.com\nRiddle help: www.eclectech.co.uk/words.php";
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
