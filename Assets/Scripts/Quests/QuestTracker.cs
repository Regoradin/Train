using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour {

	private List<Quest> quests;
	public List<Quest> Quests
	{
		get
		{
			return quests;
		}
	}

	void AddQuest(Quest quest)
	{
		quests.Add(quest);
	}

}
