using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAcceleration : MonoBehaviour {
	public Transform HeadNode;
	public float Speed = 2.0f;
	public bool AllowedToMove = true;

	public SteamVR_TrackedController LeftCon;
	public SteamVR_TrackedController RightCon;

	public Vector3 StartingPoint { get; private set; }

	private void Start()
	{
		StartingPoint = HeadNode.localPosition;
	}

	private void Update()
	{
		if(LeftCon.triggerPressed || RightCon.triggerPressed)
		{
			StartingPoint = HeadNode.localPosition;
		}

		// Get the forward of the camera on the XZ plane (horizontal, Right + Forward)
		Vector3 currentHeadForward = HeadNode.forward;
		currentHeadForward.y = 0.0f;

		// Get the current local position of the head inside the camera rig on the XZ plane (horizontal, Right + Forward)
		Vector3 currentDirection = HeadNode.localPosition - StartingPoint;
		currentDirection.y = 0.0f;

		// Activate movement
		if(currentDirection.magnitude > 0.10f && Vector3.Dot(currentHeadForward, currentDirection) > 0.0f && AllowedToMove)
		{
			Vector3 move = currentHeadForward.normalized * Speed * currentDirection.magnitude * Time.deltaTime;
			transform.position += move;
		}
	}
}
