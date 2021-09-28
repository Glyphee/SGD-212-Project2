using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimBehaviorTestScript : MonoBehaviour
{
    public Animator selectedAnimator;
    public string[] animBoolArray;

    void Start()
    {
        selectedAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            selectedAnimator.SetBool(animBoolArray[0], true);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            selectedAnimator.SetBool(animBoolArray[1], true);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            selectedAnimator.SetBool(animBoolArray[2], true);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            selectedAnimator.SetBool(animBoolArray[3], true);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            selectedAnimator.SetBool(animBoolArray[4], true);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            selectedAnimator.SetBool(animBoolArray[5], true);
        }
    }
}
