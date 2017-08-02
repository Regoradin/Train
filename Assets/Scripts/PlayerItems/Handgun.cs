using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : PlayerItem {

	public override void Activate()
	{
		//Some logic here for triggering the pull out gun animation and all that
		Debug.Log("Pulling out gun");
	}

	public override void Use()
	{
		Debug.Log("bang! shooting!");
	}

	public override void Deactivate()
	{
		Debug.Log("putting away gun");
	}

}
