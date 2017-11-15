using UnityEngine;

public class Player : MonoBehaviour {
	private int _HealthPoint;
	public float LastHitTime;
	public float HitRecoverTime;

	public int HealthPoint
	{
		get
		{
			return _HealthPoint;
		}

		set
		{
			// Handle recover after being hit
			if(Time.realtimeSinceStartup - LastHitTime > HitRecoverTime)
			{
				LastHitTime = Time.realtimeSinceStartup;
				_HealthPoint = value;
				Debug.LogError(_HealthPoint);
			}
		}
	}
}
