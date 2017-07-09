using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillQuest : Quest {

	private GameObject target;
	public GameObject Target
	{
		get
		{
			return target;
		}
	}

	public KillQuest(string name, string description, GameObject target)
		:base(name, description)
	{
		this.target = target;

		//This will have to be properly implemented once an enemy system exists, but this is the gist.

		//if (target.GetComponent<Enemy>())
		//{
		//	target.GetComponent<Enemy>().questy == true;
		//}
	}

}
