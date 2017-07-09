using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {

	new public string name;
	public string description;
	public float reward = 0;	//for the mean time rewards are purely monetary, maybe something else in the future

	public Quest(string name, string description)
	{
		this.name = name;
		this.description = description;
	}

	protected void Complete()
	{
		GameObject.Find("Player").GetComponent<PlayerInventory>().Money += reward;
		Destroy(this);
	}

}
