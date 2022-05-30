using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISpawner : MonoBehaviour
{
	[SerializeField] private int NumOfAI;
	[SerializeField] private float spawnRange;
	[SerializeField] private GameObject AI;
	private int i = 0;
	private void Start()
	{
		Spawn();
	}
	private void Spawn()
	{
		while(i<NumOfAI)
		{
			float randomZ = Random.Range(-spawnRange, spawnRange);
			float randomX = Random.Range(-spawnRange, spawnRange);

			Vector3	spawnPos = new Vector3(transform.position.x + randomX, 1, transform.position.z + randomZ);
			NavMeshHit hit;
			if (NavMesh.SamplePosition(spawnPos, out hit, 1.0f, NavMesh.AllAreas))
			{
				StartCoroutine(SpawnAI(hit));
			}
			print (i);
		}
	}
	private IEnumerator SpawnAI(NavMeshHit hit)
	{
		Instantiate(AI, hit.position, Quaternion.identity, this.transform);
		i++;
		yield return new WaitForSeconds(.1f);
	}
}
