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

    public int LineLength = 50;

    public string ReactionRight = "RIGHT!";
    public string ReactionWrong = "YOU'RE WRONG";
    public string ReactionIgnore = "NOTICE MEEE";

    [System.Serializable]
    public class Game5Question
    {
        public GameObject VillagerObject;

        public string Name;
        public string Text;
        public int Answer;
    }
    public Game5Question[] Questions;

    public AudioClip LaughClip;
    public AudioClip SadClip;

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
        TextText.text = ResolveTextSize(currentQuestion.Text, LineLength);

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

        TextText.text = ResolveTextSize(ReactionIgnore, LineLength);
        currentWaiter = null;

        StartCoroutine(Next());
    }

    void MouseIterate()
    {
        int direction = mouseScrollInput.GetDirection();
        Debug.Log(direction);

        if (currentWaiter != null && direction != 0)
        {
            StopCoroutine(currentWaiter);
            currentWaiter = null;

            bool right = direction == currentQuestion.Answer;
            if (direction == -1)
            {
                player.ChangeAnimationState(Player.STATE_LAUGH);
                AudioSource.PlayClipAtPoint(LaughClip, Vector3.zero);

                if (right)
                    TextText.text = ResolveTextSize(ReactionRight, LineLength);
                else
                    TextText.text = ResolveTextSize(ReactionWrong, LineLength);
            }
            else if (direction == 1)
            {
                player.ChangeAnimationState(Player.STATE_SAD);
                AudioSource.PlayClipAtPoint(SadClip, Vector3.zero);

                if (right)
                    TextText.text = ResolveTextSize(ReactionRight, LineLength);
                else
                    TextText.text = ResolveTextSize(ReactionWrong, LineLength);
            }

            StartCoroutine(Next());
        }
    }

    // Wrap text by line height
    private string ResolveTextSize(string input, int lineLength)
    {
        // Split string by char " "    
        string[] words = input.Split(" "[0]);

        // Prepare result
        string result = "";

        // Temp line string
        string line = "";

        // for each all words     
        foreach (string s in words)
        {
            // Append current word into line
            string temp = line + " " + s;

            // If line length is bigger than lineLength
            if (temp.Length > lineLength)
            {

                // Append current line into result
                result += line + "\n";
                // Remain word append into new line
                line = s;
            }
            // Append current word into current line
            else
            {
                line = temp;
            }
        }

        // Append last line into result   
        result += line;

        // Remove first " " char
        return result.Substring(1, result.Length - 1);
    }
}
