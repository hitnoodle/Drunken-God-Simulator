using UnityEngine;
using System.Collections;

public class MiniGame7Controller : MiniGameController 
{
    public float SpawnRate = 5f; // In seconds
    public int HowManyEnemies = 5;
    public GameObject EnemyPrefab;

    private MouseScrollInput mouseScrollInput;

    private int ParryDirection;

    public override void StartGame()
    {
        base.StartGame();

        Player.ChangeAnimationState(Player.STATE_PARRY_IDLE);

        mouseScrollInput = MouseScrollInput.Instance;
        mouseScrollInput.OnMouseIterate += MouseIterate;

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (HowManyEnemies > 0)
        {
            HowManyEnemies--;

            ParryEnemy enemy = (Instantiate(EnemyPrefab) as GameObject).GetComponent<ParryEnemy>();

            yield return new WaitForSeconds(0.1f);

            int type = Random.Range(0, 2);
            enemy.Attack(Player, type);

            yield return new WaitForSeconds(SpawnRate);
        }

        End();
    }

    void End()
    {
        mouseScrollInput.OnMouseIterate -= MouseIterate;

        Player.ChangeAnimationState(Player.STATE_IDLE);

        if (OnGameDone != null)
            OnGameDone(0f);

        Started = false;
        //Destroy(gameObject);
    }

    void MouseIterate()
    {
        if (mouseScrollInput.GetDirection() == -1)
        {
            Player.ChangeAnimationState(Player.STATE_ATTACK_DOWN);
        }
        else if (mouseScrollInput.GetDirection() == 1)
        {
            Player.ChangeAnimationState(Player.STATE_ATTACK_UP);
        }
    }
}
