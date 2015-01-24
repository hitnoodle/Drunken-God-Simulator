using UnityEngine;
using System.Collections;

public class MouseScrollInput : MonoBehaviour 
{
    // Increases the target translation
    public float Speed = 100.0f;

    // Maximum acceleration possible by scrolling the wheel faster
    public float MaxAcceleration = 2.0f;

    // How quickly to follow the target
    public float FollowSpeed = 10.0f;

    // Across which Vector to translate (Default is Y axis)
    public Vector3 TranslationVector = new Vector3(0, 1, 0);

    // Whether or not to use the scrollwheel acceleration
    public bool ScrollWheelAcceleration = true;

    public delegate void _OnMouseScroll(float delta);
    public _OnMouseScroll OnMouseScroll;

    private float _Timer;
    private float _Translation;
    private float _Position;
    private float _Target;
    private float _Falloff;
    private float _Input;

	// Use this for initialization
	void Start() 
	{
	
	}
	
	// Update is called once per frame
	void Update() 
	{
        _Timer += Time.deltaTime;
        _Input = Input.GetAxis("Mouse ScrollWheel");

        // This is the acceleration according to the time difference between the "clicks" of the mousewheel
        // If you leave that out, it will be more like Opera scrolling (larger discrete steps but smooth follow)
        // The "300" could be adjusted (lower means larger steps, stronger acceleration)
        if (ScrollWheelAcceleration)
        {
            if (_Input != 0)
            {
                _Target += Mathf.Clamp((_Input / (_Timer * 300)) * Speed, MaxAcceleration * -1, MaxAcceleration);
                _Timer = 0;
            }
        }
        else // Opera-Style
        {
            _Target += Mathf.Clamp(_Input * Speed, MaxAcceleration * -1, MaxAcceleration);
        }

        // As a falloff we use the distance between position and target
        // results in faster Movement at higher distances
        _Falloff = Mathf.Abs(_Position - _Target);

        // Determine the amount of translation for this frame
        _Translation = Time.deltaTime * _Falloff * FollowSpeed;

        // 0.001 is our deadzone
        if (_Position + 0.001 < _Target)
        {
            //this.GetComponent<Transform>().Translate(translationVector * translation * -1);
            _Position += _Translation;

            if (OnMouseScroll != null) OnMouseScroll(_Translation * -1);
        }
        if (_Position - 0.001 > _Target)
        {
            //this.GetComponent<Transform>().Translate(translationVector * translation);
            _Position -= _Translation;

            if (OnMouseScroll != null) OnMouseScroll(_Translation);
        }

        //Debug.Log(_Position + " " + _Translation);
	}
}
