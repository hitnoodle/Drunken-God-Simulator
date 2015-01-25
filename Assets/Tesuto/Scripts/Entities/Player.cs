using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    public MouseScrollInput MouseScrollInput;

    public float[] StateSpeed;

    private Animator _anim;
	private BoxCollider2D _ColliderAttack;

    public const int STATE_IDLE         = 0;
	public const int STATE_ASCENT		= 1;
	public const int STATE_ATTACK_DOWN	= 2;
	public const int STATE_ATTACK_UP	= 3;
	public const int STATE_CATAPULT		= 4;
	public const int STATE_KNOCK		= 5;
	public const int STATE_LAUGH		= 6;
	public const int STATE_PICK		    = 7;
	public const int STATE_PULLPUSH		= 8;
	public const int STATE_RUN		    = 9;
	public const int STATE_SAD		    = 10;
    public const int STATE_PARRY_IDLE   = 11;
    public const int STATE_TREE         = 12;
	
	// Use this for initialization
	void Start () {
		_anim			= GetComponent<Animator>();
		_ColliderAttack	= GetComponent<BoxCollider2D>();

		MouseScrollInput.OnMouseScroll	+= MouseScroll;
	}

	void MouseScroll(float Param){
        _anim.speed = Param * 3f;
	}

    public void SetAnimationSpeed(float Param)
    {
        _anim.speed = Param;
    }

	public void ActivateAttackCollider()
    {
		_ColliderAttack.enabled		= true;
	}

	public void DeactivateAttackCollider()
    {
		_ColliderAttack.enabled		= false;
	}

    public void SetMouseScrollActive(bool active)
    { 
        if (active)
            MouseScrollInput.OnMouseScroll += MouseScroll;
        else
            MouseScrollInput.OnMouseScroll -= MouseScroll;
    }

	public void ChangeAnimationState(int STATE){
		_anim.SetInteger("State",STATE);
        _anim.speed = StateSpeed[STATE];
	}

    public int GetAnimationState()
    {
        return _anim.GetInteger("State");
    }

    public void Idle()
    {
        ChangeAnimationState(STATE_IDLE);
    }

    public void IdleParry()
    {
        ChangeAnimationState(STATE_PARRY_IDLE);
    }
}
