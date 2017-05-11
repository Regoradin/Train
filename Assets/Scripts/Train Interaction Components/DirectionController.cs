using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionController : MonoBehaviour {

	private TrainController train_controller;

	public string initial_direction;

	private Dictionary<string, float> angles;

	void Start() {
		angles = new Dictionary<string, float>();
		train_controller = GetComponentInParent<TrainController>();


		angles["left"] = 45;
		angles["center"] = 0;
		angles["right"] = -45;

		ChangeDirection(initial_direction);
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player")
		{
			if (Input.GetButtonDown("train_left"))
			{
				Debug.Log("lefting");
				if (train_controller.Direction == "right")
				{
					ChangeDirection("center");
				}
				else if(train_controller.Direction == "center")
				{
					ChangeDirection("left");
				}
			}
			if (Input.GetButtonDown("train_right"))
			{
				Debug.Log("righting");
				if (train_controller.Direction == "left")
				{
					ChangeDirection("center");
				}
				else if(train_controller.Direction == "center")
				{
					ChangeDirection("right");
				}
			}
		}
	}

	void ChangeDirection(string direction)
	{
		//sets the direction on the train_controller
		train_controller.Direction = direction;

		//update graphics
		transform.eulerAngles = angles[direction] * Vector3.forward;

	}
}
