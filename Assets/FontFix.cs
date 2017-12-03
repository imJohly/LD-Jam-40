using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FontFix : MonoBehaviour {

	public Font[] fonts;

	void Awake()
	{
		for(int i = 0; i < fonts.Length; i++)
		{
			fonts[i].material.mainTexture.filterMode = FilterMode.Point;
		}
	}
}
