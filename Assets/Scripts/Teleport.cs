using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour {
	public Transform Destination;
	public PostProcFadeToBlack FadeHandler;
	public GameObject ZombieHandler;
	public AudioSource SoundHandler;
	public float YRotation;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			SoundHandler.Play();
			FadeHandler.FadeOut();
			other.gameObject.GetComponent<ChickenAcceleration>().AllowedToMove = false;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player") && FadeHandler.IsFadedOut)
		{
			other.transform.position = Destination.position;
			other.transform.Find("Camera (eye)").transform.rotation *= Quaternion.Euler(0, YRotation, 0);
			other.gameObject.GetComponent<ChickenAcceleration>().AllowedToMove = true;

			if(!ZombieHandler.activeInHierarchy)
			{
				ZombieHandler.SetActive(true);
			}
		}
	}
}
