using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private bool doorBool, finalDoorBool;
    private bool isPresent; // makes sure player is in range
    public bool hasKey; // for when player has a key
    public int deadBosses;

    AudioManager audioMan; //add an Audio Manager to the gameobject/prefab

    public static DoorScript D;

    public MessageScript MessageScript; // direct reference for MessageScript

    void Start()
    {
        if(this.gameObject.tag == "door") // these two decide if the gameObject that this is on is a regular door or the final door
        {
            doorBool = true;
            finalDoorBool = false;
            hasKey = true;
            deadBosses = 1;
        }
        else if(this.gameObject.tag == "FinalDoor")
        {
            finalDoorBool = true;
            doorBool = false;
            hasKey = false;
            deadBosses = 1;
        }
        else if(this.gameObject.tag == "castleDoor")
        {
            hasKey = false;
            deadBosses = 0;
        }
        
        audioMan = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>();

        D = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && isPresent && hasKey || Input.GetKeyDown(KeyCode.P) && isPresent && hasKey && deadBosses == 3) // checks to see if player presses the key to interact with objects, is present in the collision range, and has key
        {           
            isPresent = false;
            MessageScript.J.DismissPrompt();
            audioMan.Play("Open");
            this.gameObject.SetActive(false); // if above is true then disables the gameObject this is attached to           
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isPresent = true;

            if(finalDoorBool && !hasKey) // summons messagePanel upon player entering
            {
                MessageScript.StartMessage("It's locked, seems we'll need a key. There should be one somewhere...");
            }
            else if(deadBosses == 0)
            {
                MessageScript.StartMessage("The door is a bit stuck. Might want to defeat the two skeletons around here first.");
            }
            else if(doorBool || finalDoorBool && hasKey) // turns on promptPanel when player enters
            {
                MessageScript.J.SummonPrompt();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            isPresent = false;
            MessageScript.J.DismissPrompt(); // turns off promptPanel upon player exiting
        }
    }
}
