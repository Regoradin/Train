using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	private Canvas canvas;

	public Text money;
	public Image health;
	public Text quests;

	private PlayerInventory inventory;
	private QuestTracker quest_tracker;

	public float health_offset;

	public Store target_station;

	void Start()
	{
		canvas = GetComponentInParent<Canvas>();

		inventory = GetComponentInParent<PlayerInventory>();

		quest_tracker = GetComponentInParent<QuestTracker>();
	}

	private void Update()
	{
		money.text = "$" + inventory.Money;

		float health_percent = inventory.Health / inventory.max_health;

		health.rectTransform.sizeDelta = new Vector2((Screen.width - health_offset) * health_percent, health.rectTransform.rect.height);

		quests.text = SetupQuests();
	}

	private string SetupQuests()
	{
		string result = "Quests";

		foreach (Quest quest in quest_tracker.Quests)
		{
			if (!quest.completed)
			{
				result += "\n\n<size=18>" + quest.name + "</size>\n";
				result += quest.description;
			}
		}

		return result;
	}

	public void AddQuest()
	{
		quest_tracker.AddQuest(new DeliveryQuest("Test Quest", "to test questing", target_station, "wood", 10));
	}


}
