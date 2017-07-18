using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailSegment : MonoBehaviour {

	public RailNode end_node;
	public float distance;
	private string direction;
	public string Direction
	{
		get
		{
			return direction;
		}
		set
		{
			if (value == "left" || value == "center" || value == "right")
			{
				direction = value;
			}
			else
			{
				Debug.Log("Invalid direction " + value + " tried to set as node " + name + " direction");
			}
		}
	}

}
