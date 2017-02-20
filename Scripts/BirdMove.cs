using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BirdMove : MonoBehaviour {

    CanvasToggle canvTog;
    Rigidbody2D rbody;
    Transform trans;
    public Vector2 vec = new Vector2();
    public bool ok = false;
    public Vector3 birdpos, bird;
    CanvasRenderer panel;
    Text ptext;
    Image img;
    public PlayerMovement player;
    public bool trigger;
    public bool moved = false;
    Warp warper;
    DoorOpen doorOpen;

    // Use this for initialization
    void Start () {

        rbody = GameObject.FindGameObjectWithTag("BirdObj").GetComponent<Rigidbody2D>();
        birdpos = GameObject.FindGameObjectWithTag("BirdObj").GetComponent<Transform>().position;
        bird = GameObject.FindGameObjectWithTag("BirdObj").GetComponent<Transform>().position;
        panel = GameObject.FindGameObjectWithTag("belowMsg").GetComponent<CanvasRenderer>();
        ptext = GameObject.FindGameObjectWithTag("panelTxt").GetComponent<Text>();
        img = GameObject.FindGameObjectWithTag("belowMsg").GetComponent<Image>();
        canvTog = GameObject.FindGameObjectWithTag("belowMsg").GetComponent<CanvasToggle>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        warper = GameObject.FindGameObjectWithTag("door").GetComponent<Warp>();
        doorOpen = GameObject.FindGameObjectWithTag("dooropen").GetComponent<DoorOpen>();
    }
	
	// Update is called once per frame
	void Update () { 

        if ((ok) && (rbody.gameObject))
        {
            rbody.MovePosition(rbody.position + vec * Time.deltaTime);
        }


        if ((trigger) && (Input.GetKeyDown(KeyCode.K) && (canvTog.remote.isOwned == false))) {
            canvTog.Show();
            ptext.text = "A drone modelled to resemble a bird is hovering; the metal beak has a weird shape to it. It's quite high up. \n[ Space ] Continue";
        }

        else if ((trigger) && (Input.GetKeyDown(KeyCode.K)) && (canvTog.remote.isOwned == true) && (canvTog.remoteWorking == false) && (doorOpen.unlocked == false)) {
            canvTog.Show();
            ptext.text = "You try using the remote control near the 'bird'. Nothing happens. \n[ Space ] Continue";
        }

        else if ((trigger) && (Input.GetKeyDown(KeyCode.K)) && (canvTog.remote.isOwned == true) && (canvTog.remoteWorking == true) && (moved != true)) { 
            canvTog.Show();
            ptext.text = "You press the button on the remote. The bird starts moving towards the door across the room.";
            moved = true;
            ok = true;
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
