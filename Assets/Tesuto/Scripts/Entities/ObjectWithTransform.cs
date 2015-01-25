using UnityEngine;
using System.Collections;

public class ObjectWithTransform : MonoBehaviour {
	private Transform _Transform;
	private Vector3 position;

	public float VerticalSpeed		= 0f;
	public float HorizontalSpeed	= 0f;

	public float VerticalSpeedMultiplier		= 1f;
	public bool VerticalIsActive	= false;

    private bool _Initialized = false;

    public void Initialize()
    {
        _Transform = transform;
        position = _Transform.position;

        _Initialized = true;
    }

	public void SetXPosition(float delta){
		position				= _Transform.position;
		position.x				+= delta;
		_Transform.position		= position;
	}

	void VerticalMovement(){
        if (!_Initialized) return;

		if(VerticalIsActive){
			position				= _Transform.position;
			position.y				-= VerticalSpeedMultiplier * VerticalSpeed * Time.deltaTime;
			_Transform.position		= position;
		}

	}

	void HorizontalMovement(){
        if (!_Initialized) return;

		position				= _Transform.position;
		position.x				+= HorizontalSpeed * Time.deltaTime;
		_Transform.position		= position;
	}

	public void SetVerticalSpeed(float param){
		VerticalSpeedMultiplier			= param;
	}



	void Update(){
		VerticalMovement();
		HorizontalMovement();
	}
}
