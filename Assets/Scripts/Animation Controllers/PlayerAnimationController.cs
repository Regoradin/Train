using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

	private Animator animator;
	private CharacterController controller;

	private Transform previous_parent;
	private Vector3 previous_position;
	private Quaternion previous_rotation;

	void Start()
	{
		animator = GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
	}

	void Update () {

		//walking and running animations
		animator.SetFloat("speed_vertical", Input.GetAxis("Vertical"));
		if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Strafe") != 0)
		{
			animator.SetFloat("running", Input.GetAxis("Run"));
		}
		else
		{
			animator.SetFloat("running", 0f);
		}

		//strafing animation
		if(Input.GetAxis("Strafe") != 0)
		{
			animator.SetBool("strafing", true);
			animator.SetFloat("speed_strafe", Input.GetAxis("Strafe"));
		}
		else
		{
			animator.SetBool("strafing", false);
		}

		//turning animation and motion
		if (Input.GetAxis("Horizontal") > 0)
		{
			if (Input.GetAxis("Vertical") == 0f)
			{
				animator.SetBool("turning_right", true);
			}
			transform.Rotate(Vector3.up, 96.4f * Time.deltaTime);
		}
		else
		{
			animator.SetBool("turning_right", false);
		}
		if(Input.GetAxis("Horizontal") < 0)
		{
			if (Input.GetAxis("Vertical") == 0f)
			{
				animator.SetBool("turning_left", true);
			}
			transform.Rotate(Vector3.up, -96.4f * Time.deltaTime);
		}
		else
		{
			animator.SetBool("turning_left", false);
		}

		//falling
		if (!controller.isGrounded)
		{
			animator.SetBool("falling", true);
		}
		else
		{
			animator.SetBool("falling", false);
		}
		if (!controller.isGrounded)
		{
			controller.Move(Physics.gravity);
		}

		//Shooting animation
		if (Input.GetButton("Shoot"))
		{
			animator.SetBool("shoot", true);

			Invoke("StopAction", 1);

		}
	}

	void Equip()
	{
		//gets called by the animator controller when the gun is grabbed in the shooting animation
		
		GameObject weapon = GameObject.FindGameObjectsWithTag("Weapon")[0];

		//stores previous position/rotation/parent so as to reset when unequiping
		previous_parent = weapon.transform.parent;
		previous_position = weapon.transform.localPosition;
		previous_rotation = weapon.transform.localRotation;

		//parents the gun to the hand with the appropriate position/rotation. currently these are magic numbers, maybe fix in the future?
		weapon.transform.parent = GameObject.Find("mixamorig:RightHand").transform;
		weapon.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
		weapon.transform.localPosition = new Vector3(-0f, 0.0106f, 0.0413f);
		
	}

	void Unequip()
	{
		//gets called by the animator controller when gun is replaced in the shooting animation

		GameObject weapon = GameObject.FindGameObjectsWithTag("Weapon")[0];

		//resets the parent/position/rotation to what it was before shooting
		weapon.transform.parent = previous_parent;
		weapon.transform.localPosition = previous_position;
		weapon.transform.localRotation = previous_rotation;
	}

	void StopAction()
	{
		//provides an invokable exit from actions
		animator.SetBool("shoot", false);
	}

	void OnTriggerEnter(Collider other)
	{
		//carriage entering
		if(other.tag == "Carriage")
		{
			transform.parent = other.transform;
		}
	}

}
