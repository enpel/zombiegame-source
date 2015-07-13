using UnityEngine;
using System.Collections;
using UniRx;
using System;

public class ZombieSpawner : MonoBehaviour
{
	[SerializeField]
	GameObject ZombiePrefab;
	public GameObject target;
	[SerializeField]
	int activeDay = 1;
	[SerializeField]
	int spawnRateMilliSec =  3000;

	// Use this for initialization
	void Start ()
	{
		if (activeDay > 1)
			GameModel.Instance.ObserveEveryValueChanged (model => model.DayCount).Where (l => l >= activeDay).First ().Subscribe (_ => {
				ActiveSpawner();
			}).AddTo (this);
		else
			ActiveSpawner ();
		
		GameModel.Instance.ObserveEveryValueChanged (l => l.IsClear).Where (l => l).First ()
			.Subscribe (_ => GameObject.Destroy (this.gameObject)).AddTo (this);
	}

	void ActiveSpawner()
	{
		Observable.Interval (TimeSpan.FromMilliseconds (spawnRateMilliSec))
			.Subscribe (_ =>
			            {
				var zombie = Instantiate(ZombiePrefab, this.transform.position, Quaternion.identity) as GameObject;
				zombie.GetComponent<ZombieAgent>().target = target;
			}).AddTo(this);
	}

}
