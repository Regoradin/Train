using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour, IAiInteractable {

	private TrainController train_controller;

	public float max_health;

	private float health;
	public float Health
	{
		get
		{
			return health;
		}
		set
		{
			health = value;
			if (health <= 0)
			{
				health = 0;
				Break();
			}
			if (health >= max_health)
			{
				health = max_health;
			}
			else if (reparable)
			{
				//if the thing is now damaged, call for repairs
				train_controller.crew_manager.AddTask(new Task(gameObject, "Repair", 2));
			}
		}
	}

	public float damage = -1;
	public bool destroy_on_break = false;
	public bool reparable = true;

	[HideInInspector]
	public bool broken = false;

	void Awake()
	{
		health = max_health;
		train_controller = GetComponentInParent<TrainController>();

		if(damage == -1)
		{
			Debug.Log("Damage on " + name + " is set to -1");
		}
	}

	private void OnTriggerEnter(Collider other)
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

	public void AiInteract(GameObject ai, string type)
	{
		float amount = 10;//this is where you will get the repair ability of the ai.
		Repair(amount);
	}

	public void Repair(float amount)
	{
		if (reparable)
		{
			Health += amount;
		}
	}

	public void Break()
	{
		broken = true;
		if (destroy_on_break)
		{
			Destroy(gameObject);
			return;
		}
		train_controller.crew_manager.AddTask(new Task(gameObject, "Repair", 1));
	}
}
