using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchBoxAtk : MonoBehaviour {

	private bool triggered;

	void Update()
	{
		if(GetComponent<Collider>().enabled == false)
			triggered = false;
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Wolf" && triggered == false)
		{
			var heading = new Vector3(col.transform.position.x, 0, col.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z);
			var distance = heading.magnitude;
			var direction = heading / distance;

			direction *= 15;
			direction.y = 3;
			
			col.GetComponent<WolfHealth>().TakeDamage(1);
			col.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
			col.GetComponent<Rigidbody>().drag = 0;

			triggered = true;
		}
	}
}
