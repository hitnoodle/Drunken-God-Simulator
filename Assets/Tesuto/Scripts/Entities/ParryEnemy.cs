using UnityEngine;
using System.Collections;

public class ParryEnemy : MonoBehaviour 
{
    public const int STATE_ATTACK_UP = 0;
    public const int STATE_ATTACK_DOWN = 1;

    public float MinSpeed = 5;
    public float MaxSpeed = 10;

    public float AttackDelta = 2f;
    public float DeathDelta = 1.5f;

    public float[] StateSpeed;

    private Player _Player;
    private Animator _Animator;

    private IEnumerator AttackRoutine;

	// Use this for initialization
	void Start() 
	{
        _Animator = GetComponent<Animator>();
	}

    void ChangeAnimationState(int STATE)
    {
        _Animator.SetInteger("State", STATE);
        _Animator.speed = StateSpeed[STATE];
    }

    // 1 is up, 2 is down
    public void Attack(Player player, int direction)
    {
        GetComponent<SpriteRenderer>().enabled = true;

        _Player = player;

        ChangeAnimationState(direction);

        AttackRoutine = Attaack();
        StartCoroutine(AttackRoutine);
    }

    IEnumerator Attaack()
    {
        float speed = Random.Range(MinSpeed, MaxSpeed);

        bool onDestination = false;
        while (!onDestination && transform.position.x > _Player.transform.position.x + AttackDelta)
        {
            float newPos = transform.position.x - (Time.deltaTime * speed);
            if (newPos < _Player.transform.position.x + AttackDelta)
            {
                newPos = _Player.transform.position.x + AttackDelta;
                onDestination = true;
            } 

            transform.position = new Vector3(newPos, transform.position.y, transform.position.z);
            yield return null;
        }

        while (true)
        {
            transform.position = new Vector3(transform.position.x - (Time.deltaTime * speed * 0.1f), transform.position.y, transform.position.z);
            if (transform.position.x <= DeathDelta) StartCoroutine(OutFalse());

            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        string objectTag = coll.gameObject.tag;
        if (objectTag == "Player")
        {
            StopCoroutine(AttackRoutine);

            Player player = coll.gameObject.GetComponent<Player>();
            int playerState = player.GetAnimationState();
            int enemyState = _Animator.GetInteger("State");

            if (playerState == Player.STATE_ATTACK_UP && enemyState == STATE_ATTACK_UP)
            {
                StartCoroutine(Out());

            }
            else if (playerState == Player.STATE_ATTACK_DOWN && enemyState == STATE_ATTACK_DOWN)
            {
                StartCoroutine(Out());
            }
            else
            {
                StartCoroutine(OutFalse());
            }

        }
    }

    IEnumerator Out()
    {
        float speedX = Random.Range(10, 15);
        float speedY = Random.Range(-15, 15);

		SoundManager.PlaySoundEffect("sword baru");

        while (transform.position.x <= 13)
        {
            transform.position = new Vector3(transform.position.x + speedX * Time.deltaTime, transform.position.y + speedY * Time.deltaTime, transform.position.z);
            transform.Rotate(0, 0, 30);
            
            yield return null;
        }

        Destroy(gameObject);
    }

    IEnumerator OutFalse()
    {
        while (transform.position.x <= 13)
        {
            float speedX = Random.Range(10, 15);
            float speedY = Random.Range(-15, 15);

            transform.position = new Vector3(transform.position.x + speedX * Time.deltaTime, transform.position.y + speedY * Time.deltaTime, transform.position.z);

            yield return null;
        }

		SoundManager.PlaySoundEffect("sword slice baru");

        Destroy(gameObject);
    }
}
