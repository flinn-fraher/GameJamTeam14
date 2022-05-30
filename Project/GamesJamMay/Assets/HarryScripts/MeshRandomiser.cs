using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRandomiser : MonoBehaviour
{
	[SerializeField] private List<GameObject> Meshes = new List<GameObject>();
	private void Awake()
	{
		foreach(GameObject Actor in Meshes)
		{
			Actor.SetActive(false);
		}
		Meshes[Random.Range(0, Meshes.Count)].SetActive(true);
	}
}
