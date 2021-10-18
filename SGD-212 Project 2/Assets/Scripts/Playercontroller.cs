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
    // [SerializeField] private int playerHealth = 3;

    private Vector3 mousePos;
    private Vector3 lookDirection;
    private float lookAngle;
    public int slimesInParty = 0; //amount of slimes in your party
    public bool isHolding = false; //if the player is holding a slime

    public MessageScript MessageScript;
    AudioManager audioMan;
    // public Animator SlimeAnim;
    float timer;

    float nextActionTime = 0.0f; //timer
    float period = 0.15f; //happens every second

    private bool isMoving;
    public Animator playerAnimator;

    public UnityEngine.CharacterController characterController;
    [SerializeField] private float speed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;
    public SlimeController slimeController;
    public DoorScript DoorScript;

    //Gravity related things
    [Header("Gravity")]
    [SerializeField] float gravity;
    [SerializeField] float constantGravity;
    [SerializeField] float maxGravity;
    float currentGravity;

    Vector3 gravityDirection = Vector3.down;
    Vector3 gravityMovement;



    private void Start()
    {
        isMoving = true;

        slimesHeld = slimeList.Count;
        // playerAnimator = gameObject.GetComponent<Animator>();
        audioMan = GetComponent<AudioManager>();

        if (PlayerPrefs.HasKey("Volume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            //print("No volume setting detected, setting to default");
        }
    }

    void Update()
    {
        CalculateGravity();
        //was -0.05f
        //Move the player
        if(isMoving == true)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;
            characterController.Move(moveDirection * Time.deltaTime + gravityMovement);
        }

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
                //slimeList[currSlime].transform.position = slimeholdPoint.transform.position;
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
        if (currSlime != -1 && slimesInParty != 0)
        {
            slimeList[currSlime].transform.position = slimeholdPoint.transform.position;
            isHolding = true;
            slimeList[currSlime].GetComponent<SlimeController>().isHeld = true;
        }


        //Launches the slime
        if (Input.GetMouseButtonDown(0) == true/* && slimesHeld != 0*/)
        {
            playerAnimator.SetTrigger("Attack01");
            if(isHolding)
            {
                slimeList[currSlime].GetComponent<NavMeshAgent>().enabled = false;
                // slimeList[currSlime].GetComponentInChildren<Animator>().SetTrigger("Thrown");
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
        }

        if (slimesInParty > 0 && !isHolding) //searching for slimes in party 
        {
            //print("Current slime missing, searching for slimes in party");

            currSlime++;
            
            if (currSlime > slimeList.Count - 1)
            {
                currSlime = 0;
            }
        }
    }

    void CalculateGravity()
    {
        if(characterController.isGrounded)
        {
            currentGravity = constantGravity;
        }
        else
        {
            if(currentGravity > maxGravity)
            {
                currentGravity -= gravity * Time.deltaTime;
            }
        }

        gravityMovement = gravityDirection * -currentGravity * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("key"))
        {
            other.gameObject.SetActive(false);
            DoorScript.hasKey = true;
            MessageScript.StartMessage("You found a key! Maybe it unlocks a door somewhere on this floor.");
            audioMan.Play("Key Collect");
        }
        else if (other.gameObject.CompareTag("enemy1"))
        {
            // playerHealth--;
            healthImageScript.instance.playerHealth--;
            audioMan.Play("Hurt");
            playerAnimator.SetTrigger("damaged");
            healthImageScript.instance.CheckHealth();
            
        }       
        else if (other.gameObject.CompareTag("enemy2") || other.gameObject.CompareTag("enemy3"))
        {
            // playerHealth = playerHealth - 2;
            healthImageScript.instance.playerHealth--;
            healthImageScript.instance.playerHealth--;
            audioMan.Play("Hurt");
            playerAnimator.SetTrigger("damaged");
            healthImageScript.instance.CheckHealth();
            other.gameObject.GetComponent<AudioManager>().Play("Attack");
        }
        else if (other.gameObject.CompareTag("projectile"))
        {
            // playerHealth--;
            healthImageScript.instance.playerHealth--;
            audioMan.Play("Hurt");
            healthImageScript.instance.CheckHealth();
        }
    }

    public IEnumerator GameOver()
    {
        isMoving = false;
        playerAnimator.SetBool("IsDead", true);
        yield return new WaitForSeconds(2);

        StartCoroutine(levelTransport_Script.J.RegularTransit("GameOver"));
    }
}
