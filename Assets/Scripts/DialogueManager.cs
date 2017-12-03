using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text dialogueText;

	private Queue<string> sentences;

	void Start()
	{
		sentences = new Queue<string>();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.E))
			DisplayNextSentence();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		dialogueText.gameObject.SetActive(true);

		sentences.Clear();

		foreach(string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if(sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach(char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		dialogueText.gameObject.SetActive(false);
	}
}
