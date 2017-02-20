using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NoteTwo : MonoBehaviour {

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
            ptext.text = "''My third is in immortal but not in mortal''\n''My fourth is in solidity but not in solidly''\n[ Space ] Continue";
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
