using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public bool OnStart;
	public Dialogue dialogue;

	void Start()
	{
		if(OnStart)
			TriggerDialogue();
	}

	public void TriggerDialogue()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}
}
