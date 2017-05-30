using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

	public float max_health;

	private float health;
	private float Health
	{
		get
		{
			return health;
		}
		set
		{
			Debug.Log(name + value);
			health = value;
			if (health <= 0)
			{
				Destroy(gameObject);
			}
		}
	}

	public float damage = -1;

	void Start()
	{
		health = max_health;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Damage>())
		{
			float other_damage = other.GetComponent<Damage>().damage;

			//if the other's damage is -1, it will never be destroyed and destroy everything it touches.
			if(other_damage == -1)
			{
				Destroy(gameObject);
			}
			else
			{
				if(damage != -1)
				{
					Health = health - other_damage;
				}
			}
		}
	}

}
