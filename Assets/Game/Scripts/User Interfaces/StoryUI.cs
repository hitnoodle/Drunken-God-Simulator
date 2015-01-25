using UnityEngine;
using System.Collections;

using RPG.Stories;

public class StoryUI : MonoBehaviour 
{
    public float ShowDelay = 0.2f;

    public float PanelFade = 1f;
    public bool FadePropBefore = false;

    public float WritingPace = 0.05f;
    public int LineLength = 50;

    public SpriteRenderer Box;
    public SpriteRenderer Continue;

    public TextMesh TextName;
    public TextMesh TextStory;

    public SpriteRenderer[] Props;

    private bool _Started = false;

    private SpriteRenderer _Background;
    private SpriteRenderer _Prop;

    private bool _Writing = false;
    private IEnumerator _WritingRoutine;
    private string _CurrentText;

	// Use this for initialization
	void Start() 
	{
        StoryTeller.OnStart += Show;
        StoryTeller.OnTextEvent += ShowCurrentText;
	}

    void Show()
    {
        Transform[] childs = transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in childs)
        {
            if (t.name == "Props")
            {
                Props = t.GetComponentsInChildren<SpriteRenderer>();
            }
            else if (t.name == "Background")
            {
                _Background = t.GetComponent<SpriteRenderer>();
            }
        }

        if (_Background != null)
        {
            _Background.enabled = true;
        }

        StartCoroutine(ShowUIRoutine(ShowDelay));
    }

    IEnumerator ShowUIRoutine(float delay)
    {
        yield return null;

        if (_WritingRoutine != null) StopCoroutine(_WritingRoutine);

        yield return new WaitForSeconds(delay);

        if (_WritingRoutine != null) StartCoroutine(_WritingRoutine);

        ShowUI();
        _Started = true;
    }

    void ShowUI()
    {
        Box.gameObject.SetActive(true);
        Continue.gameObject.SetActive(true);

        TextName.gameObject.SetActive(true);
        TextStory.gameObject.SetActive(true);
    }

    // Wrap text by line height
    private string ResolveTextSize(string input, int lineLength)
    {
        // Split string by char " "    
        string[] words = input.Split(" "[0]);

        // Prepare result
        string result = "";

        // Temp line string
        string line = "";

        // for each all words     
        foreach (string s in words)
        {
            // Append current word into line
            string temp = line + " " + s;

            // If line length is bigger than lineLength
            if (temp.Length > lineLength)
            {

                // Append current line into result
                result += line + "\n";
                // Remain word append into new line
                line = s;
            }
            // Append current word into current line
            else
            {
                line = temp;
            }
        }

        // Append last line into result   
        result += line;

        // Remove first " " char
        return result.Substring(1, result.Length - 1);
    }

    void ShowCurrentText(StoryText storyText)
    {
        if (storyText.Metadata != null)
        {
            ShowProp(storyText.Metadata);
        } 

        TextName.text = storyText.Name;
        TextStory.text = "";

        _Writing = true;
        _CurrentText = ResolveTextSize(storyText.Text, LineLength);
        _WritingRoutine = WriteText(_CurrentText);

        StartCoroutine(_WritingRoutine);
    }

    IEnumerator WriteText(string text)
    { 
        char[] textArray = text.ToCharArray();
        foreach (char c in textArray)
        {
            TextStory.text += c;
            yield return new WaitForSeconds(WritingPace);
        }

        _Writing = false;
    }

    public void Next()
    { 
        if (_Started)
        {
            if (_Writing)
            {
                StopCoroutine(_WritingRoutine);
                TextStory.text = _CurrentText;

                _Writing = false;
            }
            else
            {
                StoryTeller.Continue(0);
            }
        }
    }
	
	// Update is called once per frame
	void Update() 
	{
        if (Input.GetMouseButtonDown(0))
            Next();
	}

    SpriteRenderer GetProp(string name)
    {
        foreach (SpriteRenderer s in Props)
            if (s.name == name) return s;

        return null;
    }

    void ShowProp(string name)
    {
        SpriteRenderer prop = GetProp(name);
        if (prop != null)
        {
            prop.enabled = true;
            StartCoroutine(FadeIn(prop, PanelFade));
        }
    }

    IEnumerator FadeIn(SpriteRenderer sprite, float time)
    {
        bool finished = false;
        float currentTime = 0f;

        while (!finished)
        {
            currentTime += Time.deltaTime;
            float percentage = Mathf.Lerp(0, 1, currentTime / time);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, percentage);

            if (percentage > 0.99f)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
                finished = true;
            }

            yield return null;
        }

        if (_Prop != null && FadePropBefore)
        {
            //_Prop.color = new Color(_Prop.color.r, _Prop.color.g, _Prop.color.b, 0);
            //_Prop.enabled = false;
            StartCoroutine(FadeOut(_Prop, PanelFade));
        }

        _Prop = sprite;
    }

    IEnumerator FadeOut(SpriteRenderer sprite, float time)
    {
        bool finished = false;
        float currentTime = 0f;

        while (!finished)
        {
            currentTime += Time.deltaTime;
            float percentage = Mathf.Lerp(1, 0, currentTime / time);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, percentage);

            if (percentage < 0.01f)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
                finished = true;
            }

            yield return null;
        }
    }

    void HideProp(string name)
    {
        SpriteRenderer prop = GetProp(name);
        if (prop != null) prop.enabled = false;
    }

    public void HideUI()
    {
        StopAllCoroutines();

        Box.gameObject.SetActive(false);
        Continue.gameObject.SetActive(false);

        TextName.gameObject.SetActive(false);
        TextStory.gameObject.SetActive(false);
    }

	void OnDestroy()
	{
		StoryTeller.OnStart -= Show;
		StoryTeller.OnTextEvent -= ShowCurrentText;
	}    
}
