using UnityEngine;
using System.Collections;

public class WeaponSummonZombie : WeaponBase
{
	[SerializeField]
	GameObject prefab;
	[SerializeField]
	GameObject target;
	protected override void HitFire (RaycastHit hit)
	{
		GameObject obj = hit.collider.gameObject;if (obj.layer == LayerMask.NameToLayer("Floor"))
		{
			var fx = Instantiate(prefab, hit.point, Quaternion.identity) as GameObject;
			fx.GetComponent<ZombieAgent>().target = target;
		}
	}
}
