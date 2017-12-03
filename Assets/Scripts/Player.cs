using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player inst;

	public int health;
	public int chickenAmount;

	void Awake()
	{
		if(inst == null)
			inst = this;
		else
			Destroy(gameObject);
	}
}
