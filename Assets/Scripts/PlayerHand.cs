using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {
	public float MinimumHitDistance;

	private void OnTriggerStay(Collider other)
	{
		if (transform.localPosition.magnitude > MinimumHitDistance &&
			other.gameObject.layer == LayerMask.NameToLayer("Zombie"))
		{
			Zombie z = other.gameObject.GetComponent<Zombie>();

			if (z != null && z.CanBeShot())
			{
				z.Die();
			}

		}
	}
}
