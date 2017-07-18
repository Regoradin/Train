using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDriver : MonoBehaviour {

	private RailNode destination;
	private RailNode current_node;

	private List<RailNode> path;

	//The train controllers on this train, and the one that is ahead of it
	private TrainController train_controller;
	public TrainController next_train_controller;

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
		if (path[0] == current_node)
		{
			path.Remove(node);
		}

		//Determines the index of the next segment, and recalculates the pathfinding if it does not exist
		int next_node_index = -1;
		while(next_node_index == -1)
		{
			foreach(RailSegment segment in node.connected_segments)
			{
				if(segment.end_node == path[0])
				{
					next_node_index = node.connected_segments.IndexOf(segment);
				}
				if(next_node_index == -1)
				{
					Pathfind();
				}
			}
		}
		
		//changes the direction appropriately
		train_controller.Direction = node.connected_segments[next_node_index].Direction;

	}

	private void Pathfind()
	{
		//does an A* pathfinding to find a path, and then updates the path list
	}

}
