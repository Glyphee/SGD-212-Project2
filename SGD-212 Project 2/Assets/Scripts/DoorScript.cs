using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private bool doorBool, finalDoorBool;
    private bool isPresent; // makes sure player is in range
    private bool hasKey; // for when player has a key

    public MessageScript MessageScript; // direct reference for MessageScript

    void Start()
    {
        if(this.gameObject.tag == "door") // these two decide if the gameObject that this is on is a regular door or the final door
        {
            doorBool = true;
            finalDoorBool = false;
            hasKey = true;
        }
        else if(this.gameObject.tag == "FinalDoor")
        {
            finalDoorBool = true;
            doorBool = false;
            hasKey = false;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && isPresent && hasKey) // checks to see if player presses the key to interact with objects, is present in the collision range, and has key
        {
            isPresent = false;
            MessageScript.J.DismissPrompt();
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
