using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rbody;
    Animator anim;
    public bool canWalk;
    CanvasToggle canvTog;
    Text ptext;

    // Use this for initialization
    void Start()
    {

        //gives access to components
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canvTog = GameObject.FindGameObjectWithTag("belowMsg").GetComponent<CanvasToggle>();
        ptext = GameObject.FindGameObjectWithTag("panelTxt").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {

        if (canWalk)
        {
            Walk();
        }

    }

    void Walk()
    {
        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (canvTog.running == true)
        {
            movement_vector *= 2;
        }

        if (movement_vector != Vector2.zero) // you must be moving if vector nonzero
        {
            anim.SetBool("iswalking", true);
            anim.SetFloat("input_x", 2*(movement_vector.x));
            anim.SetFloat("input_y", 2*(movement_vector.y));
        }
        else
        {
            anim.SetBool("iswalking", false);
        }
        rbody.MovePosition(rbody.position + ((movement_vector) * (Time.deltaTime)));
    }

}
