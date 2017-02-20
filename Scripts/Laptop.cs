using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Laptop : MonoBehaviour
{

    CanvasToggle canvTog;
    Text ptext;
    private bool trigger;
    public GameObject light;


    // Use this for initialization
    void Start()
    {
        canvTog = GameObject.FindGameObjectWithTag("belowMsg").GetComponent<CanvasToggle>();
        ptext = GameObject.FindGameObjectWithTag("panelTxt").GetComponent<Text>();
        light = GameObject.FindGameObjectWithTag("Light");
        light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if ((trigger) && (Input.GetKeyDown(KeyCode.K)))
        {
            canvTog.Show();
            ptext.text = "You've reached the end of the beginning...\nThank you for playing thus far.\nDeveloped in: Unity (C#)\nSprites used: www.opengameart.org\nwww.gameart2d.com\nRiddle help: www.eclectech.co.uk/words.php\nLevel design and development: Parag Patel (paragcpatel.co.uk)";

            if (canvTog.usb.isOwned == true)
            {
                light.SetActive(true);
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
