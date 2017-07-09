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
	public QuestTracker quests;

	public Text map_label;

	private bool questy = false;
	public bool Questy
	{
		get { return questy; }
		set
		{
			questy = value;
			if (value)
			{
				//sets the map label to an orange color if it is a questy location
				map_label.color = new Color(1, .549f, 0);
			}
			else
			{
				//the default
				map_label.color = new Color(50, 50, 50);
			}
		}
	}


	private void Start()
	{
		inv = GameObject.Find("Player").GetComponent<PlayerInventory>();
		quests = GameObject.Find("Player").GetComponent<QuestTracker>();
	}

	/// <summary>
	/// This is called whenever a train enters a station, and should set up any procedurally generated UI elements.
	/// </summary>
	public abstract void SetupUI();


	/// <summary>
	/// Launches the train from the station by setting the target speed to the given value.
	/// </summary>
	/// <param name="speed"></param>
	public void Launch(float speed)
	{
		train.target_speed = speed;
	}

}
