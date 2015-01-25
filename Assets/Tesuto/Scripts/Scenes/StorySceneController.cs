using UnityEngine;
using System.Collections;

using RPG.Stories;

public class StorySceneController : MonoBehaviour 
{
    public string Name; 

	// Use this for initialization
	void Start() 
	{
        if (PlayerPrefs.HasKey("Story"))
        {
            Name = PlayerPrefs.GetString("Story");
            PlayerPrefs.DeleteKey("Story");
        }

        StartCoroutine(StartScene());
        StoryTeller.OnEnd += FinishStory;

		if(SoundManager.IsBackgroundMusicPlaying())
			SoundManager.PlayBackgroundMusic("LOL to LOL",true);
	}

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(1f);

        StoryTeller.Start(Name);
    }

    void FinishStory()
    {
		StoryTeller.DestroyStory();
		
		if (Name == "Opening") Application.LoadLevel("GameTestScene");
		else if (Name == "Ascention")
		{
			int ending = PlayerPrefs.GetInt("Ending");
			
			if (ending == 0)
				PlayerPrefs.SetString("Story", "Ending Bad");
			else if (ending == 1)
				PlayerPrefs.SetString("Story", "Ending Good");
			
			Application.LoadLevel("StoryScene");
		}
		else if (Name.Contains("Ending")) Application.LoadLevel("TitleScene");
    }
}
