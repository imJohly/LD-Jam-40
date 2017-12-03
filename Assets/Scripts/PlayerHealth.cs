using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int curHealth;
	[Space]
	public Image HeartUI;
	public Sprite[] heartSprites;
	public Text chickenText;
	[Space]
	public GameObject deadScreen;
	public GameObject normScreen;

	Player player;

	void Start()
	{
		deadScreen.SetActive(false);
		normScreen.SetActive(true);

		player = Player.inst;
		curHealth = player.health;
	}

	bool done;
	bool done2;
	bool done3;
	bool done4;
	void Update()
	{
		if(curHealth <= 0 && done == false)
		{
			deadScreen.SetActive(true);
			normScreen.SetActive(false);

			GetComponent<PlayerMovement>().enabled = false;
			FindObjectOfType<CameraRotation>().enabled = false;
			FindObjectOfType<PickupChicken>().enabled = false;
			GetComponent<CharacterController>().enabled = false;
			gameObject.AddComponent<Rigidbody>();

			Cursor.lockState = CursorLockMode.None;

			done = true;
		}

		if(curHealth <= 0)
			AudioManager.instance.StopAll();

		if(curHealth > player.health)
			curHealth = player.health;
		
		chickenText.text = Player.inst.chickenAmount.ToString();

		if(Player.inst.GetComponent<PlayerHealth>().curHealth <= 0 || Player.inst.chickenAmount >= 21)
		{

			return;
		}
		if(done4 == false && curHealth == 4)
		{
			GameObject.Find("HurtD").GetComponent<DialogueTrigger>().TriggerDialogue();
			done4 = true;
		}
		if(Player.inst.chickenAmount >= FindObjectOfType<ChickenCount>().outChicken && done3 == false && FindObjectOfType<TimeOfDay>().curDay == 1)
		{
			GameObject.Find("BeepD").GetComponent<DialogueTrigger>().TriggerDialogue();
			done3 = true;
		}
		if(Player.inst.chickenAmount >= FindObjectOfType<ChickenCount>().outChicken && done2 == false)
		{
			chickenText.color = Color.red;
			StartCoroutine(Beeping());

			done2 = true;
		}
		else if(Player.inst.chickenAmount < FindObjectOfType<ChickenCount>().outChicken || FindObjectOfType<TimeOfDay>().night)
		{
			chickenText.color = Color.black;
			StopAllCoroutines();
			AudioManager.instance.Stop("Beep");

			done2 = false;
		}
	}

	public void TakeDamage(int amount)
	{
		curHealth -= amount;
		UpdateHealth();
	}

	public void AddHealth(int amount)
	{
		curHealth += amount;
		UpdateHealth();
	}

	void UpdateHealth()
	{
		HeartUI.sprite = heartSprites[curHealth];
	}

	IEnumerator Beeping()
	{
		while(true)
		{
			if(FindObjectOfType<TimeOfDay>().night == false)
				AudioManager.instance.Play("Beep");
			yield return new WaitForSeconds(0.5f);
		}
	}
}