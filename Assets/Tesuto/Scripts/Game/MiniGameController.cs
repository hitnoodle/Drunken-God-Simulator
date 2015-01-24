using UnityEngine;
using System.Collections;

public class MiniGameController : MonoBehaviour 
{
    public int Index;
    public bool Started = false;

    public delegate void _OnGameDone(float result);
    public _OnGameDone OnGameDone;

    public void StartGame()
    {
        Started = true;
    }
}
