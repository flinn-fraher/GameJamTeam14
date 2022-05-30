using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWander : MonoBehaviour
{
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private Transform player;
	[SerializeField] private LayerMask whatIsGround, whatIsPlayer;

	[SerializeField] private Vector3 walkPoint;
	[SerializeField] private bool walkPointSet;
	[SerializeField] private float walkPointRange;
	[SerializeField] private Vector3 distanceToWalkPoint;
	[SerializeField] private float rangeCheck = 2f;
	[SerializeField] private Animator animator;
	private Color color;
	private void Awake()
	{
		color = Random.ColorHSV();
		StartCoroutine(RunPatrol());
	}

	private IEnumerator RunPatrol()
	{
		while (true)
		{
			Patrolling();
			yield return new WaitForSeconds(.1f);
		}
	}
	private void Patrolling()
	{
		if (agent.velocity.magnitude > .1f)
		{
			animator.SetBool("Walking", true);
		}
		else
		{
			animator.SetBool("Walking", false);
		}
		if (!walkPointSet)
		{
			SearchWalkPoint();
		}
		if(walkPointSet)
		{
			agent.SetDestination(walkPoint);
		}
		distanceToWalkPoint = transform.position - walkPoint;
		if(distanceToWalkPoint.magnitude < rangeCheck)
		{
			walkPointSet = false;
		}
	}
	private void SearchWalkPoint()
	{
		float randomZ = Random.Range(-walkPointRange, walkPointRange);
		float randomX = Random.Range(-walkPointRange, walkPointRange);
		
		walkPoint = new Vector3(transform.position.x + randomX, 1, transform.position.z + randomZ);
		NavMeshHit hit;
		if (NavMesh.SamplePosition(walkPoint, out hit, 1.0f, NavMesh.AllAreas))
		{
			walkPointSet = hit.hit;
		}
		else
		{
			SearchWalkPoint();
		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = color;
		Gizmos.DrawSphere(walkPoint, 1f);
	}
}
