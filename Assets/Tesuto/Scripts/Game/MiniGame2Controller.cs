using UnityEngine;
using System.Collections;

public class MiniGame2Controller : MiniGameController 
{
	public string STATE_TREE_GROW;

	public ObjectWithAnimation TheTree;
	public float MaximumGrow;

	public float paramGrow;

    public bool IsSoundPlaying = false;
    public AudioClip TreeClip;

	private Player player;
	private MouseScrollInput mouseScrollInput;

    public override void StartGame()
    {
        base.StartGame();
        Debug.Log("Mini Game 2 Started");

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mouseScrollInput = GameObject.FindGameObjectWithTag("Manager").GetComponent<MouseScrollInput>();
        mouseScrollInput.OnMouseScroll += OnMouseScroll;

        TheTree.SetAnimationSpeed(0f);
    }

	void OnMouseScroll(float delta)
    {
        if (!IsSoundPlaying)
        {
            AudioSource.PlayClipAtPoint(TreeClip, Vector3.zero);
            IsSoundPlaying = true;
        }

		TheTree.SetAnimationSpeed(delta);
        
		if(paramGrow <= MaximumGrow)
			paramGrow += delta;
		else
			SceneEnd();
	}

	public void SceneEnd()
	{
		mouseScrollInput.OnMouseScroll -= OnMouseScroll;
		player.ChangeAnimationState(Player.STATE_IDLE);
		
		if (OnGameDone != null)
			OnGameDone(0f);
		
		Destroy(gameObject);
	}
}
