using UnityEngine;

public class Board : MonoBehaviour
{
   [Header("Input Settings: ")]
   [SerializeField] private LayerMask boxesLayerMask;
   [SerializeField] private float touchRadius;

   [Header("Mark Sprites: ")]
   [SerializeField] private Sprite spriteX;
   [SerializeField] private Sprite spriteO;

   [Header("Win message: ")]
   [SerializeField] private GameObject winMessage;
   [SerializeField] private Sprite spriteWinO;
   [SerializeField] private Sprite spriteWinX;
   [SerializeField] private Sprite spriteStandoff;

   [Header("Sound effects: ")]
   [SerializeField] private AudioClip boxClick;
   private AudioSource audioSource;

   public Mark[] marks;
   private Camera cam;

   private Mark currentMark;
   private int allSteps = 9;

   private void Start()
   {

    audioSource = GetComponent<AudioSource>();
    cam = Camera.main;
    currentMark = Mark.X;
    marks = new Mark[9];
   }
   
   private void Update()
   {
      if(Input.GetMouseButtonUp(0))
      {
          Vector2 touchPosition = cam.ScreenToWorldPoint(Input.mousePosition);

          Collider2D hit = Physics2D.OverlapCircle(touchPosition, touchRadius, boxesLayerMask);

          if(hit)
          {
             HitBox(hit.GetComponent<Box>());
          }
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

        bool won = CheckIfWin();

        if(won)
        {
            Debug.Log(currentMark.ToString() + " wins!");
            if(currentMark.ToString() == "O")
            {
                winMessage.GetComponent<SpriteRenderer>().sprite = spriteWinO;
            }
            else if(currentMark.ToString() == "X")
            {
                winMessage.GetComponent<SpriteRenderer>().sprite = spriteWinX;
            }
            return;
        }
        else if(allSteps == 0)
        {
            winMessage.GetComponent<SpriteRenderer>().sprite = spriteStandoff;
        }

        SwitchPlayer();
    }
   }

   private bool CheckIfWin()
   {
    return
    AreBoxesMatched(0,1,2) || AreBoxesMatched(3,4,5) || AreBoxesMatched(6,7,8) ||
    AreBoxesMatched(0,3,6) || AreBoxesMatched(1,4,7) || AreBoxesMatched(2,5,8) ||
    AreBoxesMatched(0,4,8) || AreBoxesMatched(2,4,6);

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
}
