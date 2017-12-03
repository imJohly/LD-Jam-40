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

	Player player;

	void Start()
	{
		player = Player.inst;
		curHealth = player.health;
	}

	void Update()
	{
		chickenText.text = Player.inst.chickenAmount.ToString();
		
		if(Player.inst.chickenAmount >= FindObjectOfType<ChickenCount>().outChicken)
			chickenText.color = Color.red;
		else
			chickenText.color = Color.black;
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
}