using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : PlayerItem {

	private GameObject player;
	private float range;
	private float repair_amount;

	public Wrench(GameObject player, float range, float repair_amount)
	{
		this.player = player;
		this.range = range;
		this.repair_amount = repair_amount;
	}

	public override void Use()
	{
		Debug.Log("Using wrench");

		RaycastHit hit;
		if(Physics.Raycast(player.GetComponentInChildren<Camera>().ViewportPointToRay(new Vector3(.5f, .5f, 0)), out hit))
		{
			Damage dam = hit.collider.GetComponent<Damage>();
			if (hit.collider.GetComponent<Damage>() != null)
			{
				dam.Repair(repair_amount);
			}
		}
		
	}

}
