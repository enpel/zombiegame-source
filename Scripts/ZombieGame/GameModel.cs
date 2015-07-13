using UnityEngine;
using System.Collections;

public class GameModel
{
	public int killCount { get; private set; }
	public bool IsClear { get; private set; }
	public int DayCount { get; private set; }
	
	private static GameModel _instance;
	public static GameModel Instance
	{
		get
		{
			if (_instance == null)
				_instance = new GameModel();

			return _instance;
		}
	}

	static GameModel()
	{
	}

	public void Initialize()
	{
		killCount = 0;
		IsClear = false;
		DayCount = 1;
	}

	public void AddKillCount()
	{
		killCount++;
	}

	public void Clear()
	{
		IsClear = true;
	}

	public void AddDayCount()
	{
		DayCount ++;
	}


}
