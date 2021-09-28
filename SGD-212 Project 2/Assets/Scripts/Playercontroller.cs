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
    [SerializeField] private float throwPower = 10f;

    private Vector3 mousePos;
    private Vector3 lookDirection;
    private float lookAngle;

    public UnityEngine.CharacterController characterController;
    [SerializeField] private float speed = 5.0f;
    private Vector3 moveDirection = Vector3.zero;

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
            int slimeindex = slimeList.Count - 1;
            if (currSlime == 0)
            {
                currSlime = 1;
                slimeList[currSlime].transform.position = slimeholdPoint.transform.position;
            }
            else if (currSlime == 1)
            {
                currSlime = 2;
                slimeList[currSlime].transform.position = slimeholdPoint.transform.position;
            }
            else
            {
                currSlime = 0;
                slimeList[currSlime].transform.position = slimeholdPoint.transform.position;
            }
        }

        //Keeps the slimes at the holding point
        if (currSlime == 0)
        {
            slimeList[currSlime].transform.position = slimeholdPoint.transform.position;
        }
        else if (currSlime == 1)
        {
            slimeList[currSlime].transform.position = slimeholdPoint.transform.position;
        }
        else if (currSlime == 2)
        {
            slimeList[currSlime].transform.position = slimeholdPoint.transform.position;
        }

        //Launches the slime
        /*
        if (Input.GetMouseButtonDown(0) == true && currSlime == 0)
        {
            currSlime = 1;
        }
        else if (Input.GetMouseButtonDown(0) && currSlime == 1)
        {
            currSlime = 2;
        }
        else
        {
            currSlime = 0;
        }
        */
    }
}
