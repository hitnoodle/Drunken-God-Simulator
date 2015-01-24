using UnityEngine;
using System.Collections;

public class ObjectAttribute : MonoBehaviour {
	public float AWARENESS		= 10f; 

	private Animator _anim;
	void Start(){
		_anim				= GetComponent<Animator>();
	}

	public void SetEvent(int param){
		_anim.SetInteger("event",0);
	}

}