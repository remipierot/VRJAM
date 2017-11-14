using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
	public enum ZombieState
	{
		IDLE,
		WALK,
		ATTACK,
		SHOT,
		DYING,
		DEAD
	}

	public NavMeshAgent Agent;
	public Animation LegacyAnimator;
	public ZombieState CurrentState;
	public Rigidbody Body;

	private void Start()
	{
		Agent.destination = Vector3.zero;
	}

	private void Update()
	{
		_ComputeState();
		_PlayAnimation();
	}

	private void _ComputeState()
	{
		Vector3 horizontalVelocity = Body.velocity;
		horizontalVelocity.y = 0.0f;

		if (horizontalVelocity.magnitude > 0.0f && CurrentState == ZombieState.IDLE)
		{
			CurrentState = ZombieState.WALK;
		}
	}

	private void _PlayAnimation()
	{
		switch (CurrentState)
		{
			case ZombieState.IDLE:
				LegacyAnimator.Play("Zombie_Idle_01");
				break;
			case ZombieState.WALK:
				LegacyAnimator.Play("Zombie_Walk_01");
				break;
			case ZombieState.ATTACK:
				LegacyAnimator.Play("Zombie_Attack_01");
				break;
			case ZombieState.SHOT:
				LegacyAnimator.Play("Zombie_Death_01");
				CurrentState = ZombieState.DYING;
				break;
			case ZombieState.DYING:
				if (!LegacyAnimator.isPlaying)
				{
					LegacyAnimator.Stop();
					CurrentState = ZombieState.DEAD;
				}
				break;
			default:
				break;
		}
	}
}
