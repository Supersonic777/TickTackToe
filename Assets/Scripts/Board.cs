using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Input Settings: ")]
    public bool isSingleplayer;
    public GameObject replayButton;
    public GameObject enemyStepMessage;
    public GameObject board;
    public GameObject[] boxes;
    public Mark[] marks;
    private Camera cam;

    [Header("Input Settings: ")]
    [SerializeField] private LayerMask boxesLayerMask;   
    [SerializeField] private float touchRadius;

    [Header("Mark Sprites: ")]
    [SerializeField] private Sprite spriteX;
    [SerializeField] private Sprite spriteO;
    [SerializeField] private GameObject stepIndicator;
    [SerializeField] private Sprite xStepMessage;
    [SerializeField] private Sprite oStepMessage;

    [Header("Win message: ")]
    [SerializeField] private GameObject winMessage;
    [SerializeField] private Sprite spriteWinO;
    [SerializeField] private Sprite spriteWinX;
    [SerializeField] private Sprite spriteStandoff;

    [Header("Sound effects: ")]
    [SerializeField] private AudioClip boxClick;

    private bool enemyCanStep = false;
    private AudioSource audioSource;
    private Mark currentMark;
    private int allSteps = 9;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cam = Camera.main;
        currentMark = Mark.X;
        marks = new Mark[9];

        if(!isSingleplayer)
        {
            stepIndicator.GetComponent<SpriteRenderer>().sprite = xStepMessage;
        }
    }
   
    private void Update()
    {
        if((Input.GetMouseButtonUp(0) && currentMark == Mark.X) || (Input.GetMouseButtonUp(0) && !isSingleplayer))
        {
            Vector2 touchPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapCircle(touchPosition, touchRadius, boxesLayerMask);

            if(hit && !CheckIfWin3x3())
            {
                HitBox(hit.GetComponent<Box>());
            }
        }
        else if(isSingleplayer && enemyCanStep && !CheckIfWin3x3())
        {
            if(allSteps!=0 )
            {   
                enemyStepMessage.SetActive(true);
            }

            Invoke("EnemyStep", Random.Range(1,2));
            enemyCanStep = false;
        }
    }

    private void HitBox(Box box)
    {
        allSteps -=1;
        Debug.Log(allSteps);
        audioSource.PlayOneShot(boxClick);

        if(!box.isMarked)
        {
            marks [box.index] = currentMark;
            box.SetAsMarked(GetSprite(), currentMark); 
            bool won = CheckIfWin3x3();
            enemyCanStep = true;

            if(won)
            {
                Invoke("GameFinish", 2.0f);
                if(!isSingleplayer)
                {
                    stepIndicator.GetComponent<SpriteRenderer>().sprite = null;
                }

                return;
            }
            else if(allSteps == 0)
            {
                if(!isSingleplayer)
                {
                    stepIndicator.GetComponent<SpriteRenderer>().sprite = null;
                }
                Invoke("GameFinish", 2.0f);

                return;
            }

            PrintStepMark();

            SwitchPlayer();
        }
    }

    private bool CheckIfWin3x3()
    {
        return
        AreBoxesMatched(0,1,2) || AreBoxesMatched(3,4,5) || AreBoxesMatched(6,7,8) ||
        AreBoxesMatched(0,3,6) || AreBoxesMatched(1,4,7) || AreBoxesMatched(2,5,8) ||
        AreBoxesMatched(0,4,8) || AreBoxesMatched(2,4,6);
    }

    private bool CheckIfWin4x4()
    {
        return
        AreBoxesMatched(0,1,2) || AreBoxesMatched(1,2,3) || AreBoxesMatched(4,5,6) || AreBoxesMatched(5,6,7) ||
        AreBoxesMatched(8,9,10) || AreBoxesMatched(9,10,11) || AreBoxesMatched(12,13,14) || AreBoxesMatched(13,14,15) ||
        AreBoxesMatched(0,4,8) || AreBoxesMatched(4,8,12) || AreBoxesMatched(1,5,9) || AreBoxesMatched(5,9,13) ||
        AreBoxesMatched(2,6,10) || AreBoxesMatched(6,10,14) || AreBoxesMatched(3,7,11) || AreBoxesMatched(7,11,15) ||
        AreBoxesMatched(2,5,8) || AreBoxesMatched(3,6,9) || AreBoxesMatched(6,9,12) || AreBoxesMatched(7,10,13) ||
        AreBoxesMatched(1,6,11) || AreBoxesMatched(0,5,10) || AreBoxesMatched(5,10,15) || AreBoxesMatched(4,9,14);
    }



    private bool AreBoxesMatched(int i, int j, int k)
    {
        Mark m = currentMark;
        bool matched = marks[i]==m && marks[j]==m && marks[k]==m;

        return matched; 
    }

    private void SwitchPlayer()
    {
        currentMark = (currentMark == Mark.X) ? Mark.O : Mark.X;
    }

    private Sprite GetSprite()
    {
        return (currentMark == Mark.X) ? spriteX : spriteO;
    }

    private void GameFinish()
    {
        Debug.Log(currentMark.ToString() + " wins!");

        if(CheckIfWin3x3())
        {
            if(currentMark.ToString() == "O")
            {
                winMessage.GetComponent<SpriteRenderer>().sprite = spriteWinO;
            }
            else if(currentMark.ToString() == "X")
            {
                winMessage.GetComponent<SpriteRenderer>().sprite = spriteWinX;
            }
        }
        else
        {
            winMessage.GetComponent<SpriteRenderer>().sprite = spriteStandoff;
        }

        board.SetActive(false);
        replayButton.SetActive(true);
    }

    private void EnemyStep()
    {
        int randomNumber = Random.Range(0, marks.Length-1);

        if(marks[randomNumber] == Mark.None)
        {
            allSteps -=1;
            Debug.Log(allSteps);
            audioSource.PlayOneShot(boxClick);

            if(isSingleplayer)
            {
                enemyStepMessage.SetActive(false);
            }
        
            marks [boxes[randomNumber].GetComponent<Box>().index] = currentMark;
            boxes[randomNumber].GetComponent<Box>().SetAsMarked(GetSprite(), currentMark); 
            bool won = CheckIfWin3x3();

            if(won)
            {
                Invoke("GameFinish", 2.0f);

                return;
            }
            else if(allSteps == 0)
            {
                winMessage.GetComponent<SpriteRenderer>().sprite = spriteStandoff;
                Invoke("GameFinish", 2.0f);

                return;
            }

            SwitchPlayer();

            return;
        }
        else
        {
            EnemyStep();
        }
    }

    private void PrintStepMark()
    {
        if(!isSingleplayer)
        {
            if(currentMark == Mark.X)
            {
                stepIndicator.GetComponent<SpriteRenderer>().sprite = oStepMessage;
            }
            else if(currentMark == Mark.O)
            {
                stepIndicator.GetComponent<SpriteRenderer>().sprite = xStepMessage;
            }
            else if(CheckIfWin3x3())
            {
                stepIndicator.GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }

    
}
