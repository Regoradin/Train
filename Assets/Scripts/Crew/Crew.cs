using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;

public class Crew : MonoBehaviour {

	private AIRig ai;

	//set to true by outside scripts if the ai is busy doing something and shouldn't be looked at to recieve new jobs
	public bool busy = false;

	public Task task
	{
		set
		{
			//assigns the task to the ai, which then will go and do it
			ai.AI.WorkingMemory.SetItem<string>("type", value.type);
			ai.AI.WorkingMemory.SetItem<GameObject>("task_object", value.task_object);
		}
	}

	private void Start()
	{
		ai = GetComponentInChildren<AIRig>();
	}

}
