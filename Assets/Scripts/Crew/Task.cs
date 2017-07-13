using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task{

	public int priority;
	public string type;
	public GameObject task_object;

	public Task(GameObject task_object, string type)
	{
		this.task_object = task_object;
		this.type = type;
		priority = CalculatePriority();
	}
	public Task(GameObject task_object, string type, int priority)
	{
		this.task_object = task_object;
		this.type = type;
		this.priority = priority;
	}

	private int CalculatePriority()
	{
		//do some mathmagic in here
		return 1;
	}

}
