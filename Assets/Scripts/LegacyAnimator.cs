using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegacyAnimator : MonoBehaviour {
	private Animation _ZombieAnimation;
	public ZombieState _CurrentState;

	private void Start ()
	{
		_ZombieAnimation = transform.GetChild(0).gameObject.GetComponent<Animation>();	
	}
	
	private void Update ()
	{
		switch(_CurrentState)
		{
			case ZombieState.IDLE:
				_ZombieAnimation.Play("Zombie_Idle_01");
				break;
			case ZombieState.WALK:
				_ZombieAnimation.Play("Zombie_Walk_01");
				break;
			case ZombieState.ATTACK:
				_ZombieAnimation.Play("Zombie_Attack_01");
				break;
			case ZombieState.DIE:
				_ZombieAnimation.Play("Zombie_Death_01");
				_CurrentState = ZombieState.DEAD;
				break;
			default:
				if(!_ZombieAnimation.isPlaying)
				{
					_ZombieAnimation.Stop();
				}
				break;
		}
	}

	public void SetState(ZombieState newState)
	{
		if(_CurrentState != newState)
		{
			_CurrentState = newState;
		}
	}
}
