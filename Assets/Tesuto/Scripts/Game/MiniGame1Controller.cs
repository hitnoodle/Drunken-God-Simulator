using UnityEngine;
using System.Collections;

public class MiniGame1Controller : MiniGameController
{
    public int KnockedTime;
    public ObjectWithCollider Door;

    private Player player;

    public override void StartGame()
    {
        base.StartGame();
        Debug.Log("Mini Game 1 Started");

        Door.onCollideWithPlayer += DoorKnocked;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.ChangeAnimationState(Player.STATE_KNOCK);
        player.SetMouseScrollActive(true);
    }

    void DoorKnocked()
    {
        if (!Started) return;

        player.DeactivateAttackCollider();
        KnockedTime--;

        if (KnockedTime <= 0)
            SceneEnd();
    }

    public void SceneEnd()
    {
        player.ChangeAnimationState(Player.STATE_IDLE);
        player.SetMouseScrollActive(false);

        if (OnGameDone != null)
            OnGameDone(0f);

        Destroy(gameObject);
    }
}