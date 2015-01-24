using UnityEngine;
using System.Collections;

public class ObjectWithTransform : MonoBehaviour {
	private Transform _Transform;
	private Vector3 position;

	// Use this for initialization
	void Start () {
		_Transform				= transform;
		position				= _Transform.position;
	}

	public void SetXPosition(float delta){
		position				= _Transform.position;
		position.x				+= delta;
		_Transform.position		= position;
	}
}
