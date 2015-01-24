using UnityEngine;
using System.Collections;

public class GameTestScene : MonoBehaviour 
{
    public float Speed;
    public float CurrentDistance;

    public float[] EventDistances;

    public MouseScrollInput MouseScroll;
    public ParallaxBG[] Backgrounds;

    public int EventIndex = 4;

    private float _InitialSpeed;
    private bool _OnEvent = false;

    private Player _Player;
    private MiniGameController _CurrentMiniGame;

	// Use this for initialization
	void Start() 
	{
        _InitialSpeed = Speed;
	    MouseScroll.OnMouseScroll += OnMouseScroll;

        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        _Player.ChangeAnimationState(Player.STATE_RUN);
        _Player.SetMouseScrollActive(false);
	}

    void OnMouseScroll(float delta)
    {
        if (_OnEvent) return;

        Speed += delta;
        SetBackgroundSpeed(Speed / _InitialSpeed);
    }

    void SetBackgroundSpeed(float factor)
    {
        foreach (ParallaxBG bg in Backgrounds)
            bg.Factor = factor;
    }
	
	// Update is called once per frame
	void Update() 
	{
        if (_OnEvent) return;

        CurrentDistance += Speed * Time.deltaTime;

        if (EventIndex < EventDistances.Length && CurrentDistance >= EventDistances[EventIndex]) 
        {
            CurrentDistance = EventDistances[EventIndex];
            SetBackgroundSpeed(0);

            EventIndex++;
            if (EventIndex == 3) EventIndex = 5;
            else if (EventIndex == 6)
            {
                EventIndex = 1;
                CurrentDistance = 0;
            }

            StartMiniGame(EventIndex);

            _OnEvent = true;
        }
	}

    void StartMiniGame(float index)
    {
        CameraZoom.Zoom("Mini Game - " + index);
        CameraZoom.onZoomFinished += SpawnMiniGame;

        _Player.ChangeAnimationState(Player.STATE_IDLE);

        GameObject miniGameObject = Instantiate(Resources.Load<GameObject>("Mini Game/Mini Game - " + EventIndex)) as GameObject;
        _CurrentMiniGame = miniGameObject.GetComponent("MiniGame" + EventIndex + "Controller") as MiniGameController;
    }

    void SpawnMiniGame()
    {
        CameraZoom.onZoomFinished -= SpawnMiniGame;

        _CurrentMiniGame.StartGame();
        _CurrentMiniGame.OnGameDone += MiniGameDone;
    }

    void MiniGameDone(float result)
    {
        CameraZoom.Zoom("Normal");
        CameraZoom.onZoomFinished += StartMovingAgain;
    }

    void StartMovingAgain()
    {
        CameraZoom.onZoomFinished -= StartMovingAgain;

        Speed = _InitialSpeed;
        SetBackgroundSpeed(1f);

        _Player.ChangeAnimationState(Player.STATE_RUN);

        _OnEvent = false;
    }
}
