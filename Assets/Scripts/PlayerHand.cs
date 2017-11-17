using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour {
	public float MinimumHitDistance;
	public AudioSource SoundHandler;
	public ChickenAcceleration Player;

	private SteamVR_TrackedObject _TrackedObj;
	private SteamVR_Controller.Device _Controller;

	private void Start()
	{
		_TrackedObj = transform.GetComponent<SteamVR_TrackedObject>();
	}

	private void OnTriggerStay(Collider other)
	{
		if ((transform.position - Player.StartingPoint).magnitude > MinimumHitDistance &&
			other.gameObject.layer == LayerMask.NameToLayer("Zombie"))
		{
			Zombie z = other.gameObject.GetComponent<Zombie>();

			if (z != null && z.CanBeShot())
			{
				StartCoroutine(_LongVibration(0.5f, 0.5f));
				SoundHandler.Play();

				Vector3 punchDirection = -z.transform.forward;
				punchDirection.y = 0.2f;
				punchDirection = punchDirection.normalized;

				z.Body.AddForce(punchDirection * 5000.0f);
				z.Die();
			}
		}
	}

	// Length in seconds
	// Strength [0;1]
	private IEnumerator _LongVibration(float length, float strength)
	{
		for (float i = 0; i < length; i += Time.deltaTime)
		{
			_Controller = SteamVR_Controller.Input((int)_TrackedObj.index);
			_Controller.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
			yield return null;
		}
	}
}
