using UnityEngine;
using System.Collections;

public class MiniGame5Controller : MiniGameController 
{
    public float StartX = 3.21f;
    public float EndX = 12.2f;

    public float MoveSpeed = 10f;
    public float WaitDuration = 3f;

    public TextMesh TextName;
    public TextMesh TextText;

    [System.Serializable]
    public class Game5Question
    {
        public GameObject VillagerObject;

        public string Name;
        public string Text;
        public int Answer;

        public string ReactionRight = "RIGHT!";
        public string ReactionWrong = "YOU'RE WRONG";
        public string ReactionIgnore = "NOTICE MEEE";
    }
    public Game5Question[] Questions;

    private Player player;
    private MouseScrollInput mouseScrollInput;

    private int currentVillager = 0;
    private Game5Question currentQuestion;
    private IEnumerator currentWaiter;

    public override void StartGame()
    {
        base.StartGame();
        Debug.Log("Mini Game 5 Started");

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.ChangeAnimationState(Player.STATE_IDLE);

        mouseScrollInput = MouseScrollInput.Instance;
        mouseScrollInput.OnMouseIterate += MouseIterate;

        TextName = GameObject.Find("Text Name").GetComponent<TextMesh>();
        TextText = GameObject.Find("Text Story").GetComponent<TextMesh>();

        StartCoroutine(StartVillager());
    }

    IEnumerator StartVillager()
    {
        yield return null;

        currentQuestion = Questions[currentVillager];
        Transform villagerTransform = currentQuestion.VillagerObject.transform;
        while(villagerTransform.position.x > StartX)
        {
            villagerTransform.position = new Vector3(villagerTransform.position.x - (Time.deltaTime * MoveSpeed), villagerTransform.position.y, villagerTransform.position.z);
            yield return null;
        }

        villagerTransform.position = new Vector3(StartX, villagerTransform.position.y, villagerTransform.position.z);

        TextName.text = currentQuestion.Name;
        TextText.text = currentQuestion.Text;

        currentWaiter = WaitForAnswer();
        StartCoroutine(currentWaiter);
    }

    IEnumerator Next()
    {
        currentVillager++;
        if (currentVillager <= Questions.Length)
        {
            Transform villagerTransform = currentQuestion.VillagerObject.transform;
            while (villagerTransform.position.x < EndX)
            {
                villagerTransform.position = new Vector3(villagerTransform.position.x + (Time.deltaTime * MoveSpeed), villagerTransform.position.y, villagerTransform.position.z);
                yield return null;
            }

            villagerTransform.position = new Vector3(EndX, villagerTransform.position.y, villagerTransform.position.z);

            if (currentVillager == Questions.Length)
            {
                yield return new WaitForSeconds(1f);

                player.ChangeAnimationState(Player.STATE_IDLE);

                if (OnGameDone != null)
                    OnGameDone(0f);

                Destroy(gameObject);
            }
            else
                StartCoroutine(StartVillager());
        }
    }

    IEnumerator WaitForAnswer()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(WaitDuration);

        TextText.text = currentQuestion.ReactionIgnore;
        currentWaiter = null;

        StartCoroutine(Next());
    }

    void MouseIterate()
    {
        if (currentWaiter != null)
        {
            StopCoroutine(currentWaiter);
            currentWaiter = null;

            bool right = mouseScrollInput.GetDirection() == currentQuestion.Answer;
            Debug.Log(mouseScrollInput.GetDirection());

            if (mouseScrollInput.GetDirection() == -1)
            {
                player.ChangeAnimationState(Player.STATE_LAUGH);
                
                if (right)
                    TextText.text = currentQuestion.ReactionRight;
                else
                    TextText.text = currentQuestion.ReactionWrong;
            }
            else if (mouseScrollInput.GetDirection() == 1)
            {
                player.ChangeAnimationState(Player.STATE_SAD);

                if (right)
                    TextText.text = currentQuestion.ReactionRight;
                else
                    TextText.text = currentQuestion.ReactionWrong;
            }

            StartCoroutine(Next());
        }
    }
}
