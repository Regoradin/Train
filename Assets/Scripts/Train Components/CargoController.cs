using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoController : MonoBehaviour {

	private Dictionary<string, int> cargoes;    //list of types and amounts of cargo

	void Start()
	{
		cargoes = new Dictionary<string, int>();
	}

	/// <summary>
	/// Adds amount units of cargo.
	/// </summary>
	/// <param name="added_cargo"></param>
	public void AddCargo(string added_cargo, int amount)
	{
		if (cargoes.ContainsKey(added_cargo))
		{
			cargoes[added_cargo] += amount;
		}
		else
		{
			cargoes[added_cargo] = amount;
		}

		UpdateCarriages();
	}


	/// <summary>
	/// Removes amount units of cargo.
	/// </summary>
	/// <param name="removed_cargo"></param>
	public void RemoveCargo(string removed_cargo, int amount)
	{
		if (cargoes.ContainsKey(removed_cargo))
		{
			cargoes[removed_cargo] -= amount;
		}
		if(cargoes[removed_cargo] < amount)
		{
			//clears the dictionary of any nonexistant cargoes
			cargoes.Remove(removed_cargo);
		}

		UpdateCarriages();
	}

	void UpdateCarriages()
	{
		//updates the weight and speed and such of the various carriages. This is probably where stuff like cargo space and the like will be dealt with as well

		foreach (string key in cargoes.Keys)
		{
			Debug.Log("You now have " + cargoes[key] + " of " + key);
		}
	}
	
}
