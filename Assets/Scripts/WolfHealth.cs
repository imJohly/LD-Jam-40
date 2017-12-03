using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfHealth : MonoBehaviour {

	public int maxHealth;
	private int curHealth;

	void Start()
	{
		curHealth = maxHealth;
	}

	void Update()
	{
		if(curHealth <= 0)
		{
			GetComponent<AIWolf>().curState = AIWolf.State.Retreat;
		}
	}

	public void TakeDamage(int amount)
	{
		curHealth -= amount;
	}
}
