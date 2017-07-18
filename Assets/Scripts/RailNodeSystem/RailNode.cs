using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailNode : MonoBehaviour {

	public List<RailSegment> connected_segments;

	public TrainController last_train_controller;

	private void OnTriggerEnter(Collider other)
	{
		TrainDriver other_driver = other.GetComponentInParent<TrainDriver>();
		TrainController other_controller = other.GetComponent<TrainController>();


		if(other_driver != null)
		{
			other_driver.next_train_controller = last_train_controller;

			other_driver.EnterNode(this);
		}
		if(other_controller != null)
		{
			last_train_controller = other_controller;
		}

	}
}
