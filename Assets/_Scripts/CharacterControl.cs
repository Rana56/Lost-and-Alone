using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour 
{
	public bool IsGrounded { get {return characterController.isGrounded;}}

	public float speed = 5.0f;
	public float jumpSpeed = 6.0f;
	public float gravity = 10.0f;
	public float rotationSpeed = 100.0f;
	public float currentSpeed = 0.0f;
	public float maxSpeed = 7.0f;
	public float acceleration = 20.0f;
	public float decceleration = 40.0f;
	[SerializeField] private bool m_IsWalking;

	private Vector3 moveDirection = Vector3.zero;
	private CharacterController characterController;
	Animator character;
	public int walkingID;
	public int jumpID;
	public int runJumpID;

	void Awake()
	{
		characterController = GetComponent<CharacterController>();
		character = GetComponent<Animator>();
		walkingID = Animator.StringToHash("walkingSpeed");
		jumpID = Animator.StringToHash("jump");
		runJumpID = Animator.StringToHash("runJump");
	}

	void Update(){
	    // if we are on the ground then allow movement
	    if (IsGrounded){
			float input = Input.GetAxis("Vertical");
	        bool  isMoving = (input != 0);

			moveDirection.x = transform.forward.x;
			moveDirection.z = transform.forward.z;
	        
			if (isMoving){
				//m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
				//currentSpeed = m_IsWalking ? speed : maxSpeed;
				currentSpeed += ((acceleration * input) * Time.deltaTime);
				currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
			}
			else if (currentSpeed > 0){
				currentSpeed -= (decceleration * Time.deltaTime);
				currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
			}
			else if (currentSpeed < 0){
				currentSpeed += (decceleration * Time.deltaTime);
				currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, 0);
			}

			moveDirection.x *= currentSpeed;
			moveDirection.z *= currentSpeed;

			moveDirection.y  = Mathf.Max(0, moveDirection.y);

			if (Input.GetButton("Jump") && currentSpeed == 0) 
			{
				moveDirection.y = jumpSpeed;
				character.SetTrigger(jumpID);	
				Debug.Log("Jump");
			}
			else if (Input.GetButton("Jump") && currentSpeed > 0){
				moveDirection.y = jumpSpeed;
				character.SetTrigger(runJumpID);
				Debug.Log("RunJump");
			}
	    }

		else {
			moveDirection.y -= (gravity * Time.deltaTime);
		}
		
		float rotation = (Input.GetAxis("Horizontal") * rotationSpeed) * Time.deltaTime;
	    transform.Rotate(0, rotation, 0);

		characterController.Move(moveDirection * Time.deltaTime);
		character.SetFloat(walkingID, currentSpeed);

	}
}
