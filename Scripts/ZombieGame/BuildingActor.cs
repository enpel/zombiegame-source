using UnityEngine;
using System.Collections;

public class BuildingActor : ActorObjectBase
{
	[SerializeField]
	AudioClip deadSE;
	[SerializeField]
	GameObject deadEffect;

	protected override void OnDead ()
	{
		deadSE.PlayOnShot ();
		GameObject.Instantiate (deadEffect, this.transform.position, Quaternion.identity);

		base.OnDead ();
	}

}
