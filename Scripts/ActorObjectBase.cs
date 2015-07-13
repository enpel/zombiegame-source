using UnityEngine;
using System.Collections;
using UniRx;

public class ActorObjectBase : MonoBehaviour
{
	public int currentHelth { get; private set; }
	public int maxHelth = 10;
	
	// Use this for initialization
	void Awake ()
	{
		currentHelth = maxHelth;	
		
		this.ObserveEveryValueChanged (x => x.currentHelth)
			.Subscribe (helth => {
				if (helth <= 0)
					OnDead ();
			}).AddTo(this);
	}
	
	public virtual void AddDamage(int damage)
	{
		this.currentHelth -= damage;
	}

	protected virtual void OnDead()
	{
		Destroy (this.gameObject);
	}
}
