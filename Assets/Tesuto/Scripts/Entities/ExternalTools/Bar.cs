using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour {
	private Transform _Transform;
	private Vector3 _Scale;

	private ObjectAttribute parameter;
	private MiniGame3Controller objectParameter;
	private float maximumParameter;
	private float defaultScaleY;

	private SpriteRenderer _Sprite;
	// Use this for initialization
	void Start () {
		_Transform			= transform;
		_Scale				= _Transform.localScale;

		parameter			= GetComponentInParent<ObjectAttribute>();
		objectParameter		= GetComponentInParent<MiniGame3Controller>();
		maximumParameter	= objectParameter.DefaultMonsterAwareness;
		_Sprite				= GetComponent<SpriteRenderer>();

		defaultScaleY		= _Scale.y;
	}
	
	void Update(){
		_Scale				= _Transform.localScale;

		_Scale.y			= (parameter.AWARENESS/maximumParameter)*defaultScaleY;

		_Transform.localScale	= _Scale;
	}

	public void SetColor(Color color){
		_Sprite.color		= color;
	}
}
