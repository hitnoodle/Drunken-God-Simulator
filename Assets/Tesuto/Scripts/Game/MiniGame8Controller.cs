using UnityEngine;
using System.Collections;

public class MiniGame8Controller : MiniGameController 
{
	public float MeteorSpeedMultiplier;
	public float SpeedMultiplier;
	public float MaximumIteration;

	private MouseScrollInput mouseScrollInput;
	private ObjectWithTransform[] Meteors;

	private RunningPeoples peoples;


	// Use this for initialization
	public override void StartGame()
	{
		base.StartGame();
		Debug.Log("Mini Game 8 Started");

		mouseScrollInput					= GameObject.FindGameObjectWithTag("Manager").GetComponent<MouseScrollInput>();
		mouseScrollInput.OnMouseScroll		+= OnMouseScroll;

		peoples								= GetComponentInChildren<RunningPeoples>();
        peoples.StartRunning();
		peoples.actHitted					+= SceneEnd;

		Player.ChangeAnimationState(Player.STATE_RUN);
        Player.SetMouseScrollActive(true);

		Meteors			= GetComponentsInChildren<ObjectWithTransform>();
        foreach (ObjectWithTransform meteor in Meteors) meteor.Initialize();
	}

	void OnMouseScroll(float delta){
		float DeltaPosition			= (delta * 20f * Time.deltaTime) * 0.1f;
		if(MeteorSpeedMultiplier - DeltaPosition > 0)
			MeteorSpeedMultiplier		-= DeltaPosition;

		for(int i=0; i<Meteors.Length ;i++){
			Meteors[i].SetVerticalSpeed(MeteorSpeedMultiplier);
		}
	}

	void Update(){
		float Parameter		= (SpeedMultiplier * Time.deltaTime) * 0.1f;
		if(MeteorSpeedMultiplier + Parameter <= MaximumIteration)
			MeteorSpeedMultiplier			+= Parameter;
	}

	public void SceneEnd(){
		mouseScrollInput.OnMouseScroll		-= OnMouseScroll;
        Player.ChangeAnimationState(Player.STATE_IDLE);
		
		if (OnGameDone != null)
			OnGameDone(0f);

        Started = false;
		//Destroy(gameObject);
	}
}