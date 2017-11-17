using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
	private bool _AlreadyPlayedDead = false;

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
	public GameObject DyingExplosion;
	public float Speed;

	public AudioSource SoundHandler;
	public AudioClip[] RandomSounds;
	public AudioClip AttackSound;
	public AudioClip DieSound;

	private void Start()
	{
		Agent.destination = Vector3.zero;
		Agent.stoppingDistance = MinimumAttackDistance;
		Agent.speed = Speed;
	}

	private void Update()
	{
		_ComputeState();
		_PlaySound();
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

	private void _PlaySound()
	{
		if(!SoundHandler.isPlaying)
		{
			if (CurrentState == ZombieState.IDLE || CurrentState == ZombieState.WALK)
			{
				int clipId = (int)(Time.realtimeSinceStartup * 1000.0f)%RandomSounds.Length;
				SoundHandler.PlayOneShot(RandomSounds[clipId]);
			}
			else if (CurrentState == ZombieState.ATTACK)
			{
				SoundHandler.PlayOneShot(AttackSound);
			}
			else if (CurrentState == ZombieState.SHOT || (CurrentState == ZombieState.DYING && !_AlreadyPlayedDead))
			{
				SoundHandler.PlayOneShot(DieSound);
				_AlreadyPlayedDead = true;
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
				DyingExplosion.SetActive(true);

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

	public void Die()
	{
		_ChangeCurrentState(ZombieState.SHOT);
		Agent.isStopped = true;
	}

	public bool CanBeShot()
	{
		return CurrentState == ZombieState.IDLE || 
			CurrentState == ZombieState.WALK || 
			CurrentState == ZombieState.ATTACK;
	}
}
