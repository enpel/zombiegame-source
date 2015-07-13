using UnityEngine;
using System.Collections;
using UniRx;

public class WeaponSMG : WeaponBase
{
	void Start()
	{
		this.ObserveEveryValueChanged(x=>x.ammoCurrent).Subscribe(count=>{

		}).AddTo(this);
	}

	public override bool FireTrigger ()
	{
		return Input.GetMouseButton (0);
	}

	public override bool CanFire {
		get {
			return base.CanFire;
		}
	}

	public override void Fire (Camera camera)
	{

		base.Fire (camera);
	}
}
