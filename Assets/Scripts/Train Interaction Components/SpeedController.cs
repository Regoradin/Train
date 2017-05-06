using System;
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

			//applying breaks
			if (train_controller.local_speed > train_controller.target_speed && train_controller.target_speed > 0)
			{
				ApplyBreaks(break_force);
			}
			if (train_controller.local_speed < train_controller.target_speed && train_controller.target_speed < 0)
			{
				ApplyBreaks(-break_force);
			}
			if(train_controller.local_speed > 0 && train_controller.target_speed < 0)
			{
				ApplyBreaks(break_force);
			}
			if(train_controller.local_speed < 0 && train_controller.target_speed > 0)
			{
				ApplyBreaks(-break_force);
			}
		}
	}
	/// <summary>
	/// Applies a breaking force to the train. Note that the sign of this force will be opposite of the direction of the force given, e.g. a positive value for this fucntion will apply a force to the train in the negative direction
	/// </summary>
	/// <param name="force"></param>
	void ApplyBreaks(float force)
	{
		train_controller.AddForce(-force);
	}
}
