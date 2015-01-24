using UnityEngine;
using System.Collections;

public class MiniGame1Controller : MiniGameController 
{
	private Player player;
	
	// Use this for initialization
	void Start() 
	{
		player		= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		player.ChangeAnimationState(Player.STATE_KNOCK);
	}

    public override void StartGame()
    {
        base.StartGame();
    }

	public void SceneEnd()
	{
		if (!Started) return;

	    if (OnGameDone != null) OnGameDone(0f);

	    Destroy(gameObject);
	}
}
