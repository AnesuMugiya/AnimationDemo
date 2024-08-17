using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;
    int velocityZHash;
    int velocityXHash;

    // Start is called before the first frame update
    void Start()
    {
        // Searches the gameObject this script is attached to to get the animator component
        animator = GetComponent<Animator>();

        // increases performance as Hash values are faster to retrieve than strings
        velocityZHash = Animator.StringToHash("velocity Z");
        velocityXHash = Animator.StringToHash("velocity X");
    }

    // handles acceleration and deceleration
    void changeVelocity( bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity){
        // if player presses forward, increase velocity in the forward z direction
        if (forwardPressed && velocityZ < currentMaxVelocity){
            velocityZ += Time.deltaTime * acceleration;
        }

        // if player presses left, increase velocity in the left -x direction
        if (leftPressed && velocityX > -currentMaxVelocity){
            velocityX -= Time.deltaTime * acceleration;
        }

        // if player presses right, increase velocity in the right x direction
        if (rightPressed && velocityX < currentMaxVelocity){
            velocityX += Time.deltaTime * acceleration;
        }

        // decrease velocityZ
        if (!forwardPressed && velocityZ > 0.0f){
            velocityZ -= Time.deltaTime * deceleration;
        }

        // decrease -velocityX
        if (!leftPressed && velocityX < 0.0f){
            velocityX += Time.deltaTime * deceleration;
        }

        // decrease velocityZ
        if (!rightPressed && velocityX > 0.0f){
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    // handles locking or resetting of velocity
    void lockOrResetVelocity( bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity){
        // lock foward
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity){
            velocityZ = currentMaxVelocity;
        // decelerate to the maximum walk velocity
        } else if (forwardPressed && velocityZ > currentMaxVelocity){
            velocityZ -= Time.deltaTime * deceleration;
            // round to the currentMaxVelocity if within offset
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f)){
                velocityZ = currentMaxVelocity;
            }
        } else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f)){
            velocityZ = currentMaxVelocity;
        }

        // lock left
        if (leftPressed && runPressed && velocityX < -currentMaxVelocity){
            velocityX = -currentMaxVelocity;
        // decelerate to the maximum walk velocity
        } else if (leftPressed && velocityX < -currentMaxVelocity){
            velocityX += Time.deltaTime * deceleration;
            // round to the currentMaxVelocity if within offset
            if (velocityX < -currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f)){
                velocityX = -currentMaxVelocity;
            }
        } else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f)){
            velocityX = -currentMaxVelocity;
        }

        // lock right
        if (rightPressed && runPressed && velocityX > currentMaxVelocity){
            velocityX = currentMaxVelocity;
        // decelerate to the maximum walk velocity
        } else if (rightPressed && velocityX > currentMaxVelocity){
            velocityX -= Time.deltaTime * deceleration;
            // round to the currentMaxVelocity if within offset
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f)){
                velocityX = currentMaxVelocity;
            }
        } else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f)){
            velocityX = currentMaxVelocity;
        }

        // resets velocityZ to 0
        if (!forwardPressed && velocityZ < 0.0f){
            velocityZ = 0.0f;
        }

        // resets velocityX to 0
        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -maximumWalkVelocity && velocityX < maximumWalkVelocity)){
            velocityX = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // When refactoring code, replace characters with their key codes to improve perfomance.
        // input will be true is the player is pressing on the passed in key parameter
        // get key input from the player
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        // set current maxVelocity
        // currentMaxVelocity is equal to either maximumWalkVelocity or maximumRunVelocity depending on if
        // runPressed is true. This is called a turnary operator. It sets the variable to the first option if the 
        // condition is true otherwise it will set it to the value of the second option.

        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        // handle changes in velocity
        changeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        lockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        
        animator.SetFloat(velocityZHash, velocityZ);
        animator.SetFloat(velocityXHash, velocityX); 
       
    }
}
