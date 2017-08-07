using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDriver : MonoBehaviour {

	public RailNode TEST_NODE;

	public RailNode destination;
	private RailNode current_node;

	private List<RailNode> path;

	//The train controllers on this train, and the one that is ahead of it
	private TrainController train_controller;
	[HideInInspector]
	public TrainController next_train_controller;

	private void Start()
	{
		path = new List<RailNode>();

		train_controller = GetComponent<TrainController>();

		Debug.Log("Entering node and doing pathfinding");
		EnterNode(TEST_NODE);
		Debug.Log("path is:");
		foreach(RailNode node in path)
		{
			Debug.Log(node.name);
		}
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
		else
		{
			train_controller.target_speed = train_controller.top_speed;
		}

	}

	public void EnterNode(RailNode node)
	{
		//updates the path having passed this node if a path exists. If not, find a path
		current_node = node;
		if (path.Count > 0)
		{
			if (path[0] == current_node)
			{
				path.Remove(node);
			}
		}
		else
		{
			Pathfind();
		}

		//Determines the index of the next segment, and recalculates the pathfinding if it does not exist
		int next_node_index = -1;
		while (next_node_index == -1)
		{
			foreach (RailSegment segment in node.connected_segments)
			{
				if (segment.end_node == path[0])
				{
					next_node_index = node.connected_segments.IndexOf(segment);
				}
			}
			if (next_node_index == -1)
			{
				Pathfind();
			}

		}
		
		
		//changes the direction appropriately
		train_controller.Direction = node.connected_segments[next_node_index].Direction;
		Debug.Log("New direction is: " + train_controller.Direction);
		train_controller.target_speed = train_controller.top_speed;
		Debug.Log("target speed is: " + train_controller.target_speed);

	}

	private void Pathfind()
	{
		List<RailNode> open_list = new List<RailNode>();
		List<RailNode> closed_list = new List<RailNode>();
		RailNode current_node = this.current_node;  //creates a local variable for the current node which shouldn't affect this.current node.


		while(current_node != destination)
		{
			//updates the open and closed lists and updates each nodes scores appropriately
			foreach (RailSegment seg in current_node.connected_segments)
			{
				if (!closed_list.Contains(seg.end_node))
				{
					float new_g_score = current_node.g_score += seg.weighted_distance;
					float new_h_score = Vector3.Distance(seg.end_node.transform.position, destination.transform.position);

					//if there is no score on this node or the new score is lower:
					if ((new_g_score + new_h_score <= seg.end_node.g_score + seg.end_node.h_score) || (seg.end_node.g_score == -1))
					{
						seg.end_node.g_score = new_g_score;
						seg.end_node.h_score = new_h_score;
					}
					open_list.Add(seg.end_node);
				}
			}

			//pick the lowest scoring node, tell it who it's parent is, and continue
			open_list = QuicksortNodes(open_list);
			open_list[0].parent_node = current_node;
			current_node = open_list[0];
			open_list.Remove(current_node);
			closed_list.Add(current_node);

		}
		//once we get to this point, the current node will equal the destination, and each node along the way will have a pointer to the one before it.
		path.Clear();
		while(current_node != this.current_node)
		{
			path.Add(current_node);
			current_node = current_node.parent_node;
		}
		path.Reverse();

	}

	/// <summary>
	/// Quicksorts a list of nodes from low to high by the sum of their g and h scores.
	/// </summary>
	/// <param name="nodes"></param>
	/// <returns></returns>
	private List<RailNode> QuicksortNodes(List<RailNode> nodes)
	{
		if (nodes.Count > 1)
		{

			int pivot_index = nodes.Count / 2;

			List<RailNode> less_than = new List<RailNode>();
			List<RailNode> greater_than = new List<RailNode>();
			//divide around the pivot.
			foreach (RailNode node in nodes)
			{
				if (node != nodes[pivot_index])
				{
					if (node.total_score <= nodes[pivot_index].total_score)
					{
						less_than.Add(node);
					}
					else
					{
						greater_than.Add(node);
					}
				}
			}
			List<RailNode> result = new List<RailNode>();
			result.AddRange(QuicksortNodes(less_than));
			result.Add(nodes[pivot_index]);
			result.AddRange(QuicksortNodes(greater_than));

			return result;
		}
		return nodes;
	}
}
