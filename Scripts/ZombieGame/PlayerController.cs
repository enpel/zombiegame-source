using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using System;

public class PlayerController : ObservableMonoBehaviour
{
	[SerializeField]
	Camera camera;

	[SerializeField]
	BuildingActor guardTarget;

	WeaponBase[] weapons;
	public WeaponBase currentWeapon;// { get; private set; }
	private int weaponIndex = 0; 

	double interval = 0;
	// Use this for initialization
	void Start () {
		weapons = this.GetComponents<WeaponBase>();
		currentWeapon = weapons[weaponIndex];
		var clickStream = UpdateAsObservable ()
			.Where (_ => currentWeapon.FireTrigger());
		
		var rightclickStream = UpdateAsObservable ()
			.Where (_ => Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.R));

		
		var weaponChangeStreamQ = UpdateAsObservable ()
			.Where (_ => Input.GetKeyDown (KeyCode.Q));
		var weaponChangeStreamE = UpdateAsObservable ()
			.Where (_ => Input.GetKeyDown (KeyCode.E));


		clickStream.TimeInterval().Where(l=>{
			interval += l.Interval.TotalMilliseconds;
			return interval  > currentWeapon.FireRatePerMilliSec;} ).Subscribe (_ =>
		{
			interval = 0;
			if (currentWeapon.CanFire)
			{
				currentWeapon.Fire(camera);
			}
			else if (!currentWeapon.IsReloading)
			{
				currentWeapon.Reload();
			}
		}).AddTo(this);

		rightclickStream.Subscribe (_ =>
		                            {
			if (!currentWeapon.IsReloading)
			{
				currentWeapon.Reload();
			}
		}).AddTo(this);

		weaponChangeStreamQ.Subscribe (_ =>{
			weaponIndex--;
			if (weaponIndex < 0)
				weaponIndex = weapons.Length -1;
			currentWeapon = weapons[weaponIndex];
		}).AddTo(this);
		weaponChangeStreamE.Subscribe (_ =>{
			weaponIndex++;
			if (weaponIndex >= weapons.Length)
				weaponIndex = 0;
			currentWeapon = weapons[weaponIndex];
		}).AddTo(this);


		guardTarget.ObserveEveryValueChanged (x => x.currentHelth)
				.Subscribe (helth => {
				Debug.Log("helth: "+helth);
					if (helth <= 0)
						Application.LoadLevel(Application.loadedLevelName);
		}).AddTo(this);
	}
}
