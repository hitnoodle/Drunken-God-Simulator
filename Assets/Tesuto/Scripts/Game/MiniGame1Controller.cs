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
	
	// Update is called once per frame
	void Update() 
	{
        if (!Started) return;
//
//        if (Input.GetMouseButtonDown(0))
//        {
//			SceneEnd();
//        }
	}

	public void SceneEnd(){
	    if (OnGameDone != null)
	        OnGameDone(0f);
	    Destroy(gameObject);


	}
}
