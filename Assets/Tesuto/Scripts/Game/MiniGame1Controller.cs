using UnityEngine;
using System.Collections;

public class MiniGame1Controller : MiniGameController 
{
	// Use this for initialization
	void Start() 
	{
	
	}

    public override void StartGame()
    {
        base.StartGame();
    }
	
	// Update is called once per frame
	void Update() 
	{
        if (!Started) return;

        if (Input.GetMouseButtonDown(1))
        {
            if (OnGameDone != null)
                OnGameDone(0f);

            Destroy(gameObject);
        }
	}
}
