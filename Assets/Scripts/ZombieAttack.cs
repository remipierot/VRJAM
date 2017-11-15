using UnityEngine;

public class ZombieAttack : MonoBehaviour {
	private Player _Player;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			if(_Player == null)
			{
				_Player = other.gameObject.GetComponent<Player>();
			}

			if(_Player != null)
			{
				_Player.HealthPoint--;
			}
			
		}
	}
}
