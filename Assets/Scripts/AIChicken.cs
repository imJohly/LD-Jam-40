using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChicken : MonoBehaviour {

	public float moveSpeed;

	private Vector3[] wanderPoints;
	private	int curPoint;

	private float currentLerpTime;

	Animator anim;

	void OnEnable()
	{
		wanderPoints = new Vector3[4];

		curPoint = 1;
		for(int i = 0; i < wanderPoints.Length; i++)
		{
			wanderPoints[i] = new Vector3(Random.Range(transform.position.x - 4, transform.position.x + 4), 0, Random.Range(transform.position.z - 4, transform.position.z + 4));
		}

	}

	void Start()
	{
		anim = GetComponent<Animator>();

		curPoint = 1;
		for(int i = 0; i < wanderPoints.Length; i++)
		{
			wanderPoints[i] = new Vector3(Random.Range(transform.position.x - 4, transform.position.x + 4), 0, Random.Range(transform.position.z - 4, transform.position.z + 4));
		}
	}

	void FixedUpdate()
	{
		var lookPos = wanderPoints[curPoint] - transform.position;
		lookPos.y = 0;
		var rotation = Quaternion.LookRotation(lookPos);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);

		currentLerpTime += Time.deltaTime * moveSpeed;
		if(currentLerpTime > 1)
			currentLerpTime = 1;

		transform.position = Vector3.Lerp(transform.position, new Vector3(wanderPoints[curPoint].x, transform.position.y, wanderPoints[curPoint].z), currentLerpTime);

		var distance = (new Vector3(wanderPoints[curPoint].x, 0, wanderPoints[curPoint].z) - new Vector3(transform.position.x, 0, transform.position.z)).sqrMagnitude;
		if(distance < 0.1f)
		{
			if(running == false)
				StartCoroutine(WaitTime());
		}
		if(curPoint > wanderPoints.Length - 1)
			curPoint = 0;

		anim.SetBool("Moving", running);

		if(transform.position.y < -200)
		{
			transform.position = new Vector3(transform.position.x, 200, transform.position.z);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		if(wanderPoints != null)
		{
			foreach(Vector3 point in wanderPoints)
			{
				Gizmos.DrawSphere(point, 0.5f);
			}
		}
	}

	bool running;
	IEnumerator WaitTime()
	{
		running = true;
		yield return new WaitForSeconds(Random.Range(0f, 1.5f));
		curPoint = Random.Range(0, 4);
		currentLerpTime = 0;
		running = false;
	}

	void OnCollisionStay(Collision col)
	{
		if(col.gameObject.tag == "Boundary")
		{
			wanderPoints[curPoint] = new Vector3(Random.Range(transform.position.x - 4, transform.position.x + 4), 0, Random.Range(transform.position.z - 4, transform.position.z + 4));
		}
	}
}
