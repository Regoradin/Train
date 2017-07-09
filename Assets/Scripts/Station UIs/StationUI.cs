using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class stationUI : MonoBehaviour {
	[HideInInspector]
	public TrainController train;

	public GameObject UI;

	[HideInInspector]
	public PlayerInventory inv;

	public Text map_label;

	protected List<Quest> quests;


	private void Start()
	{
		inv = GameObject.Find("Player").GetComponent<PlayerInventory>();
	}

	/// <summary>
	/// This is called whenever a train enters a station, and should set up any procedurally generated UI elements.
	/// </summary>
	public abstract void SetupUI();

	/// <summary>
	/// Associates a quest with this station
	/// </summary>
	public void AssociateQuest(Quest quest)
	{
		quests.Add(quest);
		map_label.color = new Color(1, .549f, 0);
	}
	/// <summary>
	/// Finishes a quest already associated with this station
	/// </summary>
	/// <param name="quest"></param>
	public void CompleteQuest(Quest quest)
	{
		quests.Remove(quest);
		if(quests.Count == 0)
		{
			map_label.color = new Color(50, 50, 50);
		}

		quest.Complete();
		quest = null;
	}


	/// <summary>
	/// Launches the train from the station by setting the target speed to the given value.
	/// </summary>
	/// <param name="speed"></param>
	public void Launch(float speed)
	{
		train.target_speed = speed;
	}

}
