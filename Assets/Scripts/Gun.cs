using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public Seat seat;
	public GameObject bullet;

	public float fire_speed;
	public float muzzle_velocity;

	void Update()
	{
		if (seat.Seated)
		{
			if (Input.GetKey("mouse 0"))
			{
				Shoot(bullet);
			}
		}
	}

	void Shoot(GameObject bullet)
	{
		GameObject new_bullet = Instantiate(bullet, transform.position + Vector3.forward * 20, transform.rotation);

		new_bullet.GetComponent<Rigidbody>().velocity = muzzle_velocity * Vector3.forward;

	}

}
