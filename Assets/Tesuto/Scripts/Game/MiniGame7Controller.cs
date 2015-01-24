using UnityEngine;
using System.Collections;

public class MiniGame7Controller : MiniGameController 
{
    private Player player;
    private MouseScrollInput mouseScrollInput;
    private ParryEnemy parryEnemy;

    public override void StartGame()
    {
        base.StartGame();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.ChangeAnimationState(Player.STATE_IDLE);

        parryEnemy = GetComponentInChildren<ParryEnemy>();
        parryEnemy.OnFinishedAttacking += End;
    }

    void End()
    {
        player.ChangeAnimationState(Player.STATE_IDLE);

        if (OnGameDone != null)
            OnGameDone(0f);

        Destroy(gameObject);
    }
}
