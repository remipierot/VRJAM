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
	public float MinimumAttackDistance;

	private void Start()
	{
		Agent.destination = Vector3.zero;
		Agent.stoppingDistance = MinimumAttackDistance;
	}

	private void Update()
	{
		_ComputeState();
		_PlayAnimation();
	}

	private void _ComputeState()
	{
		if(CurrentState != ZombieState.SHOT &&
			CurrentState != ZombieState.DYING &&
			CurrentState != ZombieState.DEAD)
		{
			float distanceToDestination = (Agent.destination - transform.position).magnitude;

			if (distanceToDestination > MinimumAttackDistance)
			{
				_ChangeCurrentState(ZombieState.WALK);
			}
			else
			{
				transform.LookAt(Agent.destination);
				_ChangeCurrentState(ZombieState.ATTACK);
			}
		}
	}

	private void _ChangeCurrentState(ZombieState newState)
	{
		if (CurrentState != newState)
		{
			CurrentState = newState;
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
