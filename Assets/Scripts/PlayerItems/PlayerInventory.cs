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
				//rounds money to the nearest .01
				money = Mathf.Round(value * 100)/100;
			}
			else
			{
				money = 0;
			}
		}
	}

	//probably will be replaced with a real system
	public float initial_money;

	private PlayerItem active_item;
	public Dictionary<string, PlayerItem> item_inventory;

	void Start()
	{
		money = initial_money;
		item_inventory = new Dictionary<string, PlayerItem>
		{
			{"primary_weapon", new Wrench(GameObject.Find("Player"), 100, 1) }
		};


	}

	void Update()
	{
		if (Input.GetButtonDown("Use"))
		{
			if (active_item != null)
			{
				active_item.Use();
			}
		}

		if (Input.GetKeyDown("1"))
		{
			TrySwitchActive("primary_weapon");
		}
	}

	/// <summary>
	/// attempts to switch the item at the given item slot to the active item. if it already is the active item, deactivates it
	/// </summary>
	/// <param name="key"></param>
	private void TrySwitchActive(string key)
	{
		if (active_item != item_inventory[key])
		{
			active_item = item_inventory[key];
			active_item.Activate();
		}
		else
		{
			active_item.Deactivate();
			active_item = null;
		}
	}


}
