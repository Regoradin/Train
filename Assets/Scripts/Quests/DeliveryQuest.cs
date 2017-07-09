using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryQuest : Quest {

	private Store target_station;
	public Store Target_station
	{
		get
		{
			return target_station;
		}
	}

	//probably temporary solution until a proper cargo system is settled on
	private string type;
	private int amount;
	public string Type
	{
		get
		{
			return type;
		}
	}
	public int Amount
	{
		get
		{
			return amount;
		}
		set
		{
			amount = value;
		}
	}

	public DeliveryQuest(string name, string description, Store target_station, string type, int amount)
		:base(name, description)
	{
		this.target_station = target_station;
		this.type = type;
		this.amount = amount;

		target_station.AssociateQuest(this);
	}
}
