using UnityEngine;
using System.Collections;

public class ObjectWithAnimation : MonoBehaviour {
	private Animator _anim;
	
	// Use this for initialization
	void Start () {
		Initialize();
	}

	void Initialize(){
		_anim = GetComponent<Animator>();
        _anim.speed = 0;
	}

	public void SetAnimationSpeed(float Param){
		if(_anim != null)
			_anim.speed = Param;
		else{
			Initialize();
			_anim.speed = Param;
		}
	}

	public void ChangeAnimationState(string state){
		_anim.SetTrigger(state);
	}
}