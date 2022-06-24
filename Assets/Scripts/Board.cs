using UnityEngine;

public class Board : MonoBehaviour
{
   [Header("Input Settings: ")]
   [SerializeField] private LayerMask boxesLayerMask;
   [SerializeField] private float touchRadius;

   [Header("Mark Sprites: ")]
   [SerializeField] private Sprite spriteX;
   [SerializeField] private Sprite spriteO;

   public Mark[] marks;
   private Camera cam;

   private Mark currentMark;

   private void Start()
   {
    cam = Camera.main;
    currentMark = Mark.X;
    marks = new Mark[25];
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
    if(!box.isMarked)
    {
        marks [box.index] = currentMark;

        box.SetAsMarked(GetSprite(), currentMark); 

        SwitchPlayer();
    }
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
