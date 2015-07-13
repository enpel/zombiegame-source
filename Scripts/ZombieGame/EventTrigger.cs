using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Collider))]
public class EventTrigger : MonoBehaviour {
	public event Action<Collider> onTriggerEnter;
	public event Action<Collider> onTriggerExit;

	void OnTriggerEnter(Collider other) {
		if (onTriggerEnter != null)
			onTriggerEnter (other);
	}
	
	void OnTriggerExit(Collider other) {
		if (onTriggerExit != null)
			onTriggerExit (other);
	}
}
