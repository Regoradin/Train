using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailNode : MonoBehaviour {

	public List<RailSegment> connected_segments;

	[HideInInspector]
	public TrainController last_train_controller;

	//these will be used by TrainDrivers doing pathfinding, and will be wiped when finished.
	[HideInInspector]
	public float g_score = -1;
	[HideInInspector]
	public float h_score = -1;
	public float total_score
	{
		get
		{
			return g_score + h_score;
		}
	}
	[HideInInspector]
	public RailNode parent_node;

	private void Awake()
	{
		connected_segments = new List<RailSegment>();
		connected_segments.AddRange(GetComponents<RailSegment>());
	}

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

	public void ResetPathfinding()
	{
		g_score = -1;
		h_score = -1;

		parent_node = null;
	}
}
