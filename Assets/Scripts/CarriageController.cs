using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageController : MonoBehaviour {

	public float rotation_speed;

	//the track that the train is currently running on
	private GameObject track;
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	//finds the next track that the train is on
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
		if (track)
		{
			//ensures that the train is rotated to be aligned with the track
			//adjusts rotation by rotation_speed to smoothly match track rotation
			if(Mathf.DeltaAngle(transform.eulerAngles.y, track.transform.eulerAngles.y) < 0)
			{
				transform.Rotate(-rotation_speed * Vector3.up);

			}
			if (Mathf.DeltaAngle(transform.eulerAngles.y, track.transform.eulerAngles.y) > 0)
			{
				transform.Rotate(rotation_speed * Vector3.up);

			}
			//if the angle between the train and the track is smaller than the rotation speed...
			if (Mathf.DeltaAngle(transform.eulerAngles.y, track.transform.eulerAngles.y) > -rotation_speed && Mathf.DeltaAngle(transform.eulerAngles.y, track.transform.eulerAngles.y) < rotation_speed)
			{
				//line up the rotation of the train and the track
				transform.rotation = track.transform.rotation;

				//and center it in the local x direction by setting it to have 0 x position local to the track
				Vector3 position_to_track = track.transform.InverseTransformPoint(transform.position);
				position_to_track = Vector3.Scale(position_to_track, new Vector3(0f, 1f, 1f));
				transform.position = track.transform.TransformPoint(position_to_track);

			//	Debug.Log(name + " lined up with " + track.name + " at position " + position_to_track);
			}



			//ensures that the train is only going in the forward direction
			Vector3 local_velocity = Vector3.forward * rb.velocity.magnitude;
			rb.velocity = transform.TransformDirection(local_velocity);
		}		
	}

}
