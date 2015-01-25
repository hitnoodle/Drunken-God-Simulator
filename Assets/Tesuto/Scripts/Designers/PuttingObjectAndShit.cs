using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PuttingObjectAndShit : MonoBehaviour 
{
    public GameTestScene MiniGamesDistances;
    public GameObject[] MiniGames;

    public bool PutObject = false;

    private float _PlayerSpeed;
    private float _LayerSpeed;

	// Use this for initialization
	void Start() 
	{
        _PlayerSpeed = MiniGamesDistances.Speed;
        _LayerSpeed = GetComponent<ScrollBG>().Speed;
	}

    void SetMiniGamePositions()
    {
        for (int i = 0; i < MiniGames.Length; i++)
        {
            GameObject go = MiniGames[i];

            // Our games are only one digit
            //string indexString = go.name.Substring(go.name.IndexOf("Mini Game - ") + 12, 1);
            //int index = System.Int32.Parse(indexString);
            
            foreach(GameTestScene.MiniGameData data in MiniGamesDistances.Data)
            {
                if (data.MiniGame.name == go.name)
                {
                    float distance = data.Distance;
                    SetObjectPosition(go, distance);   
                }
            }
        }
    }

    void SetObjectPosition(GameObject objectToPut, float distance)
    {
        float duration = distance / _PlayerSpeed;
        float newPosition = duration * _LayerSpeed * -1;

        objectToPut.transform.position = new Vector3(newPosition, objectToPut.transform.position.y, objectToPut.transform.position.z);
    }
	
	// Update is called once per frame
	void Update() 
	{
        if (PutObject)
        {
            SetMiniGamePositions();
            PutObject = false;
        }
	}
}
