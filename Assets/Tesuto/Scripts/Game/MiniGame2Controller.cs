using UnityEngine;
using System.Collections;

public class MiniGame2Controller : MiniGameController 
{
	public ObjectWithAnimation TheTree;
    public bool IsSoundPlaying = false;

	private MouseScrollInput mouseScrollInput;

    public override void StartGame()
    {
        base.StartGame();

        Debug.Log("Mini Game 2 Started");

        mouseScrollInput = GameObject.FindGameObjectWithTag("Manager").GetComponent<MouseScrollInput>();
        mouseScrollInput.OnMouseScroll += OnMouseScroll;

		Player.ChangeAnimationState(Player.STATE_TREE);

        TheTree.SetAnimationSpeed(0f);
        TheTree.finishState += SceneEnd;
    }

	void OnMouseScroll(float delta)
    {
		if(delta < 0f){
			if (!IsSoundPlaying)
			{
				SoundManager.PlaySoundEffectOneShot("magic hand of nabi");
				IsSoundPlaying = true;
			}
			TheTree.SetAnimationSpeed(-1 * delta);
		}
			
	}

	public void SceneEnd()
	{
		mouseScrollInput.OnMouseScroll -= OnMouseScroll;

        SoundManager.StopSoundEffect("magic hand of nabi");

        TheTree.SetAnimationSpeed(0);
        Player.ChangeAnimationState(Player.STATE_IDLE);
		
		if (OnGameDone != null)
			OnGameDone(0f);

        Started = false;
		//Destroy(gameObject);
	}
}
