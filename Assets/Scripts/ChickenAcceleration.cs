using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAcceleration : MonoBehaviour {
	public Transform HeadNode;
	public float Speed = 2.0f;

	void Update ()
	{
		Vector3 currentHeadForward = HeadNode.forward;
		currentHeadForward.y = 0.0f;

		Vector3 currentDirection = HeadNode.localPosition;
		currentDirection.y = 0.0f;

		Debug.LogError(Vector3.Angle(currentHeadForward, currentDirection));

		if (currentDirection.magnitude > 0.15f && Vector3.Angle(currentHeadForward, currentDirection) < 45.0f)
		{
			transform.position += currentHeadForward * Speed * Time.deltaTime;
		}
	}
}
