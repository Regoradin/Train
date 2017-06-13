using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler{

	RectTransform rt;

	public GameObject carriage;

	private Vector3 original_position;

	private Dropsystem dropsys;

	private void Start()
	{
		dropsys = GetComponentInParent<Dropsystem>();

		rt = transform as RectTransform;

		original_position = rt.position;
	}

	public void OnBeginDrag(PointerEventData data)
	{
		Debug.Log("beigninsdfo dragging");
	}

	public void OnDrag(PointerEventData data)
	{
		rt.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData data)
	{
		dropsys.Drop(data);
	}

	public void Reset()
	{
		rt.position = original_position;
	}
}
