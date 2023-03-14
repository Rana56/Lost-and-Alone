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
    public float jumpSpeed = 6f;
	public float gravity = 9.8f;
	
    private CharacterController controller;
	Animator character;
	private int walkingID;
	private int isJumpID;
    private int isMoveID;
    private int isFallID;
    private int isGroundID;
    
    private bool isGrounded;
    private bool isJumping;
    private bool isFalling;
    public float jumpDelay;            //Jump button delays to see if character jumped recently - pressing button to late or eary will still make character jump
	private float? lastGroundedTime;    //field is null-able so variable can hold float or null 
    private float? jumpButtonPressed;


	void Awake()
	{
		controller = GetComponent<CharacterController>();
		character = GetComponentInChildren<Animator>();
		walkingID = Animator.StringToHash("walkingSpeed");
		isJumpID = Animator.StringToHash("isJump");
        isMoveID = Animator.StringToHash("isMove");
        isFallID = Animator.StringToHash("isFall");
        isGroundID = Animator.StringToHash("isGrounded");
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

        moveVelocity.y -= gravity * Time.deltaTime;         // Apply gravity

        if (controller.isGrounded){
            lastGroundedTime = Time.time;       // number of seconds since game started - last time grounded
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            jumpButtonPressed = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpDelay)              //checks if character was on the ground within a grace period
        {
            moveVelocity.y = -5f;
            character.SetBool(isGroundID, true);
            isGrounded = true;
            character.SetBool(isJumpID, false);
            isJumping = false;
            character.SetBool(isFallID, false);

            if (Time.time - jumpButtonPressed <= jumpDelay){
                /*
                if (currentSpeed == 0){
					StartCoroutine(JumpBuffer());
                }
                else {
                    character.SetTrigger(isJumpID);
                    moveVelocity.y = jumpSpeed;
                }
                Debug.Log("jump - " + controller.isGrounded);
                */
                moveVelocity.y = jumpSpeed;
                character.SetBool(isJumpID, true);
                isJumping = true;

                lastGroundedTime = null;
                jumpButtonPressed = null; 
            }
        } 
        else {
            character.SetBool(isGroundID, false);
            isGrounded = false;

            //check if character is falling - transition when jumping up to down - when falling of ledge
            if((isJumping && moveVelocity.y < 0) || (moveVelocity.y < -2)){
                character.SetBool(isFallID, true);
            }
        }

        /*
        if (controller.isGrounded && moveVelocity.y < 0)
        {
            moveVelocity.y = -5f;
        }*/

        //Check if there are inputs to move
        if(direction.magnitude >= 0.1f){
            character.SetBool(isMoveID, true);
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
            character.SetBool(isMoveID, false);
        }       

        /*
        if (Input.GetKeyDown(KeyCode.Space)){
            if (controller.isGrounded)
            {
                if (Mathf.Abs(currentSpeed) < 0.1){
					StartCoroutine(JumpBuffer());
                }
                else {
                    character.SetTrigger(isJumpID);
                    moveVelocity.y = jumpSpeed;
                }
                
            } 
            Debug.Log("jump outside - " + controller.isGrounded);
        }*/

        controller.Move(moveVelocity * Time.deltaTime);   // Movement for jump
        character.SetFloat(walkingID, currentSpeed, 0.05f, Time.deltaTime);

    }

    IEnumerator JumpBuffer ()
	{
		character.SetTrigger(isJumpID);
		yield return new WaitForSeconds(1.0f);
		moveVelocity.y = jumpSpeed;
	}
}
