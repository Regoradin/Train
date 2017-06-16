using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageController : MonoBehaviour {

	public float rotation_speed;
	public float empty_mass;

	//the track that the train is currently running on
	private GameObject track;
	private Rigidbody rb;

	[HideInInspector]
	public string direction;

	public Sprite sprite;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.mass = empty_mass;
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
			float angle_diff = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, track.transform.rotation.eulerAngles.y);

			//front facing
			if(angle_diff < 0 && angle_diff > -90)
			{
				transform.Rotate(-rotation_speed * Vector3.up);
			}
			if (angle_diff > 0 && angle_diff < 90)
			{
				transform.Rotate(rotation_speed * Vector3.up);
			}
			//reverse facing
			if(angle_diff > 90 && angle_diff < 180)
			{
				transform.Rotate(-rotation_speed * Vector3.up);
			}
			if(angle_diff < -90 && angle_diff > -180)
			{
				transform.Rotate(rotation_speed * Vector3.up);
			}

			//if the angle between the train and the track is smaller than the rotation speed...
			if (Mathf.Abs(angle_diff) <= rotation_speed)
			{
				//line up the rotation of the train and the track
				transform.rotation = track.transform.rotation;

				//and center it in the local x direction by setting it to have 0 x position local to the track
				Vector3 position_to_track = track.transform.InverseTransformPoint(transform.position);
				position_to_track = Vector3.Scale(position_to_track, new Vector3(0f, 1f, 1f));
				transform.position = track.transform.TransformPoint(position_to_track);
			}
			//does the same thing as the above block, but for when the train is pointing in the reverse direction.
			if (Mathf.Abs(Mathf.Abs(angle_diff) - 180) <= rotation_speed)
			{
				//line up the rotation of the train and the track but in reverse direction
				transform.eulerAngles = track.transform.eulerAngles + (Vector3.up * 180);

				//and center it in the local x direction by setting it to have 0 x position local to the track
				Vector3 position_to_track = track.transform.InverseTransformPoint(transform.position);
				position_to_track = Vector3.Scale(position_to_track, new Vector3(0f, 1f, 1f));
				transform.position = track.transform.TransformPoint(position_to_track);
			}

			//ensures that the train is only going in the forward axis, multiplied by the sign of the z axis, which should (unless something goes horribly wrong) be the same direction as the train in general
			Vector3 local_velocity = Vector3.forward * rb.velocity.magnitude * Mathf.Sign(transform.InverseTransformDirection(rb.velocity).z);
			rb.velocity = transform.TransformDirection(local_velocity);
		}		
	}

}
