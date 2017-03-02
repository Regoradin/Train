using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedController : MonoBehaviour {

	private TrainController train_controller;

	public float break_force;

	void Start () {

		train_controller = GetComponentInParent<TrainController>();

	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			//changes the target speed by an input
			train_controller.target_speed += Input.GetAxis("Train_speed");

			if (Input.GetKeyDown("k"))
			{
				Debug.Log("target_speed: " + train_controller.target_speed);
			}

			if (train_controller.speed > train_controller.target_speed && train_controller.target_speed > 0)
			{
				ApplyBreaks(break_force);
			}
			if (train_controller.speed < train_controller.target_speed && train_controller.target_speed < 0)
			{
				ApplyBreaks(-break_force);
			}
		}
	}

	void ApplyBreaks(float force)
	{
		train_controller.AddForce(-force);
	}
}
