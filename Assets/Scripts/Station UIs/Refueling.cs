using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Refueling : stationUI {

	public InputField amount_input;
	public float coal_cost;

	private float amount;
	private Engine engine;

	private HashSet<char> numbers;

	private void Start()
	{
		numbers = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.' };
	}

	public override void SetupUI()
	{
		engine = train.GetComponentInChildren<Engine>();
	}
	
	/// <summary>
	/// Buys an amount of coal specified by the input field
	/// </summary>
	public void BuyCoal()
	{
		if(amount < engine.max_coal - engine.coal)
		{
			if(amount * coal_cost < inv.Money)
			{
				engine.coal += amount;
				inv.Money -= amount * coal_cost;
			}
			else
			{
				engine.coal += inv.Money / coal_cost;
				inv.Money = 0;
			}
		}
		else
		{
			FillCoal();
		}
	}

	/// <summary>
	/// Fills the engine completely with coal (or as much as the player can afford)
	/// </summary>
	public void FillCoal()
	{
		amount = engine.max_coal - engine.coal;
		if(amount * coal_cost <= inv.Money)
		{
			inv.Money -= amount * coal_cost;
			engine.coal = engine.max_coal;
		}
		else
		{
			engine.coal += inv.Money / coal_cost;
			inv.Money = 0;
		}
	}

	/// <summary>
	/// Sets the amount of coal to buy, called after editing the input field.
	/// </summary>
	public void SetAmount()
	{
		string clean_input = "";

		foreach (char chr in amount_input.text)
		{
			if (numbers.Contains(chr))
			{
				clean_input += chr;
			}
		}
		amount_input.text = clean_input;
		if (clean_input != "")
		{
			amount = float.Parse(clean_input);
		}
		else
		{
			amount = 0;
		}
	}
}
