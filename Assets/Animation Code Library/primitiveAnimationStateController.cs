using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class primitiveAnimationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        // if player presses the w key
        if (forwardPressed && !isWalking){
            // then set the isWalking boolean to be true
            animator.SetBool(isWalkingHash, true);
        }

        if (!forwardPressed && isWalking){
            // then set the isWalking boolean to be true
            animator.SetBool(isWalkingHash, false);
        }

        if (!isRunning && (forwardPressed && runPressed)){
            // then set the isWalking boolean to be true
            animator.SetBool(isRunningHash, true);
        }

        if (isRunning && (!forwardPressed || !runPressed)){
            // then set the isWalking boolean to be true
            animator.SetBool(isRunningHash, false);
        }
    }
}
