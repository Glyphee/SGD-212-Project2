using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Playercontroller : MonoBehaviour
{
    [SerializeField] public List<GameObject> slimeList = new List<GameObject>();
    [SerializeField] public GameObject playerGO;
    //public GameObject playerParent;
    [SerializeField] private int currSlime = 0;
    [SerializeField] private GameObject slimeholdPoint;
    [SerializeField] public int slimesHeld;
    [SerializeField] private float throwPower = 1f;
    [SerializeField] private int playerHealth = 5;

    private Vector3 mousePos;
    private Vector3 lookDirection;
    private float lookAngle;
    public int slimesInParty = 0; //amount of slimes in your party
    public bool isHolding = false; //if the player is holding a slime

    public MessageScript MessageScript;
    AudioManager audioMan;
    float timer;

    float nextActionTime = 0.0f; //timer
    float period = 0.15f; //happens every second

    // public Animator playerAnimator;

    public UnityEngine.CharacterController characterController;
    [SerializeField] private float speed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;
    public SlimeController slimeController;
    public DoorScript DoorScript;
    

    private void Start()
    {
        slimesHeld = slimeList.Count;
        // playerAnimator = gameObject.GetComponent<Animator>();
        audioMan = GetComponent<AudioManager>();
    }

    void Update()
    {
        //Move the player
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), -0.05f, Input.GetAxis("Vertical"));
        moveDirection *= speed;
        characterController.Move(moveDirection * Time.deltaTime);

        timer += Time.deltaTime;

        if (Time.time > nextActionTime) //timer
        { //adds footstep sounds, gets absolute value average of both inputs and clamps it to 0.5
            nextActionTime += period;
            audioMan.sounds[1].volume = Mathf.Clamp(Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical")), 0f , 0.5f);
            audioMan.Play("Footstep");
        }

        //Following the mouse
        mousePos = Input.mousePosition;
        mousePos.z = 10;
        lookDirection = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg;
        playerGO.transform.rotation = Quaternion.Euler(0f, lookAngle + 90f, 0f);

        //Cycles the slimes when the player presses space
        if (Input.GetKeyDown("space"))
        {
            if (slimesInParty > 1)
            {
                slimeList[currSlime].GetComponent<SlimeController>().isHeld = false;
                slimeList[currSlime].transform.position = slimeholdPoint.transform.position;
                if(currSlime < slimesInParty - 1)
                {
                    currSlime++;
                }               
                else
                {
                    currSlime = 0;
                }
                slimeList[currSlime].GetComponent<SlimeController>().isHeld = true;
            }
            else
            {
                slimeList[1].GetComponent<SlimeController>().isHeld = false;
                slimeList[1].transform.position = slimeholdPoint.transform.position;
                slimeList[1].GetComponent<SlimeController>().isHeld = true;
            }
        }

        //keeps the current slime at holding point
        //This is where that repeating error is, i dont know why it says that it is somewhere else
        if (currSlime != -1 && slimeList[currSlime].GetComponent<SlimeController>().isHeld)
        {
            slimeList[currSlime].transform.position = slimeholdPoint.transform.position;
            isHolding = true;
        }


        //Launches the slime
        if (Input.GetMouseButtonDown(0) == true/* && slimesHeld != 0*/)
        {
            if(isHolding)
            {
                slimeList[currSlime].GetComponent<NavMeshAgent>().enabled = false;
                slimeList[currSlime].GetComponent<Rigidbody>().velocity = -playerGO.transform.right * throwPower;
                slimeList[currSlime].GetComponent<SlimeController>().isHeld = false;
                slimeList[currSlime].GetComponent<SlimeController>().isInParty = false;
                slimeList.RemoveAt(currSlime);
                currSlime++;
                slimesInParty--;
                audioMan.Play("Throw");
            }           
            isHolding = false;
            if (currSlime > slimesInParty)
            {
                currSlime = 0;
            }
            //slimesHeld--;

            // playerAnimator.SetBool("IsAttack01", true);
        }

        if (slimesInParty > 0 && !isHolding) //searching for slimes in party 
        {
            print("Current slime missing, searching for slimes in party");

            currSlime++;
            
            if (currSlime > slimeList.Count - 1)
            {
                currSlime = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("key"))
        {
            other.gameObject.SetActive(false);
            DoorScript.hasKey = true;
            MessageScript.StartMessage("You found a key!");
            audioMan.Play("Key Collect");
        }
        else if (other.gameObject.CompareTag("enemy1"))
        {
            playerHealth--;
            print("player hurt");
            audioMan.Play("Player Hurt");
        }
        else if (other.gameObject.CompareTag("enemy2") || other.gameObject.CompareTag("enemy3"))
        {
            playerHealth = playerHealth - 2;
            audioMan.Play("Player Hurt");
        }
        else if (other.gameObject.CompareTag("projectile"))
        {
            playerHealth--;
            audioMan.Play("Player Hurt");
        }
    }
}
