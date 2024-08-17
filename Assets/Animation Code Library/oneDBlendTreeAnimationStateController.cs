using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneDBlendTreeAnimationStateController : MonoBehaviour
{
    Animator animator;
    float velocity = 0.0f;
    public float acceleration = 0.1f;
    public float deceleration = 0.5f;
    int velocityHash;

    // Start is called before the first frame update
    void Start()
    {
        // set reference fo animator
        animator = GetComponent<Animator>();

        // increases performance as Hash values are faster to retrieve than strings
        velocityHash = Animator.StringToHash("velocity");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        // if player presses the w key
        if (forwardPressed && velocity < 1.0f){
            // then set the isWalking boolean to be true
            velocity += Time.deltaTime * acceleration;
        }

        // if player isnt pressing the w key
        if (!forwardPressed && velocity > 0.0f){
            // then set the isWalking boolean to be true
            velocity -= Time.deltaTime * deceleration;
        }

        // if player presses the w key
        if (!forwardPressed && velocity < 0.0f){
            // then set the isWalking boolean to be true
            velocity = 0.0f;
        }


        animator.SetFloat(velocityHash, velocity);
       
    }
}
