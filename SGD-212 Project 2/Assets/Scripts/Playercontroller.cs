using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Playercontroller : MonoBehaviour
{
    [SerializeField] private List<GameObject> slimeList = new List<GameObject>();
    [SerializeField] private GameObject playerGO;
    [SerializeField] private int currSlime = 0;
    [SerializeField] private GameObject slimeholdPoint;
    [SerializeField] public int slimesHeld;
    [SerializeField] private float throwPower = 1f;

    private Vector3 mousePos;
    private Vector3 lookDirection;
    private float lookAngle;
    public int slimesInParty = 0; //amount of slimes in your party
    public bool isHolding = false; //if the player is holding a slime

    // public Animator playerAnimator;

    public UnityEngine.CharacterController characterController;
    [SerializeField] private float speed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;
    public SlimeController slimeController;
    

    private void Start()
    {
        slimesHeld = slimeList.Count;

        // playerAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        //Move the player
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection *= speed;
        characterController.Move(moveDirection * Time.deltaTime);
        
        //Following the mouse
        mousePos = Input.mousePosition;
        mousePos.z = 10;
        lookDirection = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.x, lookDirection.z) * Mathf.Rad2Deg;
        playerGO.transform.rotation = Quaternion.Euler(0f, lookAngle + 90f, 0f);

        //Cycles the slimes when the player presses space
        if (Input.GetKeyDown("space") == true)
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
            
            
        }      

        //keeps the current slime at holding point
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
                currSlime++;
                slimesInParty--;
            }           
            isHolding = false;
            if (currSlime > slimesInParty)
            {
                currSlime = 0;
            }
            //slimesHeld--;

            // playerAnimator.SetBool("IsAttack01", true);
        }

        //(navMesh logic moved to SlimeControllers)

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
}
