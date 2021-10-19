using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private bool doorBool, finalDoorBool;
    private bool isPresent; // makes sure player is in range
    public bool hasKey; // for when player has a key
    public int deadBosses;
    public int deadEnemies;
    public string[] stringArray;

    AudioManager audioMan; //add an Audio Manager to the gameobject/prefab

    public static DoorScript D;

    public MessageScript MessageScript; // direct reference for MessageScript

    void Start()
    {
        if(this.gameObject.tag == "door") // these two decide if the gameObject that this is on is a regular door or the final door
        {
            doorBool = true;
            finalDoorBool = false;
            deadBosses = 3;
        }
        else if(this.gameObject.tag == "FinalDoor")
        {
            finalDoorBool = true;
            doorBool = false;
            deadBosses = 3;
            print("Enemy count " + deadEnemies);
        }
        else if(this.gameObject.tag == "castleDoor")
        {
            deadBosses = 0;
            print("Boss count " + deadBosses);
        }
        
        audioMan = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioManager>();

        D = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && isPresent && hasKey && this.gameObject.tag == "door") // checks to see if player presses the key to interact with objects, is present in the collision range, and has key
        {           
            isPresent = false;
            MessageScript.J.DismissPrompt();
            audioMan.Play("Open");
            this.gameObject.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.P) && isPresent && hasKey && deadEnemies <= 0 && this.gameObject.tag == "FinalDoor") // finalDoor
        {
            isPresent = false;
            MessageScript.J.DismissPrompt();
            audioMan.Play("Open");
            this.gameObject.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.P) && isPresent && deadBosses == 3 && this.gameObject.tag == "castleDoor") // casleDoor
        {
            isPresent = false;
            MessageScript.J.DismissPrompt();
            audioMan.Play("Open");
            this.gameObject.SetActive(false);
        }

        //print("Boss count " + deadBosses);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player at door");
            isPresent = true;

            if(finalDoorBool && !hasKey && deadEnemies != 0) // summons messagePanel upon player entering
            {
                print("Accessed finaldoor condition.");
                // "It's locked, seems we'll need a key. There should be one somewhere..."
                // "The door also seems a bit jammed, so we probably should take care of the enemies on this floor as well."
                StartCoroutine(MessageScript.J.MultipleMessages(stringArray));
            }
            else if(finalDoorBool && hasKey && deadEnemies != 0)
            {
                MessageScript.J.StartMessage("Seems like it's still jammed a little. Must have not taken care of all the enemies on this floor.");
            }
            else if(deadBosses != 3)
            {
                MessageScript.StartMessage("The door is a bit stuck. Might want to defeat the skeletons around here first.");
            }
            else if(doorBool || finalDoorBool && hasKey && deadEnemies <= 0 || deadBosses == 3) // turns on promptPanel when player enters
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
