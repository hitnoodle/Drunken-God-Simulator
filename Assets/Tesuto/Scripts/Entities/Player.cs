using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private Animator _anim;

	public MouseScrollInput MouseScrollInput;

	public float AnimSpeedParameter;
	private BoxCollider2D _ColliderAttack;

	public const int STATE_ASCENT		= 1;
	public const int STATE_ATTACK_DOWN		= 2;
	public const int STATE_ATTACK_UP		= 3;
	public const int STATE_CATAPULT		= 4;
	public const int STATE_KNOCK		= 5;
	public const int STATE_LAUGH		= 6;
	public const int STATE_PICK		= 7;
	public const int STATE_PULLPUSH		= 8;
	public const int STATE_RUN		= 9;
	public const int STATE_SAD		= 10;
	
	// Use this for initialization
	void Start () {
		_anim			= GetComponent<Animator>();
		_ColliderAttack	= GetComponent<BoxCollider2D>();
		MouseScrollInput.OnMouseScroll	+= MouseScroll;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void MouseScroll(float Param){
		AnimSpeedParameter	= Param;
		_anim.speed		= AnimSpeedParameter;
	}

	public void ActivateAttackCollider(){
		_ColliderAttack.enabled		= true;
	}

	public void DeactivateAttackCollider(){
		_ColliderAttack.enabled		= false;
	}

	public void ChangeAnimationState(int STATE){
		_anim.SetInteger("State",STATE);
	}
}
