using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;

public class UIController : MonoBehaviour
{
	[SerializeField]
	GameObject stageUI;
	[SerializeField]
	Text dayText;
	[SerializeField]
	Text weaponName;
	[SerializeField]
	Text bulletAmmo;
	[SerializeField]
	Text targetHelth;
	[SerializeField]
	Text killCount;

	
	[SerializeField]
	ClearUI clearUI;

	[SerializeField]
	ActorObjectBase target;
	[SerializeField]
	PlayerController player;
	[SerializeField]
	StageManager stageManager;

	// Use this for initialization
	void Start ()
	{
		target.ObserveEveryValueChanged (x => x.currentHelth)
			.Subscribe (helth => {
				targetHelth.text = string.Format("HP {0}/{1}", helth, target.maxHelth);
			}).AddTo(this);
		player.ObserveEveryValueChanged (x => x.currentWeapon)
			.Subscribe (weapon => weaponName.text = weapon.WeaponName)
				.AddTo(this);
		
		player.ObserveEveryValueChanged (x => x.currentWeapon.ammoCurrent)
			.Subscribe (ammo => bulletAmmo.text = ammo + "/"+ player.currentWeapon.AmmoMax)
				.AddTo(this);

		stageManager.ObserveEveryValueChanged (x => x.currentDay)
			.Subscribe (day=> dayText.text = string.Format("{0}Day",day)).AddTo (this);

		GameModel.Instance.ObserveEveryValueChanged (x => x.killCount)
			.Subscribe (count=> killCount.text = string.Format("{0}",count)).AddTo (this);


		GameModel.Instance.ObserveEveryValueChanged (l => l.IsClear).Where(l=>l).First().Subscribe (_ => {
			stageUI.SetActive(false);
			clearUI.gameObject.SetActive(true);
			clearUI.ClearText = dayText.text + "\n" +
				targetHelth.text + "\n" +
					"倒したゾンビの数 \n" +
					killCount.text + "\n" ;
		});
	}
}
