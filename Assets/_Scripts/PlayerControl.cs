using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform cam;
    public float speed = 5f;
    public float runSpeed = 10f;
    private bool isRunning;
    public float currentSpeed;
    public float smoothTurnTime = 0.1f;
    private float turnVelocity;

    private Vector3 direction;
    private Vector3 movementDirection;
    private Vector3 moveVelocity = Vector3.zero;
    public float jumpSpeed = 60f;
	public float gravity = 9.8f;
	
    private CharacterController controller;
	Animator character;
	public int walkingID;
	public int jumpID;
	public int runJumpID;

	void Awake()
	{
		controller = GetComponent<CharacterController>();
		character = GetComponentInChildren<Animator>();
		walkingID = Animator.StringToHash("walkingSpeed");
		jumpID = Animator.StringToHash("jump");
		runJumpID = Animator.StringToHash("runJump");
	}

    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        // Move the player using the WASD keys
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0, vertical);       //Move in x and z axis, not y
        direction.Normalize();      //So going daiagonally doesn't increase speed     

        //Check if there are inputs to move
        if(direction.magnitude >= 0.1f){
            //check if shift pressed
            isRunning = !Input.GetKey(KeyCode.LeftShift);
            currentSpeed = isRunning ? speed : runSpeed;

            //Rotate character in y axis to point in the give direction
            float tAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;        //camera.eurler.y makes character traver in camera direction
            //Smooths turns  
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, tAngle, ref turnVelocity, smoothTurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            movementDirection = Quaternion.Euler(0f, tAngle, 0f) * Vector3.forward;              //gives direction to movement considering camera
            controller.Move(movementDirection.normalized * currentSpeed * Time.deltaTime);            //Moves characted independent of frame rate
        } 
        else {
            currentSpeed = 0;
        }       

        if (controller.isGrounded && moveVelocity.y < 0)
        {
            moveVelocity.y = 0f;
        }

        // Jump when the spacebar is pressed and the player is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            //character.SetTrigger(jumpID);
            moveVelocity.y = jumpSpeed;
            Debug.Log("jump- " + controller.isGrounded);
        }
        moveVelocity.y -= gravity * Time.deltaTime;         // Apply gravity
        controller.Move(moveVelocity * Time.deltaTime);   // Move the player
              
        character.SetFloat(walkingID, currentSpeed);
    }
}
