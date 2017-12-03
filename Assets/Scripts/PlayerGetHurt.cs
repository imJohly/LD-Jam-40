using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetHurt : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Wolf" && col.GetComponent<AIWolf>().curState == AIWolf.State.Attack)
		{
			FindObjectOfType<PlayerHealth>().TakeDamage(1);
			AudioManager.instance.Play("Hurt");
		}
	}
}
