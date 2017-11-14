using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAcceleration : MonoBehaviour {
	public Transform HeadNode;
	public float Speed = 2.0f;

	public Vector3 StartingPoint { get; private set; }

	private void Start ()
	{
		StartingPoint = transform.position;
	}

	private void Update ()
	{
		// Get the forward of the camera on the XZ plane (horizontal, Right + Forward)
		Vector3 currentHeadForward = HeadNode.forward;
		currentHeadForward.y = 0.0f;
		currentHeadForward.Normalize();

		// Get the current local position of the head inside the camera rig on the XZ plane (horizontal, Right + Forward)
		Vector3 currentDirection = HeadNode.position - StartingPoint;
		currentDirection.y = 0.0f;

		// Activate movement
		if (currentDirection.magnitude > 0.45f && Vector3.Angle(currentHeadForward, currentDirection) < 25.0f)
		{
			Vector3 move = currentHeadForward * Speed * Time.deltaTime;
			transform.position += move;
			StartingPoint += move;
		}
	}
}
