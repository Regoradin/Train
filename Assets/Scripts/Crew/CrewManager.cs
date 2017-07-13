using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;


public class CrewManager : MonoBehaviour {

	private List<Crew> crews;
	private List<Task> tasks;

	private void Start()
	{
		//builds list of all the crew
		crews = new List<Crew>();
		foreach (Crew script in GetComponentsInChildren<Crew>())
		{
			crews.Add(script);
		}

		tasks = new List<Task>();
	}

	public void AddTask(Task task)
	{
		tasks.Add(task);
		AssignTasks();
	}

	public void RemoveTask(Task task)
	{
		if (tasks.Contains(task))
		{
			tasks.Remove(task);
			AssignTasks();
		}
	}

	private void AssignTasks()
	{
		//sorts the task so high-priority (low number) tasks are first in the list, and are asssigned first
		tasks = QuicksortTasks(tasks);

		//currently crew are all identical. eventually have some method of choosing the most qualified among them to assign a task to.
		List<Crew> unbusy_crews = new List<Crew>();
		foreach(Crew crew in crews)
		{
			if (!crew.busy)
			{
				unbusy_crews.Add(crew);
			}
		}

		for(int i  = 0; i < unbusy_crews.Count && i < tasks.Count; i++)
		{
			crews[i].task = tasks[i];
			crews[i].GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem<bool>("has_task", true);
		}
	}

	/// <summary>
	/// Quicksorts a list of tasks by priority.
	/// </summary>
	/// <param name="tasks"></param>
	/// <returns></returns>
	private List<Task> QuicksortTasks(List<Task> tasks)
	{
		if (tasks.Count > 1)
		{

			int pivot_index = tasks.Count / 2;

			List<Task> less_than = new List<Task>();
			List<Task> greater_than = new List<Task>();
			//divide around the pivot.
			foreach (Task task in tasks)
			{
				if (task != tasks[pivot_index])
				{
					if (task.priority <= tasks[pivot_index].priority)
					{
						less_than.Add(task);
					}
					else
					{
						greater_than.Add(task);
					}
				}
			}
			List<Task> result = new List<Task>();
			result.AddRange(QuicksortTasks(less_than));
			result.Add(tasks[pivot_index]);
			result.AddRange(QuicksortTasks(greater_than));

			return result;
		}
		return tasks;
	}
}
