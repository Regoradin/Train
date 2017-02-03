using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour {

	public float power;
	public float top_speed;
	public float rotation_speed;

	//the track that the train is currently running on
	private GameObject track;
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		//rb.velocity = new Vector3(0f, 0f, speed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Track")
		{
			track = other.gameObject;
		}

	}

	//if no new track is selected when leaving a track, clear the track and act with physics normally
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Track")
		{
			if (track == other.gameObject){
				track = null;
			}
		}
	}

	void Update()
	{
		Debug.Log("speed: " + rb.velocity.magnitude);
		if (rb.velocity.magnitude < top_speed)
		{
			rb.AddRelativeForce(power * Vector3.forward);
		}

		if (track)
		{
			//ensures that the train is rotated to be aligned with the track
			//transform.rotation = track.transform.rotation;

			//adjusts rotation by rotation_speed to smoothly match track rotation

			if(Mathf.DeltaAngle(transform.eulerAngles.y, track.transform.eulerAngles.y) < 0)
			{
				transform.Rotate(-rotation_speed * Vector3.up);
			}
			if (Mathf.DeltaAngle(transform.eulerAngles.y, track.transform.eulerAngles.y) > 0)
			{
				transform.Rotate(rotation_speed * Vector3.up);
			}



			//ensures that the train is only going in the forward direction
			Vector3 local_velocity = Vector3.forward * rb.velocity.magnitude;
			rb.velocity = transform.TransformDirection(local_velocity);
		}		
	}

}
