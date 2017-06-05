using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	private Canvas canvas;

	public Text money;
	public Image health;

	private PlayerInventory inventory;

	public float health_offset;

	void Start()
	{
		canvas = GetComponentInParent<Canvas>();

		inventory = GetComponentInParent<PlayerInventory>();
	}

	private void Update()
	{
		money.text = "$" + inventory.Money;

		float health_percent = inventory.Health / inventory.max_health;

		health.rectTransform.sizeDelta = new Vector2((Screen.width - health_offset) * health_percent, health.rectTransform.rect.height);

	}


}
