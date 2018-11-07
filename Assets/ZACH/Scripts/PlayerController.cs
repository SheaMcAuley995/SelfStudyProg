using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MovementData
{
    public float movementSpeed;
    public float backwardSpeed;
    public float jumpForce;
    public const float dampTime = 0.1f;
}

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public MovementData movement;

    // Input variables for movement
    float verticalMovement = 0f;
    float horizontalMovement = 0f;
    bool isGrounded, canJump; // isGrounded is for input, canJump is for applying physics in FixedUpdate

    // Script dependencies and components
    Transform mainCam;
    Transform playerTransform;
    Rigidbody rb;
    Animator animator;

	// Use this for initialization
	void Start ()
    {
        FindComponents(); // See below
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Input check for movement
        verticalMovement = Input.GetAxisRaw("Vertical");
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("PosX", horizontalMovement, MovementData.dampTime, Time.deltaTime);
        animator.SetFloat("PosY", verticalMovement, MovementData.dampTime, Time.deltaTime);
    }

    void FixedUpdate()
    {
        // Vertical movement physics
        #region Up & Down Movement
        // Normal forward movement (normal speed)
        if (verticalMovement > 0)
        {
            playerTransform.position += playerTransform.forward * movement.movementSpeed * Time.deltaTime;
        }

        // Normal backwards movement (slowed)
        if (verticalMovement < 0)
        {
            playerTransform.position -= playerTransform.forward * movement.backwardSpeed * Time.deltaTime;
        }
        #endregion

        // Horizontal movement physics
        #region Sideways Movement
        // Normal right movement (normal speed)
        if (horizontalMovement > 0)
        {
            playerTransform.position += playerTransform.right * movement.movementSpeed * Time.deltaTime;
        }
        // Normal left movement (normal speed)
        if (horizontalMovement < 0)
        {
            playerTransform.position -= playerTransform.right * movement.movementSpeed * Time.deltaTime;
        }
        #endregion

        transform.eulerAngles = mainCam.transform.eulerAngles;
    }

    // Finds components to be applied on Start because IT LOOKS CLEANER UP THERE AND I LIKE IT, OKAY? 
    void FindComponents()
    {
        mainCam = Camera.main.transform;
        playerTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
}
