using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {
	private ObjectWithTransform MoveController;
	private BoxCollider2D _collider;

	private Animator _anim;

	// Use this for initialization
	void Start () {
		MoveController	= GetComponent<ObjectWithTransform>();
		_collider		= GetComponent<BoxCollider2D>();
		_anim			= GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void ChangeAnimationState(int state){
		_anim.SetInteger("boom",state);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		string objectTag = coll.gameObject.tag;
		if (objectTag == "Ground")
		{
			MoveController.VerticalSpeed	= 0f;
			ChangeAnimationState(1);
			StartCoroutine(ChangeToFire(0.3f));
//			audio.PlayOneShot();
			SoundManager.PlaySoundEffect("explosion baru");
		}
		else if(objectTag == "Peoples"){
			MoveController.VerticalSpeed	= 0f;
			ChangeAnimationState(1);
			StartCoroutine(ChangeToFire(0.3f));
			Vector3 pos		= this.gameObject.transform.localPosition;
			pos.y			-= 0.4f;
			this.gameObject.transform.localPosition = pos;
			this.gameObject.transform.parent		= coll.gameObject.transform;
//			audio.PlayOneShot();
			SoundManager.PlaySoundEffect("explosion baru");
		}

        _collider.enabled = false;
	}

	IEnumerator ChangeToFire(float time){
		yield return new WaitForSeconds(time);
		ChangeAnimationState(2);
	}
}
