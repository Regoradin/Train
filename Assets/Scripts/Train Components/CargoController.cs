using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoController : MonoBehaviour {

	private Dictionary<string, int> cargoes;    //list of types and amounts of cargo

	private PlayerInventory inv;

	public GameObject crate;    //graphical display of amount of cargo existant
	public int max_amount;
	private float filled_height;  //the height of the crate when it is totally filled
	float total_amount;			  //the total amount of cargo in the thing

	void Start()
	{
		cargoes = new Dictionary<string, int>();

		inv = GameObject.Find("Player").GetComponent<PlayerInventory>();

		filled_height = crate.transform.localScale.y;

		UpdateCarriage();
	}

	/// <summary>
	/// Adds amount units of cargo.
	/// </summary>
	/// <param name="added_cargo"></param>
	public void AddCargo(string added_cargo, int amount, float unit_price)
	{
		float total_price = unit_price * amount;
		if (inv.Money >= total_price && amount + total_amount <= max_amount)
		{
			if (cargoes.ContainsKey(added_cargo))
			{
				cargoes[added_cargo] += amount;
			}
			else
			{
				cargoes[added_cargo] = amount;
			}
			inv.Money -= total_price;
		}
		else
		{
			Debug.Log("You do not have enough money");
		}
		UpdateCarriage();
	}


	/// <summary>
	/// Removes amount units of cargo.
	/// </summary>
	/// <param name="removed_cargo"></param>
	public void RemoveCargo(string removed_cargo, int amount, float unit_price)
	{
		float total_price = amount * unit_price;

		if (cargoes.ContainsKey(removed_cargo))
		{
			if (cargoes[removed_cargo] >= amount)
			{
				cargoes[removed_cargo] -= amount;
				inv.Money += total_price;
			}
			else
			{
				inv.Money += unit_price * cargoes[removed_cargo];
				cargoes[removed_cargo] = 0;
			}
		}
		UpdateCarriage();
	}

	void UpdateCarriage()
	{
		//update the weight and speed and such of the carriage eventually.

		foreach (string key in cargoes.Keys)
		{
			Debug.Log("You now have " + cargoes[key] + " of " + key + " in " + name);
		}

		//temporary solution for visualizing space used based on amount. Eventually add in volume and  stuff so different materials fill up differnt spaces
		foreach (string key in cargoes.Keys)
		{
			total_amount += cargoes[key];
		}
		crate.transform.localScale = new Vector3(crate.transform.localScale.x, filled_height * (total_amount/max_amount), crate.transform.localScale.z);

	}

}
