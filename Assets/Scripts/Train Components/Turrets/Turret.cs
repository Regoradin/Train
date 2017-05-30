using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public Seat seat;
	public float rotation_speed;

	public float max_rotation_x;
	public float max_roation_y;

	void Update () {
		if (seat.Seated)
		{
			transform.Rotate(0, 0, Input.GetAxis("Mouse Y") * rotation_speed, Space.Self);
			transform.Rotate(0, Input.GetAxis("Mouse X") * rotation_speed, 0, Space.World);

			transform.eulerAngles = new Vector3(0, ClampAngle(transform.eulerAngles.y, -max_rotation_x, max_rotation_x), ClampAngle(transform.eulerAngles.z, -max_roation_y, max_roation_y));


		}
		else
		{
			//reset to normal
			transform.rotation = Quaternion.identity;
		}
	}

	float ClampAngle(float angle, float from, float to)
	{
		if(angle > 180)
		{
			angle = angle - 360;
		}

		return Mathf.Clamp(angle, from, to);
	}
}
