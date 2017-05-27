using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public Seat seat;
	public GameObject bullet;

	public float shot_reload_time;
	private float shot_last_time = 0;

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
		if (Time.time > shot_reload_time + shot_last_time)
		{
			//this currently starts the bullet in the middle of the gun which causes problems if the collider is turned on, roundabout solutions seem like the way to go
			GameObject new_bullet = Instantiate(bullet, transform.position, transform.rotation);

			Vector3 velocity = Vector3.forward * muzzle_velocity;
			velocity = transform.TransformDirection(velocity);

			new_bullet.GetComponent<Rigidbody>().velocity = velocity;

			new_bullet.GetComponent<Bullet>().Invoke("ActivateCollider", .5f);

			shot_last_time = Time.time;
		}
	}

}
