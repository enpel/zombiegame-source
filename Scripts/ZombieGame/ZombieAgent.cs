using UnityEngine;
using System.Collections;
using UniRx;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieAgent : ActorObjectBase
{
	public GameObject target;
	[SerializeField]
	private EventTrigger attackTrigger;
	private ActorObjectBase attackTarget;
	IDisposable attackDisposable;
	[SerializeField]
	AudioSource hitSE;
	[SerializeField]
	int power = 1;
	// Use this for initialization
	void Start ()
	{
		var agent = this.GetComponent<NavMeshAgent> ();

		//0.5秒ごとに0.05右に移動
		Observable.Interval(TimeSpan.FromMilliseconds(500))
			.Subscribe(l =>
			           {
				if (target != null && agent != null)
					agent.SetDestination (target.transform.position);   
			}).AddTo(this);//これを消すと例外
		
		attackTrigger.onTriggerEnter += OnAttackTriggerEnter;
		attackTrigger.onTriggerExit += OnAttackTriggerExit;

		
		GameModel.Instance.ObserveEveryValueChanged (l => l.IsClear).Where (l => l).First ()
			.Subscribe (_ => GameObject.Destroy (this.gameObject)).AddTo (this);
	}

	void OnAttackTriggerEnter(Collider obj)
	{
		var actor = obj.GetComponent<ActorObjectBase> ();
		if (actor != null) {
			attackTarget = actor;
			attackDisposable = Observable.Interval(TimeSpan.FromMilliseconds(1000))
				.Subscribe(l =>{
					if (attackTarget != null)
					{
						attackTarget.AddDamage(power);
						hitSE.clip.PlayOnShot ();
					}
				}).AddTo(this);
		}
	}

	void OnAttackTriggerExit(Collider obj)
	{
		var actor = obj.GetComponent<ActorObjectBase> ();
		if (actor != null && attackTarget != null && attackTarget.GetInstanceID() == actor.GetInstanceID()) {
			attackDisposable.Dispose();
			attackTarget = null;
		}
	}

	public override void AddDamage (int damage)
	{
		base.AddDamage (damage);
		GetComponent<NavMeshAgent> ().velocity = Vector3.zero;
	}

	protected override void OnDead ()
	{
		GameModel.Instance.AddKillCount ();
		base.OnDead ();
	}
}
