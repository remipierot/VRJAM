using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	private int _HealthPoint;
	public float LastHitTime;
	public float HitRecoverTime;
	public TextMeshPro Text;
	public PostProcFadeToBlack FadeHandler;

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

				Text.text = _HealthPoint + "\nHealth";

				if(_HealthPoint == 0)
				{
					FadeHandler.FadeOut();
					SceneManager.LoadScene("Final");
				}
			}
		}
	}

	public void Start()
	{
		_HealthPoint = 100;
		Text.text = _HealthPoint + "\nHealth";
	}
}
