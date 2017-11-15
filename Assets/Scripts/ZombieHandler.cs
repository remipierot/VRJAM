using System.Collections.Generic;
using UnityEngine;

public class ZombieHandler : MonoBehaviour {
	private Transform _Player;
	private List<Zombie> _AliveZombies;
	private List<Zombie> _DeadZombies;
	private float[] _SpawnTimes;

	public Vector3[] SpawnPoints;
	public GameObject ZombiePrefab;

	private void Start()
	{
		_Player = GameObject.Find("Player").transform;
		_AliveZombies = new List<Zombie>();
		_DeadZombies = new List<Zombie>();
		_SpawnTimes = new float[SpawnPoints.Length];

		for (int i = 0; i < _SpawnTimes.Length; i++)
		{
			_SpawnTimes[i] = 0.0f;
		}
	}

	private void Update()
	{
		_RemoveDeadZombies();
		_PopNewZombies();
		_UpdateZombiesDestination();
	}

	private void _PopNewZombies()
	{
		GameObject newZombie;

		for(int i=0; i<SpawnPoints.Length; i++)
		{
			_SpawnTimes[i] += Time.deltaTime;
			
			if(_SpawnTimes[i] > 1.0f + i)
			{
				_SpawnTimes[i] = 0.0f;
				newZombie = Instantiate(ZombiePrefab, SpawnPoints[i], Quaternion.identity);
				_AliveZombies.Add(newZombie.GetComponent<Zombie>());
			}
		}
	}

	private void _RemoveDeadZombies()
	{
		_DeadZombies.Clear();

		foreach (Zombie z in _AliveZombies)
		{
			if (z.CurrentState == Zombie.ZombieState.DEAD)
			{
				_DeadZombies.Add(z);
			}
		}

		foreach (Zombie z in _DeadZombies)
		{
			_AliveZombies.Remove(z);
			Destroy(z.gameObject);
		}
	}

	private void _UpdateZombiesDestination()
	{
		foreach (Zombie z in _AliveZombies)
		{
			if (!z.Agent.destination.Equals(_Player.position))
			{
				z.Agent.destination = _Player.position;
			}
		}
	}
}
