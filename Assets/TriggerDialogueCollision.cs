using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueCollision : MonoBehaviour {

	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Player")
		{
			print("Contact!");
			GetComponent<DialogueTrigger>().TriggerDialogue();
		}
	}
}
