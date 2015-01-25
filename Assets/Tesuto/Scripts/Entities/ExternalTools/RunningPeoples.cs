using UnityEngine;
using System.Collections;

public class RunningPeoples : MonoBehaviour {
	private Transform _Transform;
	private Vector3 position;

	public float HorizontalSpeed	= 0f;

	public delegate void Action();
	public event Action actHitted;

    private bool _IsStarted = false;

    public void StartRunning()
    {
        _Transform = transform;
        position = _Transform.position;

        _IsStarted = true;
    }

	void HorizontalMovement(){
		position				= _Transform.position;
		position.x				+= HorizontalSpeed * Time.deltaTime;
		_Transform.position		= position;
	}

	void Update(){
        if (_IsStarted) HorizontalMovement();
	}

	void OnTriggerEnter2D(Collider2D coll){
		string objectTag = coll.gameObject.tag;
		if (objectTag == "Meteor")
		{
//			if(actHitted != null)
//				actHitted();
		}
		if (objectTag == "FinishLine")
		{
			if(actHitted != null)
				actHitted();
		}
	}
}