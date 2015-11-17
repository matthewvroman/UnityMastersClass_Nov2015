using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public static class Extensions{

	public static void DestroyAllChildren(this Transform transform)
	{
		while(transform.childCount > 0)
		{
			Transform child = transform.GetChild(0);
			child.SetParent(null);
			GameObject.Destroy(child);
		}
	}
}
