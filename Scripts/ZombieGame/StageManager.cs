using UnityEngine;
using System.Collections;
using UniRx;
using System;

public class StageManager : MonoBehaviour
{
	[SerializeField]
	int oneDayPerSec = 10;
	[SerializeField]
	int targetDay = 3;
	public int currentDay { get{ return GameModel.Instance.DayCount; } } 

	// Use this for initialization
	void Start ()
	{
		Observable.Interval (TimeSpan.FromMilliseconds (oneDayPerSec * 1000))
			.Subscribe (_ => { 
				GameModel.Instance.AddDayCount();
				if (targetDay < currentDay)
					GameModel.Instance.Clear();
			}).AddTo(this);

		GameModel.Instance.Initialize ();

	}

	public void Restart()
	{
		Application.LoadLevel (Application.loadedLevelName);
	}
}
