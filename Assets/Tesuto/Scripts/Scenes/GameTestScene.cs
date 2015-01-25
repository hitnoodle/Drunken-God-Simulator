using UnityEngine;
using System.Collections;

public class GameTestScene : MonoBehaviour 
{
    public float Speed;
    public float CurrentDistance;

    [System.Serializable]
    public class MiniGameData
    {
        public MiniGameController MiniGame;

        public float Distance;
        public int Index;
    }
    public MiniGameData[] Data;

    public MouseScrollInput MouseScroll;
    public ParallaxBG[] Backgrounds;
    public ScrollBG[] Objects;

    public int EventIndex = 0;
    public Player _Player;

    [System.Serializable]
    public class TileData
    {
        public Sprite TransitionSprite;
        public Sprite NewSprite;

        public float Distance;
    }
    public TileData[] Tiles;
    public TiledGround TiledGround;
    public int TileIndex = 0;

    private float _InitialSpeed;
    private bool _OnEvent = false;

    private MiniGameController _CurrentMiniGame;

	// Use this for initialization
	void Start() 
	{
        _InitialSpeed = Speed;
        MouseScroll.OnMouseScroll += OnMouseScroll;

        _Player.ChangeAnimationState(Player.STATE_RUN);
        _Player.SetMouseScrollActive(false);

        SoundManager.PlayBackgroundMusic("Celtic World baru", true);
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

        foreach (ScrollBG bg in Objects)
            bg.Factor = factor;
    }
	
	// Update is called once per frame
	void Update() 
	{
        if (_OnEvent) return;

        CurrentDistance += Speed * Time.deltaTime;

        if (EventIndex < Data.Length && CurrentDistance >= Data[EventIndex].Distance) 
        {
            CurrentDistance = Data[EventIndex].Distance;
            SetBackgroundSpeed(0);

            StartMiniGame(EventIndex);
            EventIndex++;

            _OnEvent = true;
        }
		else if (EventIndex == Data.Length)
		{
			int ending = Random.Range(0, 2);
			
			PlayerPrefs.SetInt("Ending", ending);
			PlayerPrefs.SetString("Story", "Ascention");
			Application.LoadLevel("StoryScene");
		}

        if (TileIndex < Tiles.Length && CurrentDistance >= Tiles[TileIndex].Distance)
        {
            TiledGround.Change(Tiles[TileIndex].NewSprite, Tiles[TileIndex].TransitionSprite);
            TileIndex++;
        }
	}

    void StartMiniGame(int index)
    {
        int gameIndex = Data[index].Index;

        CameraZoom.Zoom("Mini Game - " + gameIndex);
        CameraZoom.onZoomFinished += SpawnMiniGame;

        _Player.ChangeAnimationState(Player.STATE_IDLE);

        _CurrentMiniGame = Data[index].MiniGame;
        _CurrentMiniGame.Player = _Player;
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
