using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public Seat seat;
	public GameObject bullet;

	public float shot_reload_time;
	private float shot_last_time = 0;

	public float round_size;
	private float ammo;
	public float round_reload_time;

	public float muzzle_velocity;

	void Start()
	{
		ammo = round_size;
	}

	void Update()
	{
		if (seat.Seated)
		{
			if (Input.GetButton("Fire1"))
			{
				if (ammo > 0)
				{
					Shoot(bullet);
				}
				else
				{
					Debug.Log("Need to reload!");
				}
			}
			if (Input.GetButtonDown("Reload"))
			{
				Debug.Log("Reloading");
				//this will probably have to change to incorporate animations
				Invoke("Reload", round_reload_time);
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

			ammo -= 1;
			shot_last_time = Time.time;
		}
	}

	void Reload()
	{
		ammo = round_size;
		Debug.Log("Reloaded");
	}

}
