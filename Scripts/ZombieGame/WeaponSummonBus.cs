using UnityEngine;
using System.Collections;
using System;

public class WeaponSummonBus : WeaponBase
{
	
	[SerializeField]
	LayerMask mask;
	[SerializeField]
	GameObject prefab;
	
	public override void Fire(Camera camera)
	{
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit = new RaycastHit();
		
		attackSE.PlayOnShot();
		ammoCurrent --;
		
		if (Physics.Raycast(ray, out hit,1000.0f, mask)){
			HitFire(hit);
		}
	}

	protected override void HitFire (RaycastHit hit)
	{
		GameObject obj = hit.collider.gameObject;if (obj.layer == LayerMask.NameToLayer("Floor"))
		{
			UnityEngine.Random.seed = DateTime.Now.Second;
			var fx = Instantiate(prefab, hit.point, Quaternion.AngleAxis(UnityEngine.Random.Range(0.0f, 360.0f), Vector3.up)) as GameObject;
		}
	}
}
