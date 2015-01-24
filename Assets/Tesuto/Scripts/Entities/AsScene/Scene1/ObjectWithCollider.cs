using UnityEngine;
using System.Collections;

public class ObjectWithCollider : MonoBehaviour {
	MiniGame1Controller SceneController;
	public int KnockedTime;

	// Use this for initialization
	void Start () {
		SceneController		= GetComponentInParent<MiniGame1Controller>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FinishScene(){
		SceneController.SceneEnd();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		string objectTag 	= coll.gameObject.tag;
		if(objectTag == "Player"){
			Player player		= coll.gameObject.GetComponent<Player>();
			player.DeactivateAttackCollider();
			KnockedTime--;
			if(KnockedTime <= 0)
				FinishScene();
		}
	}
}
