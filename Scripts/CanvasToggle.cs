using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasToggle : MonoBehaviour {

    CanvasRenderer panel;
    Text ptext;
    PlayerMovement player;
    Image img;
    Color color;
    public float alpha;
    public bool hidden;
    public bool start;
    public bool invMenu = false;
    public bool itemMenu = false;
    public bool combineMenu = false;
    public bool firstSelected = false;
    public bool remoteWorking = false;
    public bool panelOn = true;
    public bool combineSuccess = false;
    public bool computerOn = false;
    public bool satisfied = true;
    public bool firstK = true;
    public bool running = false;
    public int count;
    public int combineCount;
    public int combineCount2;
    public int maxKeys;
    bool firstInv = false;
    Animator anim;
    int hasEquipped;
    public string inventoryList;
    public string combineList1;
    public string combineList2;
    public string combination;
    BirdMove bird;
    //
    public string firstitem, seconditem;
    public string password;
    public string L1, L2, L3, L4, L5, L6;
    public string totalPass;
    public GameObject forceField;
    public bool fieldUp = true;
    //
    //public IEnumerable<KeyCode> keys;
    
    public class gameItem
    {
        public string name;
        public int ID;
        public string description;
        public bool isOwned;
        public bool targeted;

        public gameItem(string Name, int id, string Description, bool IsOwned, bool Targeted)
        {
            name = Name;
            ID = id;
            description = Description;
            isOwned = IsOwned;
            targeted = Targeted;
        }

    }

    public List<gameItem> itemList = new List<gameItem>();

    public gameItem laser = new gameItem("Laser", 0, "A portable but extremely powerful device that can cut through most materials.", false, false);
    public gameItem remote = new gameItem("Remote", 0, "A remote control with only one button, and a panel on the back.", false,  false); 
    public gameItem screwdriver = new gameItem("Screwdriver", 0, "A flat-head screwdriver", false, false);
    public gameItem teddyBear = new gameItem("Teddy Bear", 0, "A toy bear with motion-activated greetings. It seems to be working.", false, false); 
    public gameItem battery = new gameItem("Battery", 0, "A battery taken from a portable device.", false, false);
    public gameItem usb = new gameItem("USB Stick", 0, "A portable storage device.", false, false);



    // Use this for initialization
    public void Start() {

        panel = GameObject.FindGameObjectWithTag("belowMsg").GetComponent<CanvasRenderer>();
        ptext = GameObject.FindGameObjectWithTag("panelTxt").GetComponent<Text>();
        ptext.text = "You find yourself in a strange room... \n[ W ] [ A ] [ S ] [ D ] / [ Arrow Keys ] Move | [ K ] Interact | [ I ] Inventory | [ L ] Toggle Run\n[ Space ] Continue";
        hidden = false;
        start = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        img = GameObject.FindGameObjectWithTag("belowMsg").GetComponent<Image>();
        color = panel.GetColor();
        alpha = panel.GetAlpha();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        forceField = GameObject.FindGameObjectWithTag("ForceField");
        bird = GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdMove>();

        itemList.Add(screwdriver);
        itemList.Add(remote);
        itemList.Add(laser);
        itemList.Add(teddyBear);
        itemList.Add(battery);
        itemList.Add(usb);

    }
	
	// Update is called once per frame
	void Update () {

        if ((Input.GetKeyDown(KeyCode.L) && (computerOn == false)) && (running == false))
        {
            running = true;
        }
        else if ((Input.GetKeyDown(KeyCode.L) && (computerOn == false)) && (running == true))
        {
            running = false;
        }

            //cheats
            if ((Input.GetKeyDown(KeyCode.LeftShift)) && (Input.GetKeyDown(KeyCode.Alpha1)))
        {
            battery.isOwned = true;
            battery.targeted = false;
            Show();
            ptext.text = "battery";
        }
        if ((Input.GetKeyDown(KeyCode.LeftShift)) && (Input.GetKeyDown(KeyCode.Alpha2)))
        {
            laser.isOwned = true;
            laser.targeted = false;
            Show();
            ptext.text = "laser";
        }
        if ((Input.GetKeyDown(KeyCode.LeftShift)) && (Input.GetKeyDown(KeyCode.Alpha3)))
        {
            remote.isOwned = true;
            remote.targeted = false;
            Show();
            ptext.text = "remote";
        }
        if ((Input.GetKeyDown(KeyCode.LeftShift)) && (Input.GetKeyDown(KeyCode.Alpha4)))
        {
            screwdriver.isOwned = true;
            screwdriver.targeted = false;
            Show();
            ptext.text = "screwdriver";
        }
        if ((Input.GetKeyDown(KeyCode.LeftShift)) && (Input.GetKeyDown(KeyCode.Alpha5)))
        {
            teddyBear.isOwned = true;
            teddyBear.targeted = false;
            Show();
            ptext.text = "bear";
        }
        //

        if (Input.GetKeyDown(KeyCode.Space) && (hidden == false) && (start = true) /*&& (firstInv == false)*/)
        {
            Hide();
            start = false;
            invMenu = false;
            itemMenu = false;
            combineMenu = false;
            firstSelected = false;
            computerOn = false;
            //
            count = 0;
            combineCount = 0;
            combineCount2 = 0;
            combineList1 = "";
            combineList2 = "";
            firstitem = "";
            seconditem = "";
            L1 = "";
            L2 = "";
            L3 = "";
            L4 = "";
            L5 = "";
            L6 = "";
            maxKeys = 0;
            //
        }

        if (Input.GetKeyDown(KeyCode.I) && (firstInv == true) && (computerOn == false))
        {
            Inventory();
            itemMenu = false;
            combineMenu = false;
        }

        if (Input.GetKeyDown(KeyCode.I) && (hidden == true) && (firstInv == false) && (computerOn == false))
        {
            Show();
            ptext.text = "In the Inventory, you can view items you have obtained. Interactions [ K ] vary when holding certain items. Items can be useful alone, or together when combined. \n[ I ] Inventory | [ Space ] Continue";
            firstInv = true;
            itemMenu = false;
            combineMenu = false;
        }


        // view items
        InventoryMenu();
        
        Computer();        

        //reset
        if ((Input.GetKeyDown(KeyCode.LeftControl)))
        {
            SceneManager.LoadScene("scene1");
        }

    }


    public void InventoryMenu()
    {
        ViewItems();
        CombineItems();
    }

    public void Computer()
    {
        if ((Input.GetKeyDown(KeyCode.Return)) && (computerOn == true) && (maxKeys <= 7))
        {
            password = "UNITYP";
            //must change with pass
            totalPass = L1 + L2 + L3 + L4 + L5 + L6;
            Debug.Log(totalPass);
            //checkpass
            if ((totalPass == password) && (fieldUp = true))
            {
                ptext.text = "Field disabled.\n [Space to continue]";
                forceField.SetActive(false);
                fieldUp = false;
            }
            else
            {
                ptext.text = "Wrong password.\n [Space to continue]";
            }
                 
        }

        // K
        if ((Input.GetKeyDown(KeyCode.K)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 0:
                    maxKeys++;
                    break;
                case 1:
                    L1 = "K";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "K";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "K";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "K";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "K";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "K";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }


        //foreach (KeyCode key in keys)
        //{
        //    if ((Input.GetKeyDown(key) && (computerOn == true) && (maxKeys < 7)))
        //    {
        //        switch (maxKeys)
        //        {
        //            case 0:
        //                L1 = key.ToString();
        //                Debug.Log(key);
        //                maxKeys++;
        //                break;
        //            case 1:
        //                L2 = key.ToString();
        //                Debug.Log(key);
        //                maxKeys++;
        //                break;
        //            case 2:
        //                L3 = key.ToString();
        //                Debug.Log(key);
        //                maxKeys++;
        //                break;
        //            case 3:
        //                L4 = key.ToString();
        //                Debug.Log(key);
        //                maxKeys++;
        //                break;
        //            case 4:
        //                L5 = key.ToString();
        //                Debug.Log(key);
        //                maxKeys++;
        //                break;
        //            case 5:
        //                L6 = key.ToString();
        //                Debug.Log(key);
        //                maxKeys++;
        //                break;
        //            case 6:
        //                L7 = key.ToString();
        //                Debug.Log(key);
        //                maxKeys++;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

        // U
        if ((Input.GetKeyDown(KeyCode.U)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "U";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "U";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "U";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "U";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "U";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "U";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }


        // V
        if ((Input.GetKeyDown(KeyCode.V)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "V";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "V";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "V";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "V";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "V";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "V";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }


        // A
        if ((Input.GetKeyDown(KeyCode.A)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "A";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "A";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "A";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "A";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "A";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "A";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // B
        if ((Input.GetKeyDown(KeyCode.B)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "B";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "B";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "B";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "B";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "B";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "B";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // C
        if ((Input.GetKeyDown(KeyCode.C)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "C";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "C";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "C";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "C";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "C";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "C";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }


        // D
        if ((Input.GetKeyDown(KeyCode.D)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "D";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "D";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "D";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "D";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "D";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "D";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // E
        if ((Input.GetKeyDown(KeyCode.E)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "E";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "E";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "E";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "E";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "E";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "E";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // F
        if ((Input.GetKeyDown(KeyCode.F)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "F";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "F";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "F";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "F";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "F";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "F";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // G
        if ((Input.GetKeyDown(KeyCode.G)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "G";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "G";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "G";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "G";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "G";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "G";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // H
        if ((Input.GetKeyDown(KeyCode.H)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "H";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "H";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "H";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "H";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "H";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "H";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // I
        if ((Input.GetKeyDown(KeyCode.I)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "I";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "I";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "I";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "I";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "I";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "I";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // J
        if ((Input.GetKeyDown(KeyCode.J)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "J";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "J";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "J";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "J";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "J";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "J";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // L
        if ((Input.GetKeyDown(KeyCode.L)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "L";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "L";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "L";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "L";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "L";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "L";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // M
        if ((Input.GetKeyDown(KeyCode.M)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "M";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "M";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "M";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "M";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "M";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "M";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // N
        if ((Input.GetKeyDown(KeyCode.N)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "N";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "N";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "N";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "N";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "N";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "N";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // O
        if ((Input.GetKeyDown(KeyCode.O)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "O";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "O";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "O";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "O";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "O";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "O";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // P
        if ((Input.GetKeyDown(KeyCode.P)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "P";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "P";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "P";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "P";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "P";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "P";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // Q
        if ((Input.GetKeyDown(KeyCode.Q)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "Q";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "Q";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "Q";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "Q";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "Q";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "Q";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // R
        if ((Input.GetKeyDown(KeyCode.R)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "R";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "R";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "R";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "R";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "R";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "R";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // S
        if ((Input.GetKeyDown(KeyCode.S)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "S";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "S";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "S";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "S";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "S";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "S";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // T
        if ((Input.GetKeyDown(KeyCode.T)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "T";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "T";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "T";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "T";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "T";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "T";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // W
        if ((Input.GetKeyDown(KeyCode.W)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "W";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "W";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "W";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "W";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "W";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "W";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // X
        if ((Input.GetKeyDown(KeyCode.X)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "X";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "X";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "X";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "X";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "X";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "X";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // Y
        if ((Input.GetKeyDown(KeyCode.Y)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "Y";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "Y";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "Y";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "Y";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "Y";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "Y";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

        // Z
        if ((Input.GetKeyDown(KeyCode.Z)) && (computerOn == true) && (maxKeys < 7))
        {
            switch (maxKeys)
            {
                case 1:
                    L1 = "Z";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 2:
                    L2 = "Z";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 3:
                    L3 = "Z";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 4:
                    L4 = "Z";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 5:
                    L5 = "Z";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                case 6:
                    L6 = "Z";
                    ptext.text += "  *  ";
                    maxKeys++;
                    break;
                default:
                    break;
            }
        }

    }

    public void ViewItems()
    {

        // view items
        if ((Input.GetKeyDown(KeyCode.V)) && (invMenu == true) && (combineMenu == false) && (itemMenu == false))
        {
            itemMenu = true;            
            inventoryList = "";
            count = 0;

            
            foreach (gameItem item in itemList)
            {

                if ((item.isOwned == true))
                {
                    count++;
                    inventoryList += "|| " + count + ". " + item.name + " ";
                    item.ID = count;
                }
            }

            if (count == 0)
            {
                inventoryList = "You haven't obtained any items yet.";
            }

            ptext.text = inventoryList + "\n [ I ] Inventory | [ Space ] Continue | [ Numbers/Keypad ] Description";

            invMenu = false;
        }


        //1st item description
        if ((Input.GetKeyDown(KeyCode.Alpha1) || (Input.GetKeyDown(KeyCode.Keypad1))) && (itemMenu == true) && (count >= 1))
        {
            foreach (gameItem item in itemList)
            {
                    if ((item.isOwned == true) && (item.ID == 1))
                    {
                        ptext.text = "|| " + item.ID + ". " + item.description + "\n[ Space ] Continue | [ I ] Inventory Menu";
                    }
            }
            itemMenu = false;
        }

        //2nd item description
        if ((Input.GetKeyDown(KeyCode.Alpha2) || (Input.GetKeyDown(KeyCode.Keypad2))) && (itemMenu == true) && (count >= 2))
        {
            foreach (gameItem item in itemList)
            {
                if ((item.isOwned == true) && (item.ID == 2))
                {
                    ptext.text = "|| " + item.ID + ". " + item.description + "\n[ Space ] Continue | [ I ] Inventory Menu";
                }
            }
            itemMenu = false;
        }

        //3rd item description
        if ((Input.GetKeyDown(KeyCode.Alpha3) || (Input.GetKeyDown(KeyCode.Keypad3))) && (itemMenu == true) && (count >= 3))
        {
            foreach (gameItem item in itemList)
            {
                if ((item.isOwned == true) && (item.ID == 3))
                {
                    ptext.text = "|| " + item.ID + ". " + item.description + "\n[ Space ] Continue | [ I ] Inventory Menu";
                }
            }
            itemMenu = false;
        }

        //4th item description
        if ((Input.GetKeyDown(KeyCode.Alpha4) || (Input.GetKeyDown(KeyCode.Keypad4))) && (itemMenu == true) && (count >= 4))
        {
            foreach (gameItem item in itemList)
            {
                if ((item.isOwned == true) && (item.ID == 4))
                {
                    ptext.text = "|| " + item.ID + ". " + item.description + "\n[ Space ] Continue | [ I ] Inventory Menu";
                }
            }
            itemMenu = false;
        }

        //5th item description
        if ((Input.GetKeyDown(KeyCode.Alpha5) || (Input.GetKeyDown(KeyCode.Keypad5))) && (itemMenu == true) && (count >= 5))
        {
            foreach (gameItem item in itemList)
            {
                if ((item.isOwned == true) && (item.ID == 5))
                {
                    ptext.text = "|| " + item.ID + ". " + item.description + "\n[ Space ] Continue | [ I ] Inventory Menu";
                }
            }
            itemMenu = false;
        }

    }
    
    public void CombineItems()
    {

        // combine items
        if ((Input.GetKeyDown(KeyCode.C)) && (invMenu == true) && (combineMenu == false) && (itemMenu == false))
        {

            invMenu = false;
            firstSelected = false;
            combineList1 = "";
            combineCount = 0;
            combineSuccess = false;

            foreach (gameItem item in itemList)
            {
                if ((item.isOwned == true))
                {
                    combineCount++;
                    combineList1 += "|| " + combineCount + ". " + item.name + " ";
                    item.ID = combineCount;
                    item.targeted = false;
                }
            }

            if (combineCount == 0)
            {
                ptext.text = "You haven't obtained any items yet.\n [I to go back, Space to close, Numbers/Keypad to select 1st item in combination]";
            }

            if (combineCount == 1)
            {
                ptext.text = "You'll need more than one item to combine.\n [I to go back, Space to close, Numbers/Keypad to select 1st item in combination]";
            }

            if ((combineCount > 1) && (firstSelected == false))
            {
                ptext.text = combineList1 + "\n [Space to close, Numbers/Keypad to select 1st item in combination]";
                combineMenu = true;
            }
        }

        //second combinator
        if (((Input.GetKeyDown(KeyCode.Alpha1)) || (Input.GetKeyDown(KeyCode.Keypad1))) && (combineMenu == true) && (firstSelected == true))
        {
            foreach (gameItem item3 in itemList)
            {
                if ((item3.ID == 1) && (item3.isOwned))
                {
                    seconditem = item3.name;
                    firstSelected = false;
                    CombineResult(firstitem, seconditem);
                }
            }
        }

        //first combinator
        if (((Input.GetKeyDown(KeyCode.Alpha1)) || (Input.GetKeyDown(KeyCode.Keypad1))) && (combineMenu == true) && (firstSelected == false))
        {

            foreach (gameItem item in itemList)
            {
                if ((item.ID == 1) && (item.isOwned))
                {
                    firstitem = item.name;
                    item.targeted = true;
                    firstSelected = true;
                }
            }

            SecondList();
        }

        //second combinator
        if (((Input.GetKeyDown(KeyCode.Alpha2)) || (Input.GetKeyDown(KeyCode.Keypad2))) && (combineMenu == true) && (firstSelected == true) && (combineCount2 >= 2))
        {
            foreach (gameItem item3 in itemList)
            {
                if ((item3.ID == 2) && (item3.isOwned))
                {
                    seconditem = item3.name;
                    firstSelected = false;
                    CombineResult(firstitem, seconditem);
                }
            }
        }

        //first combinator
        if (((Input.GetKeyDown(KeyCode.Alpha2)) || (Input.GetKeyDown(KeyCode.Keypad2))) && (combineMenu == true) && (firstSelected == false) && (combineCount >= 2))
        {
              
            foreach (gameItem item in itemList)
            {
                if ((item.ID == 2) && (item.isOwned))
                {
                    firstitem = item.name;
                    item.targeted = true;
                    firstSelected = true;

                }
            }
            SecondList();
        }

        //second combinator
        if (((Input.GetKeyDown(KeyCode.Alpha3)) || (Input.GetKeyDown(KeyCode.Keypad3))) && (combineMenu == true) && (firstSelected == true))
        {
            foreach (gameItem item3 in itemList)
            {
                if ((item3.ID == 3) && (item3.isOwned))
                {
                    seconditem = item3.name;
                    firstSelected = false;
                    CombineResult(firstitem, seconditem);
                }

            }
        }

        //first combinator
        if (((Input.GetKeyDown(KeyCode.Alpha3)) || (Input.GetKeyDown(KeyCode.Keypad3))) && (combineMenu == true) && (firstSelected == false) && (combineCount >= 3))
        {

            foreach (gameItem item in itemList)
            {
                if ((item.ID == 3) && (item.isOwned))
                {
                    firstitem = item.name;
                    item.targeted = true;
                    firstSelected = true;

                }
            }
            SecondList();

        }

        //second combinator
        if (((Input.GetKeyDown(KeyCode.Alpha4)) || (Input.GetKeyDown(KeyCode.Keypad4))) && (combineMenu == true) && (firstSelected == true))
        {
            foreach (gameItem item3 in itemList)
            {
                if ((item3.ID == 4) && (item3.isOwned))
                {
                    seconditem = item3.name;
                    firstSelected = false;
                    CombineResult(firstitem, seconditem);
                }
            }

        }

        //first combinator
        if (((Input.GetKeyDown(KeyCode.Alpha4)) || (Input.GetKeyDown(KeyCode.Keypad4))) && (combineMenu == true) && (firstSelected == false) && (combineCount >= 4))
        {
            foreach (gameItem item in itemList)
            {
                if ((item.ID == 4) && (item.isOwned))
                {
                    firstitem = item.name;
                    item.targeted = true;
                    firstSelected = true;

                }
            }
            SecondList();

        }

        //first combinator
        if (((Input.GetKeyDown(KeyCode.Alpha5)) || (Input.GetKeyDown(KeyCode.Keypad5))) && (combineMenu == true) && (firstSelected == false) && (combineCount >= 5))
        {
            foreach (gameItem item in itemList)
            {
                if ((item.ID == 5) && (item.isOwned))
                {
                    firstitem = item.name;
                    item.targeted = true;
                    firstSelected = true;
                }
            }
            SecondList();
        }


    }

    public void SecondList()
    {
        //setting item id's after first selected
        combineCount2 = 0;
        combineList2 = "";
        foreach (gameItem item2 in itemList)
        {
            if ((item2.targeted == false) && (item2.isOwned == true))
            {
                combineCount2++;
                combineList2 += "|| " + combineCount2 + ". " + item2.name + " ";
                item2.ID = combineCount2;
            }
        }
            //displaying second list(after first selected)
            ptext.text = combineList2 + "\n [Space to close, Numbers/Keypad to select 2nd item in combination]";
    }

    public void CombineResult(string FirstItem, string SecondItem)
    {

        //remote combines with battery

        if (((FirstItem == "Remote") && (SecondItem == "Battery")) || ((FirstItem == "Battery") && (SecondItem == "Remote")))
        {
            if (panelOn == false)
            {
                combineList2 = "You put the Battery into the Remote, and screw the panel shut.\n [Space to continue]";
                remoteWorking = true;
                battery.isOwned = false;
                remote.description = "A remote with only one button. It now has a power source.";
                combineSuccess = true;
                panelOn = true;

                firstitem = "";
                seconditem = "";
            }
            else if (panelOn == true)
            {
                combineList2 = "You need to open the Remote's panel before putting in a Battery.\n [Space to continue]";
                combineSuccess = true;

                firstitem = "";
                seconditem = "";
            }

        }

        //screwdriver with remote
        if (((FirstItem == "Screwdriver") && (SecondItem == "Remote")) || ((FirstItem == "Remote") && (SecondItem == "Screwdriver")))
        {
            if ((panelOn == true) && (remoteWorking == false))
            {
                combineList2 = "You took the panel off the Remote using the Screwdriver. There's no battery inside.\n [Space to continue]";
                remote.description = "A remote with only one button, and an unscrewed panel with no battery inside.";
                panelOn = false;
                combineSuccess = true;

                firstitem = "";
                seconditem = "";
            }
            else if ((panelOn == true) && (remoteWorking == true))
            {
                battery.isOwned = true;
                combineList2 = "You take the Battery that's inside using the Screwdriver, and replace the panel.\n [Space to continue]";
                remote.description = "A remote with only one button, and a panel on the back.";
                remoteWorking = false;
                panelOn = true;
                combineSuccess = true;

                firstitem = "";
                seconditem = "";
            }
            else if (panelOn == false)
            {
                combineList2 = "You replace the empty panel using the Screwdriver.\n [Space to continue]";
                remote.description = "A remote with only one button, and a panel on the back.";
                combineSuccess = true;
                panelOn = true;

                firstitem = "";
                seconditem = "";
            }

        }
        //

        //screwdriver combines with bear
        if (((FirstItem == "Screwdriver") && (SecondItem == "Teddy Bear")) || ((FirstItem == "Teddy Bear") && (SecondItem == "Screwdriver")))
        {
            if (battery.isOwned == false)
            {
                combineList2 = "You unscrew a panel on the toy, and find a Battery inside. You take it.\n [Space to continue]";
                teddyBear.description = "A toy bear with motion-activated greetings. It is no longer working.";
                battery.isOwned = true;
                satisfied = false;
                combineSuccess = true;

                firstitem = "";
                seconditem = "";

            }
            else if (battery.isOwned == true)
            {
                combineList2 = "You unscrew a panel on the toy. It's missing a battery.\n [Space to continue]";
                combineSuccess = true;

                firstitem = "";
                seconditem = "";
            }
        }

        //teddy bear with battery
        if (((FirstItem == "Battery") && (SecondItem == "Teddy Bear")) || ((FirstItem == "Teddy Bear") && (SecondItem == "Battery")))
        {
            if ((battery.isOwned == false) && (remoteWorking == false))
            {
                combineSuccess = true;
                combineList2 = "You unscrew a panel on the toy, and find a Battery inside. You take it.\n [Space to continue]";
                teddyBear.description = "A toy bear with motion-activated greetings. It is no longer working.";
                battery.isOwned = true;
                satisfied = false;

                firstitem = "";
                seconditem = "";

            }
            else if (battery.isOwned == true)
            {
                battery.isOwned = false;
                combineSuccess = true;
                combineList2 = "You put the battery back inside the toy bear.\n [Space to continue]";
                teddyBear.description = "A toy bear with motion-activated greetings. It seems to be working.";
                satisfied = true;

                firstitem = "";
                seconditem = "";
            }

        }

        //
        ptext.text = combineList2;
        
        if (combineSuccess == false)
        {
            ptext.text = "You tried to combine the two items, but nothing happened.\n [Space to continue]";
        }
        combineMenu = false;
    }

    public void Inventory()
    {
        Show();
        invMenu = true;
        combineMenu = false;
        itemMenu = false;
        ptext.text = "[V] View Item     ||     [C] Combine\n[Space to close]";
    }
    
    public void Hide ()
    {
        panel.SetAlpha(0);
        ptext.text = "";
        hidden = true;
        player.canWalk = true;
    }

    public void Show ()
    {
        panel.SetColor(color);
        panel.SetAlpha(alpha);
        hidden = false;
        player.canWalk = false;
        anim.SetBool("iswalking", false);
        ptext.text = "show";  
    }



}
