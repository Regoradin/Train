using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest {

	public string name;
	public string description;
	public float reward = 0;	//for the mean time rewards are purely monetary, maybe something else in the future

	public Quest(string name, string description)
	{
		this.name = name;
		this.description = description;
	}

	public void Complete()
	{
		GameObject.Find("Player").GetComponent<PlayerInventory>().Money += reward;
	}

}
