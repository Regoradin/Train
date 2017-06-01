using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	public float max_health;
	private float health;
	public float Health
	{
		get
		{
			return health;
		}
		set
		{
			if (value > max_health)
			{
				health = max_health;
			}
			if(value < 0)
			{
				health = 0;
			}
			else
			{
				health = value;
			}
		}
	}

	private float money = 0;
	public float Money
	{
		get
		{
			return money;
		}
		set
		{
			if (value >= 0)
			{
				money = value;
			}
			else
			{
				money = 0;
			}
		}
	}
}
