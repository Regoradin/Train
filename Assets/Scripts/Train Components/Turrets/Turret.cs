using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, IAiInteractable {

	public Seat seat;
	public Gun gun;
	public GameObject bullet;
	public float rotation_speed;

	public float max_rotation_x;
	public float max_roation_y;

	private GameObject ai;

	void Update () {
		if (seat.Seated)
		{
			//Player interaction 
			if (seat.seated_gameobject.CompareTag("Player"))
			{
				transform.Rotate(0, 0, Input.GetAxis("Mouse Y") * rotation_speed, Space.Self);
				transform.Rotate(0, Input.GetAxis("Mouse X") * rotation_speed, 0, Space.World);

				transform.eulerAngles = new Vector3(0, ClampAngle(transform.eulerAngles.y, -max_rotation_x, max_rotation_x), ClampAngle(transform.eulerAngles.z, -max_roation_y, max_roation_y));

				if (Input.GetButton("Fire1"))
				{
					gun.Shoot(bullet);
				}
				if (Input.GetButtonDown("Reload"))
				{
					gun.Reload();
				}
			}
			else
			{
				//If it's not the player it must be a crew, so this is the crew functioning logic spot here.
			}
		}
		else
		{
			//reset to normal
			transform.rotation = Quaternion.identity;
		}

	}

	public void AiInteract(GameObject ai, string type)
	{
		if (seat.TrySeat(ai))
		{
			this.ai = ai;
			ai.GetComponent<Crew>().busy = true;
		}
	}
	
	public void AiRelease()
	{
		seat.TryDeseat();
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
