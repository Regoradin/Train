using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem{

	public Image icon;
	public GameObject game_object;
	public PlayerInventory inv;

	public virtual void Activate() {
		Debug.Log("Activating ");
	}
	public virtual void Use() { }
	public virtual void Deactivate() {
		Debug.Log("Deactivateing");
	}
}
