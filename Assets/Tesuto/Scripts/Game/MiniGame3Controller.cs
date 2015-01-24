using UnityEngine;
using System.Collections;

public class MiniGame3Controller : MiniGameController {
	public int NumberOfPulled			= 3;
	private Player player;
	private MouseScrollInput mouseScrollInput;

	private ObjectWithAnimation[] TheCreatures;
	public ObjectWithTransform Rope;
	public ObjectAttribute monsterAttribute;
	public float awarenessModifier				= 10f;

	[HideInInspector]
	public float DefaultMonsterAwareness		= 0f;

	private bool BarIsIncreased					= false;
	public bool MonsterCanBePulled				= false;

	public Bar _bar;

	public Color colorAwareDown;
	public Color colorAwarUp;

	public bool CanMinusThePoint				= true;
	// Use this for initialization
	void Start () {
		player								= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		player.ChangeAnimationState(Player.STATE_PULLPUSH);
		mouseScrollInput					= GameObject.FindGameObjectWithTag("Manager").GetComponent<MouseScrollInput>();
		mouseScrollInput.OnMouseScroll		+= OnMouseScroll;

		TheCreatures						= this.GetComponentsInChildren<ObjectWithAnimation>();

		DefaultMonsterAwareness				= monsterAttribute.AWARENESS;
	}

	void OnMouseScroll(float delta){
		for(int i=0; i<TheCreatures.Length ;i++){
			TheCreatures[i].SetAnimationSpeed(delta);
		}
		Rope.SetXPosition(-delta/300f);
		if(MonsterCanBePulled){
			monsterAttribute.AWARENESS		= DefaultMonsterAwareness;
		}
	}

	void Update(){
		if(BarIsIncreased)
			monsterAttribute.AWARENESS += awarenessModifier * Time.deltaTime;
		else
			monsterAttribute.AWARENESS -= awarenessModifier * Time.deltaTime;

		if(monsterAttribute.AWARENESS <= 0 || monsterAttribute.AWARENESS >= DefaultMonsterAwareness)
			BarIsIncreased		= !BarIsIncreased;

		if(monsterAttribute.AWARENESS >= (0.5f * DefaultMonsterAwareness)){
			MonsterCanBePulled		= true;
			CanMinusThePoint		= true;
			_bar.SetColor(colorAwareDown);

			monsterAttribute.SetEvent(0);
		}
		else{
			monsterAttribute.SetEvent(1);
			MonsterCanBePulled		= false;
			_bar.SetColor(colorAwarUp);
			if(CanMinusThePoint){
				CanMinusThePoint		= false;
				NumberOfPulled--;
				if(NumberOfPulled <= 0)
					SceneEnd();
			}
		}
	}

	public void SceneEnd()
	{
		mouseScrollInput.OnMouseScroll		-= OnMouseScroll;
		player.ChangeAnimationState(Player.STATE_IDLE);
		
		if (OnGameDone != null)
			OnGameDone(0f);
		
		Destroy(gameObject);
	}
}