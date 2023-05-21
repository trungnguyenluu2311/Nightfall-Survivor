using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;

	public static GameManager Instance { get => instance; set => instance = value; }

	private JobHandle jobHandle;
	private void Start()
	{

	}

	private void OnDisable()
	{
		jobHandle.Complete();
	}
}
