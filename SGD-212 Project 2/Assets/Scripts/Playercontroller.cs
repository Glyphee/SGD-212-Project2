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

    public UnityEngine.CharacterController characterController;
    [SerializeField] private float speed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;
    public SlimeController slimeController;

    private void Start()
    {
        slimesHeld = slimeList.Count;
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
            currSlime++;
            if (currSlime > slimeList.Count - 1)
            {
               currSlime = 0;
            }
            slimeList[currSlime].transform.position = slimeholdPoint.transform.position;
        }

        //Keeps the slimes at the holding point
        if (currSlime != -1)
        {
            slimeList[currSlime].transform.position = slimeholdPoint.transform.position;
        }

        //Launches the slime
        if (Input.GetMouseButtonDown(0) == true/* && slimesHeld != 0*/)
        {
            slimeList[currSlime].GetComponent<NavMeshAgent>().enabled = false;
            slimeList[currSlime].GetComponent<Rigidbody>().velocity = -playerGO.transform.right * throwPower;
            currSlime++;
            if (currSlime > slimeList.Count - 1)
            {
                currSlime = 0;
            }
            //slimesHeld--;
        }

        //Sets the slime's destination to the player
        if (slimeList[0].GetComponent<NavMeshAgent>().enabled == true)
        {
            slimeList[0].GetComponent<NavMeshAgent>().destination = playerGO.transform.position;
        }
        if (slimeList[1].GetComponent<NavMeshAgent>().enabled == true)
        {
            slimeList[1].GetComponent<NavMeshAgent>().destination = playerGO.transform.position;
        }
        if (slimeList[2].GetComponent<NavMeshAgent>().enabled == true)
        {
            slimeList[2].GetComponent<NavMeshAgent>().destination = playerGO.transform.position;
        }
    }
}
