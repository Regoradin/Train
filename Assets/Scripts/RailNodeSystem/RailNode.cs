using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailNode : MonoBehaviour {

	public List<RailNode> connected_nodes;

	private void OnTriggerEnter(Collider other)
	{
		TrainDriver other_driver = other.GetComponentInParent<TrainDriver>();
		if(other_driver != null)
		{
			other_driver.EnterNode(this);
		}
	}
}
