using UnityEngine;
using System.Collections;

public class ParryEnemy : MonoBehaviour 
{
    public const int STATE_RUN = 0;
    public const int STATE_ATTACK_UP = 1;
    public const int STATE_ATTACK_DOWN = 2;

    public float MinSpeed = 5;
    public float MaxSpeed = 10;

    public float AttackDelta = 2f;
    public int HowManyAttack = 3;

    public float[] StateSpeed;

    public delegate void _OnFinishedAttacking();
    public _OnFinishedAttacking OnFinishedAttacking;

    private float _StartX;
    private Player _Player;
    private Animator _Animator;

	// Use this for initialization
	void Start() 
	{
        _StartX = transform.position.x;
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _Animator = GetComponent<Animator>();

        StartCoroutine(Attaack());
	}

    IEnumerator Attaack()
    {
        float speed = Random.Range(MinSpeed, MaxSpeed);

        while (transform.position.x > _Player.transform.position.x + AttackDelta)
        {
            float newPos = transform.position.x - (Time.deltaTime * speed);
            if (newPos < _Player.transform.position.x + AttackDelta) newPos = _Player.transform.position.x + AttackDelta;

            transform.position = new Vector3(newPos, transform.position.y, transform.position.z);
            yield return null;
        }

        transform.position = new Vector3(_Player.transform.position.x + AttackDelta, transform.position.y, transform.position.z);
        int attack = Random.Range(0, 2) + 1;
        ChangeAnimationState(attack);
        if (attack == 0)
        { 
            
        }
        else if (attack == 1)
        { 
        
        }
    }

    void Back()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        ChangeAnimationState(STATE_RUN);

        StartCoroutine(Baack());
    }

    IEnumerator Baack()
    {
        float speed = Random.Range(MinSpeed, MaxSpeed);

        while (transform.position.x < _StartX)
        {
            float newPos = transform.position.x + (Time.deltaTime * speed);
            if (newPos > _StartX) newPos = _StartX;

            transform.position = new Vector3(newPos, transform.position.y, transform.position.z);
            yield return null;
        }

        HowManyAttack--;
        if (HowManyAttack <= 0)
        {
            if (OnFinishedAttacking != null)
                OnFinishedAttacking();
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            StartCoroutine(Attaack());
        }
    }

    public void ChangeAnimationState(int STATE)
    {
        _Animator.SetInteger("State", STATE);
        _Animator.speed = StateSpeed[STATE];
    }
}
