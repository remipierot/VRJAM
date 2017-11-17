using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowSpeed : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			ChickenAcceleration chicken = other.gameObject.GetComponent<ChickenAcceleration>();

			if (chicken != null)
			{
				chicken.Speed *= 2.0f;
			}

		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			ChickenAcceleration chicken = other.gameObject.GetComponent<ChickenAcceleration>();

			if (chicken != null)
			{
				chicken.Speed /= 2.0f;
			}

		}
	}
}
