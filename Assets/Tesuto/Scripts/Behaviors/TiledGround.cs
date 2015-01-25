using UnityEngine;
using System.Collections;

public class TiledGround : MonoBehaviour 
{
    public GameObject[] ParentObjects;

    private ParallaxBG _Tiles;

    private SpriteRenderer[] _Lefts;
    private SpriteRenderer[] _Rights;

    private bool _Change = false;
    private bool _ChangeRight = false;
    private bool _ChangeLeft = false;

    private Sprite _NewSprite;
    private Sprite _TransitionSprite;

	// Use this for initialization
	void Start() 
	{
        _Tiles = GetComponent<ParallaxBG>();
        _Tiles.OnBackgroundSwap += BackgroundSwap;
	}

    public void Change(Sprite newSprite, Sprite transitionSprite)
    {
        _Change = true;

        _NewSprite = newSprite;
        _TransitionSprite = transitionSprite;
    }

    void BackgroundSwap()
    {
        if (_Change)
        {
            if (!_ChangeRight)
            {
                int rightmost = 0;
                int leftmost = 0;
                float tempX = 0;

                for (int i = 0; i < _Tiles.Backgrounds.Length; i++)
                {
                    if (_Tiles.Backgrounds[i].transform.position.x > tempX)
                    {
                        rightmost = i;
                        tempX = _Tiles.Backgrounds[i].transform.position.x;
                    }
                }

                leftmost = (rightmost == 0) ? 1 : 0;
                Debug.Log(leftmost + " " + rightmost);

                _Rights = _Tiles.Backgrounds[rightmost].GetComponentsInChildren<SpriteRenderer>();
                _Lefts = _Tiles.Backgrounds[leftmost].GetComponentsInChildren<SpriteRenderer>();

                for (int i = 0; i < _Rights.Length; i++)
                {
                    if (i == 0) _Rights[i].sprite = _TransitionSprite;
                    else _Rights[i].sprite = _NewSprite;
                }

                _ChangeRight = true;
            }
            else if (_ChangeRight && !_ChangeLeft)
            {
                for (int i = 0; i < _Lefts.Length; i++)
                {
                    _Lefts[i].sprite = _NewSprite;
                }

                _Rights[0].sprite = _NewSprite;

                _ChangeLeft = true;
                _Change = false;
            }
        }
    }
}
