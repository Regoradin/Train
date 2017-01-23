using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float move_speed;
	public float look_speed;

	new private Camera camera;
	private Rigidbody rb;

	void Start () {

		camera = GetComponentInChildren<Camera>();
		rb = GetComponent<Rigidbody>();

	}
	
	void Update () {

		//takes player input for moving around
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
		movement *= move_speed;

		//moves the player relative to their orientation, not the world
		movement = transform.TransformVector(movement);

		rb.velocity = movement;

		//takes player input for looking around
		Vector3 look = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0f);
		look *= look_speed;

		//rotates the body for the y-axis rotation and the "head" camera for x-axis
		rb.angularVelocity = look;
		camera.transform.Rotate(Vector3.right, look.x);

	}
}
