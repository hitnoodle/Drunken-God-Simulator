using UnityEngine;
using System.Collections;

public class TitleSceneController : MonoBehaviour 
{
    protected bool _Done = false;
    protected Animator _Animator;
	
    // Use this for initialization
	void Start() 
	{
        _Animator = GetComponent<Animator>();
		SoundManager.PlayBackgroundMusic("LOL to LOL",true);
	}
	
	// Update is called once per frame
	void Update() 
	{
        if (!_Done && Input.GetAxis("Mouse ScrollWheel") > 0.05f)
        {
            _Animator.SetBool("IsExit", true);
            _Done = true;   
        }
	}

    void Done()
    {
        PlayerPrefs.SetString("Story", "Opening");
        Application.LoadLevel("StoryScene");
    }
}
