using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour {

	//the track that the train is currently running on
	private GameObject track;
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		rb.velocity = new Vector3(0f, 0f, 5f);
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

	void FixedUpdate()
	{
		rb.AddRelativeForce(Vector3.forward * 10);
		Debug.Log("velocity: " + rb.velocity);
		Debug.Log("velocity magnitutde: " + rb.velocity.magnitude);

		if (track)
		{
			//ensures that the train is rotated to be aligned with the track
			transform.rotation = track.transform.rotation;

			//ensures that the train is only going in the forward direction
			Vector3 local_velocity = Vector3.forward * rb.velocity.magnitude;
			rb.velocity = transform.TransformDirection(local_velocity);

			Debug.Log("local_velocity: " + local_velocity);
		}		
	}

}
