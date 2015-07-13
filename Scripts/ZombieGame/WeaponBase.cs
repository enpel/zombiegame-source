using UnityEngine;
using System.Collections;
using System;
using UniRx;

public class WeaponBase : MonoBehaviour
{
	[SerializeField]
	protected string weaponName;
	public string WeaponName { get { return weaponName; } }
	[SerializeField]
	protected int power;
	[SerializeField]
	protected int reloadTime = 1000;
	[SerializeField]
	int fireRatePerMilliSec = 100;
	public int FireRatePerMilliSec { get { return fireRatePerMilliSec; } } 
	
	[SerializeField]
	protected AudioClip attackSE;
	[SerializeField]
	protected AudioClip reloadSE;
	
	[SerializeField]
	int ammoMax = 6;
	public int AmmoMax { get { return ammoMax; } }
	public int ammoCurrent { get; protected set; }
	IDisposable reloadDisposable;

	[SerializeField]
	GameObject bulletImpactEffetcPrefab;


	public virtual bool FireTrigger()
	{
		return Input.GetMouseButtonDown (0);
	}

	public virtual bool CanFire
	{
		get { return ammoCurrent > 0; }
	}

	public bool IsReloading { get { return reloadDisposable != null; } }

	void Awake()
	{
		ammoCurrent = ammoMax;
	}

	public void Reload()
	{
		reloadDisposable = Observable.Interval(TimeSpan.FromMilliseconds(reloadTime))
			.Subscribe(l=> {
				ammoCurrent = ammoMax;
				reloadDisposable.Dispose();
				reloadDisposable = null;
			});
		reloadSE.PlayOnShot();
	}

	public virtual void Fire(Camera camera)
	{
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		
		attackSE.PlayOnShot();
		ammoCurrent --;
		
		if (Physics.Raycast(ray, out hit,1000.0f)){
			HitFire(hit);
		}
	}

	protected virtual void HitFire(RaycastHit hit)
	{
		GameObject obj = hit.collider.gameObject;
		var actor = obj.GetComponent<ActorObjectBase>();
		if (actor != null)
		{
			actor.AddDamage(power);
		}
		else if (obj.layer == LayerMask.NameToLayer("Floor"))
		{
			var fx = Instantiate(bulletImpactEffetcPrefab, hit.point, Quaternion.identity);
		}
	}
}
