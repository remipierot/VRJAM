using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombieHandler : MonoBehaviour {
	private List<Zombie> _AliveZombies;
	private List<Zombie> _DeadZombies;
	private float[] _SpawnTimes;
	private float _StartingTime;
	private int killedZombies = 0;

	public Transform Player;
	public Transform[] SpawnPoints;
	public GameObject ZombiePrefab;
	public float InitialBetweenPopFactor;
	public TextMeshPro Text;

	private void Start()
	{
		_AliveZombies = new List<Zombie>();
		_DeadZombies = new List<Zombie>();
		_SpawnTimes = new float[SpawnPoints.Length];
		_StartingTime = Time.realtimeSinceStartup;

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
			
			if(_SpawnTimes[i] > InitialBetweenPopFactor / (Time.realtimeSinceStartup - _StartingTime))
			{
				_SpawnTimes[i] = 0.0f;
				newZombie = Instantiate(ZombiePrefab, SpawnPoints[i].position, Quaternion.identity);
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

		killedZombies += _DeadZombies.Count;

		Text.text = killedZombies + "\nKills";

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
			if (!z.Agent.destination.Equals(Player.position))
			{
				z.Agent.destination = Player.position;
			}
		}
	}
}
