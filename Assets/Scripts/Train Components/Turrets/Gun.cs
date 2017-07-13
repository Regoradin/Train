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

	private bool reloading = false;

	void Start()
	{
		ammo = round_size;
	}

	/// <summary>
	/// Shoots the bullet if the gun is loaded. If not loaded and not reloading, will reload.
	/// </summary>
	/// <param name="bullet"></param>
	public void Shoot(GameObject bullet)
	{
		if (!reloading)
		{
			if (ammo > 0)
			{
				if (Time.time > shot_reload_time + shot_last_time)
				{
					//this currently starts the bullet in the middle of the gun which causes problems if the collider is turned on and the gun itself is damagable
					GameObject new_bullet = Instantiate(bullet, transform.position, transform.rotation);

					Vector3 velocity = Vector3.forward * muzzle_velocity;
					velocity = transform.TransformDirection(velocity);
					new_bullet.GetComponent<Rigidbody>().velocity = velocity;

					ammo -= 1;
					shot_last_time = Time.time;
				}
			}
			else
			{
				Invoke("Reload", round_reload_time);
				reloading = true;
			}
		}
	}

	public void Reload()
	{
		ammo = round_size;
		reloading = false;
	}

}
