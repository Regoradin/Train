using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDriver : MonoBehaviour {

	private RailNode destination;
	private RailNode current_node;

	private List<RailNode> path;

	//The train controllers on this train, and the one that is ahead of it
	private TrainController train_controller;
	private TrainController next_train_controller;

	private void Start()
	{
		path = new List<RailNode>();

		train_controller = GetComponent<TrainController>();
	}

	private void FixedUpdate()
	{
		if(next_train_controller != null)
		{
			//keeps the target speed lower than the train in front of it to prevent collisions
			if(next_train_controller.target_speed > train_controller.target_speed)
			{
				train_controller.target_speed = next_train_controller.target_speed;
			}
		}

		
	}

	public void EnterNode(RailNode node)
	{
		//updates the path having passed this node
		current_node = node;
		if(path[0] == current_node)
		{
			path.Remove(node);
		}

		if (node.connected_nodes.Contains(path[0]))
		{
			//figure out direction to get to next node here.
		}
	}

	private void Pathfind()
	{
		//does an A* pathfinding to find a path, and then updates the path list
	}

}
