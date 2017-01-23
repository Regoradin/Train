using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

	private Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void Update () {

		//walking and running animations
		animator.SetFloat("speed_vertical", Input.GetAxis("Vertical"));
		if (Input.GetAxis("Vertical") != 0)
		{
			animator.SetFloat("running", Input.GetAxis("Run"));
		}
		else
		{
			animator.SetFloat("running", 0f);
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

	}
}
