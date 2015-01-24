using UnityEngine;
using System.Collections;

public class MiniGame1Controller : MiniGameController 
{
	// Use this for initialization
	void Start() 
	{
	
	}
	
	// Update is called once per frame
	void Update() 
	{
        if (!Started) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (OnGameDone != null)
                OnGameDone(0f);

            Destroy(gameObject);
        }
	}
}
