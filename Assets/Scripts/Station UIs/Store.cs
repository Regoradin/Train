using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : stationUI{

	public GameObject cargo_toggle_prefab;
	public ToggleGroup cargo_toggle_group;

	private CargoController active_cargo;
	private int amount;

	private List<GameObject> carriages_in_station;

	void Start()
	{
		carriages_in_station = new List<GameObject>();
	}

	public override void SetupUI()
	{
		//setting up the GUI button to select the active cargo carriage. Only happens if this is the first carriage in the station, to prevent multiple setups. For some reason two sets of buttons seem to be formed, but that shouldn't affect anything... probably...
		if (carriages_in_station.Count == 0)
		{
			for (int i = 0; i < train.cargo_controllers.Count; i++)
			{
				ToggleSetup(i);
			}
		}
	}

	/// <summary>
	/// Positions, names, and sets up functionality for the cargo toggles. 
	/// </summary>
	/// <param name="i"></param>
	void ToggleSetup(int i)
	{
		GameObject cargo_toggle_object = Instantiate(cargo_toggle_prefab);
		Toggle cargo_toggle = cargo_toggle_object.GetComponent<Toggle>();
		cargo_toggle_object.transform.SetParent(cargo_toggle_group.transform);
		//setting up cargo_toggle
		cargo_toggle_object.GetComponent<RectTransform>().localPosition = new Vector3(0, -i * 20, 0);
		cargo_toggle.group = cargo_toggle_group;
		cargo_toggle.GetComponentInChildren<Text>().text = train.cargo_controllers[i].name;
		cargo_toggle.onValueChanged.AddListener(delegate (bool input)
		{
			if (input == true)
			{
				active_cargo = train.cargo_controllers[i];
			}
		});
	}

	public void AddCargo(string type)
	{
		if (active_cargo)
		{
			//the 1 here is the value, eventually make that dependent on the type of material and the station's individual economy.
			active_cargo.AddCargo(type, amount,1);
		}
	}

	public void RemoveCargo(string type)
	{
		if (active_cargo)
		{
			active_cargo.RemoveCargo(type, amount,1);
		}
	}

	public void SetAmount(int _amount)
	{
		amount = _amount;
	}
}
