using UnityEngine;
using System.Collections;

public class MiniGameController : MonoBehaviour 
{
    public bool Started = false;
    public Player Player;

    public delegate void _OnGameDone(float result);
    public _OnGameDone OnGameDone;

    public virtual void StartGame()
    {
        Started = true;
    }
}
