using UnityEngine;
using System.Collections;

public class MiniGame1Controller : MiniGameController
{
    public int KnockedTime;
    public ObjectWithCollider Door;
	public SpriteRenderer DoorSprite;
	public Sprite DoorOpen;
	public GameObject PaKades;

    public override void StartGame()
    {
        base.StartGame();
        Debug.Log("Mini Game 1 Started");

        Door.onCollideWithPlayer += DoorKnocked;

        Player.ChangeAnimationState(Player.STATE_KNOCK);
        Player.SetMouseScrollActive(true);
    }

    void DoorKnocked()
    {
        if (!Started) return;

        Player.DeactivateAttackCollider();
        KnockedTime--;

		SoundManager.PlaySoundEffectOneShot("knock the door hard");

        if (KnockedTime <= 0)
            SceneEnd();
    }

    public void SceneEnd()
    {
		DoorSprite.sprite		= DoorOpen;
		PaKades.SetActive(true);

        Player.ChangeAnimationState(Player.STATE_IDLE);
        Player.SetMouseScrollActive(false);

        if (OnGameDone != null)
            OnGameDone(0f);

        Door.onCollideWithPlayer -= DoorKnocked;
        Started = false;
    }
}