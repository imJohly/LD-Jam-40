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
	void Update()
	{
		if(curHealth <= 0 && done == false)
		{
			deadScreen.SetActive(true);
			normScreen.SetActive(false);

			done = true;

			GetComponent<PlayerMovement>().enabled = false;
			FindObjectOfType<CameraRotation>().enabled = false;
			FindObjectOfType<PickupChicken>().enabled = false;
			GetComponent<CharacterController>().enabled = false;
			gameObject.AddComponent<Rigidbody>();
		}

		if(curHealth <= 0)
			AudioManager.instance.StopAll();

		if(curHealth > player.health)
			curHealth = player.health;
		
		chickenText.text = Player.inst.chickenAmount.ToString();
		
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