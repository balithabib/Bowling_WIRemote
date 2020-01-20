using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
	public float distance;
	public float height;
	public GameObject objectToFollow;
	
	void LateUpdate () {
		if (objectToFollow == null)
			return;

		Vector3 dest = objectToFollow.transform.position;    
		dest.y += height;
		dest.z += distance;
		transform.position = dest;
	}
}
