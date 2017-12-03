using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeOfDay : MonoBehaviour {

	public Transform[] chickSpawn;
	public Transform[] wolfSpawn;
	[Space]
	public Animator	sun;
	public Animator cam;
	[Space]
	public GameObject text;
	public Text day;
	[Space]
	public AIWolf wolf;
	public GameObject chicken;

	public int curDay;
	public bool night;

	void Start()
	{
		NewDay();
	}

	void Update()
	{
		sun.SetBool("Night", night);
		cam.SetBool("Night", night);

		if(wolfDead >= FindObjectOfType<ChickenCount>().outChicken / 3)
		{
			NewDay();
			wolfDead = 0;
		}

		switch(curDay)
		{
			case 1:
				day.text = "Monday";
				break;
			case 2:
				day.text = "Tuesday";
				break;
			case 3:
				day.text = "Wednesday";
				break;
			case 4:
				day.text = "Thursday";
				break;
			case 5:
				day.text = "Friday";
				break;
			case 6:
				day.text = "Saturday";
				break;
			case 7:
				day.text = "Sunday";
				break;
		}
	}

	void NewDay()
	{
		FindObjectOfType<ChickenCount>().outChicken += 3;
		curDay++;
		night = false;

		for(int i = 0; i < FindObjectOfType<ChickenCount>().outChicken - FindObjectOfType<ChickenCount>().chickens.Length; i++)
		{
			Instantiate(chicken, chickSpawn[Random.Range(0, chickSpawn.Length)].position, Quaternion.identity);
		}

		done = false;
	}

	void ToNight()
	{
		night = true;

		StartCoroutine(NightWolfSpawning());
	}

	bool done;
	void OnTriggerStay(Collider col)
	{
		if(col.tag == "Player" && FindObjectOfType<ChickenCount>().chickens.Length >= FindObjectOfType<ChickenCount>().outChicken)
		{
			if(done == false)
			{
				text.SetActive(true);
				if(Input.GetKeyDown(KeyCode.X))
				{
					done = true;
					ToNight();
					FindObjectOfType<PlayerHealth>().AddHealth(1);
					AudioManager.instance.Play("Sleep");
				}
			}
		}

		if(FindObjectOfType<ChickenCount>().chickens.Length < FindObjectOfType<ChickenCount>().outChicken)
		{
			text.SetActive(false);
		}
	}
	
	bool running;
	IEnumerator NightWolfSpawning()
	{
		if(running == false)
		{
			for(int i = 0; i < FindObjectOfType<ChickenCount>().outChicken / 3; i++)
			{
				yield return new WaitForSeconds(2);
				print("WolfSpawned");
				AIWolf spawnedWolf = Instantiate(wolf, wolfSpawn[Random.Range(0, wolfSpawn.Length)].position, Quaternion.identity) as AIWolf;
				spawnedWolf.OnDeath += OnWolfRetreat;
				yield return new WaitForSeconds(4);
			}

			running = false;
		}
		if(GameObject.FindGameObjectsWithTag("Wolf").Length <= 0)
		{
			NewDay();
		}
	}

	void OnTriggerExit(Collider col)
	{
		text.SetActive(false);
	}

	int wolfDead;	
	void OnWolfRetreat()
	{
		wolfDead++;
	}
}
