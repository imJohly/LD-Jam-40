using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWolf : MonoBehaviour {

	public float moveSpeed;
	public float maxSpeed;
	public enum State {Search, Attack, Retreat};
	[Space]
	public State curState = State.Search;
	public float distToAttack;

	public Transform target;

	Rigidbody rigid;
	Animator anim;

	public delegate void OnDying();
	public OnDying OnDeath;
	[Space]
	public LayerMask groundLayer;
	Transform retreatPos;
	void Start()
	{
		retreatPos = FindObjectOfType<TimeOfDay>().wolfSpawn[Random.Range(0, FindObjectOfType<TimeOfDay>().wolfSpawn.Length)];

		curState = State.Search;
		target = Player.inst.transform;

		rigid = GetComponent<Rigidbody>();
		anim = transform.GetChild(0).GetComponent<Animator>();
	}

	void Update()
	{
		if(transform.position.y < -200)
		{
			if(OnDeath != null)
			{
				OnDeath();
			}
			Destroy(gameObject);
		}

		var heading = target.position - transform.position;

		if(heading.magnitude < distToAttack && curState != State.Retreat)
			curState = State.Attack;
		if(heading.magnitude > distToAttack + 5 && curState != State.Retreat)
			curState = State.Search;

		if(rigid.velocity.magnitude > maxSpeed)
		{
			var normVel = rigid.velocity.normalized;
			normVel *= maxSpeed;

			rigid.velocity = normVel;
		}

		switch(curState)
		{
			case State.Retreat:
				Retreat();
				break;

			case State.Search:

				Search();
				break;

			case State.Attack:

				StartCoroutine(Attack());
				break;
		}

		float speedPerc = Mathf.Clamp(rigid.velocity.magnitude / moveSpeed, 0, 1);
		anim.SetFloat("Speed", speedPerc);
	}

	void Search()
	{
		var heading = target.position - transform.position;
		heading.y = 0;
		var rotation = Quaternion.LookRotation(heading);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);
		var distance = heading.magnitude;
		var direction = heading / distance;

		direction *= moveSpeed;

		rigid.velocity = new Vector3(direction.x, rigid.velocity.y, direction.z);
	}

	bool running;
	IEnumerator Attack()
	{
		var heading = target.position - transform.position;
		heading.y = 0;
		var rotation = Quaternion.LookRotation(heading);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);
  
		if(running == false && Physics.Raycast(transform.position, Vector3.down, 2.1f, groundLayer))
		{
			rigid.drag = 2;

			running = true;
			yield return new WaitForSeconds(1);

			var distance = heading.magnitude;
			var direction = heading / distance;

			direction.Normalize();
			direction *= moveSpeed * 5;
			direction.y = 6;

			rigid.AddForce(direction, ForceMode.Impulse);

			anim.SetTrigger("Attack");

			AudioManager.instance.Play("Growl");

			yield return new WaitForSeconds(1);
			rigid.drag = 0;
			running = false;
		}
	}

	void Retreat()
	{
		StopAllCoroutines();

		var heading = retreatPos.position - transform.position;
		heading.y = 0;
		var rotation = Quaternion.LookRotation(heading);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);
		
		var distance = heading.magnitude;
		var direction = heading / distance;

		direction *= moveSpeed;
		
		rigid.velocity = new Vector3(direction.x, rigid.velocity.y, direction.z);

		if(distance < 0.1f)
		{
			if(OnDeath != null)
			{
				OnDeath();
			}
			Destroy(gameObject);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color  = Color.red;
		Gizmos.DrawWireSphere(transform.position, distToAttack);
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Boundary")
		{
			retreatPos = FindObjectOfType<TimeOfDay>().wolfSpawn[Random.Range(0, FindObjectOfType<TimeOfDay>().wolfSpawn.Length)];
		}
	}
}
