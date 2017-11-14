using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public int HealthPoint = 100;

	private void Update()
	{
		Debug.LogError(HealthPoint);
	}
}
