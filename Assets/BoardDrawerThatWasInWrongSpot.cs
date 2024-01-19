using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardDrawerThatWasInWrongSpot : MonoBehaviour
{
    public GameObject White_Square;
    public RectTransform White_Square_Transform;
    public GameObject Black_Square;
    public RectTransform Black_Square_Transform;
    public GameObject SquareParent;
    public GameObject Canvas;
    public RectTransform Canvas_Rect;
    public bool WhiteStarting = true;
    private bool SquareColor = false;
    public Vector2 BoardOffset;
    private float squareSize = 100;
    public float trueScale;
    public GameObject PieceParent;
    public GameObject Black_Bishop, Black_King, Black_Knight, Black_Pawn, Black_Queen, Black_Rook, White_Bishop, White_King, White_Knight, White_Pawn, White_Queen, White_Rook;
    public float pieceScale = 0.65f;
    public Vector3 ScaleOffSetForBlurIssue = new Vector3(0.01f, 0.01f, 0.01f);
    // Start is called before the first frame update
    void Start()
    {
        SquareColor = !WhiteStarting;
        if (Canvas_Rect.rect.width > Canvas_Rect.rect.height){
            squareSize = Canvas_Rect.rect.height*7/64f;
}
        else
        {
            squareSize = Canvas_Rect.rect.width * 7 / 64f;
        }
        trueScale = squareSize / 68.5f;
        Black_Bishop.transform.localScale = Black_King.transform.localScale = Black_Knight.transform.localScale = Black_Pawn.transform.localScale = Black_Queen.transform.localScale = Black_Rook.transform.localScale = White_Bishop.transform.localScale = White_King.transform.localScale = White_Knight.transform.localScale = White_Pawn.transform.localScale = White_Queen.transform.localScale = White_Rook.transform.localScale = new Vector3(pieceScale * trueScale, pieceScale * trueScale, pieceScale * trueScale);
        //White_Rook.transform.localScale = new Vector3(Black_Bishop.transform.localScale.x * trueScale * 1f, Black_Bishop.transform.localScale.y * trueScale * 1f, Black_Bishop.transform.localScale.z * trueScale * 1f);
        Debug.Log(Black_Bishop.transform.localScale);
        Black_Square.transform.localScale = new Vector3(squareSize / 68.5f, squareSize / 68.5f, squareSize / 68.5f) + ScaleOffSetForBlurIssue;
        White_Square.transform.localScale = new Vector3(squareSize / 68.5f, squareSize / 68.5f, squareSize / 68.5f)+ScaleOffSetForBlurIssue;
        BoardOffset = new Vector2(Canvas_Rect.rect.width/2-squareSize*3.5f, Canvas_Rect.rect.height/2-squareSize * 3.5f);
        //Debug.Log(68.5f+BoardOffset.x);
        Debug.Log(squareSize);
        for (int i=0; i<8; i++)
        {
            for (int o=0; o<8; o++)
            {
                if (SquareColor)
                {
                    Instantiate(White_Square, new Vector2(i*squareSize*1f, o * squareSize * 1f) + BoardOffset, Quaternion.identity, SquareParent.transform);
                }
                else
                {
                    Instantiate(Black_Square, new Vector2(i* squareSize*1f, o * squareSize * 1f) + BoardOffset, Quaternion.identity, SquareParent.transform);
                }
                SquareColor=!SquareColor;
            }
            SquareColor = !SquareColor;
        }
        if (WhiteStarting)
        {

            Instantiate(White_Rook, new Vector2(0, 0)+BoardOffset, Quaternion.identity, PieceParent.transform);
            Instantiate(White_Rook, new Vector2(7*squareSize, 0) + BoardOffset, Quaternion.identity, PieceParent.transform);

            Instantiate(White_Knight, new Vector2(squareSize, 0) + BoardOffset, Quaternion.identity, PieceParent.transform);
            Instantiate(White_Knight, new Vector2(6*squareSize, 0) + BoardOffset, Quaternion.identity, PieceParent.transform);

            Instantiate(White_Bishop, new Vector2(2 * squareSize, 0) + BoardOffset, Quaternion.identity, PieceParent.transform);
            Instantiate(White_Bishop, new Vector2(5 * squareSize, 0) + BoardOffset, Quaternion.identity, PieceParent.transform);

            Instantiate(White_Queen, new Vector2(3 * squareSize, 0) + BoardOffset, Quaternion.identity, PieceParent.transform);

            Instantiate(White_King, new Vector2(4 * squareSize, 0) + BoardOffset, Quaternion.identity, PieceParent.transform);

            for (int i = 0; i<8; i++)
            {
                Instantiate(White_Pawn, new Vector2(i * squareSize, squareSize) + BoardOffset, Quaternion.identity, PieceParent.transform);
            }

            Instantiate(Black_Rook, new Vector2(0, squareSize*7) + BoardOffset, Quaternion.identity, PieceParent.transform);
            Instantiate(Black_Rook, new Vector2(7 * squareSize, squareSize * 7) + BoardOffset, Quaternion.identity, PieceParent.transform);

            Instantiate(Black_Knight, new Vector2(squareSize, squareSize * 7) + BoardOffset, Quaternion.identity, PieceParent.transform);
            Instantiate(Black_Knight, new Vector2(6 * squareSize, squareSize * 7) + BoardOffset, Quaternion.identity, PieceParent.transform);

            Instantiate(Black_Bishop, new Vector2(2 * squareSize, squareSize * 7) + BoardOffset, Quaternion.identity, PieceParent.transform);
            Instantiate(Black_Bishop, new Vector2(5 * squareSize, squareSize * 7) + BoardOffset, Quaternion.identity, PieceParent.transform);

            Instantiate(Black_King, new Vector2(3 * squareSize, squareSize * 7) + BoardOffset, Quaternion.identity, PieceParent.transform);

            Instantiate(Black_Queen, new Vector2(4 * squareSize, squareSize * 7) + BoardOffset, Quaternion.identity, PieceParent.transform);

            for (int i = 0; i < 8; i++)
            {
                Instantiate(Black_Pawn, new Vector2(i * squareSize, squareSize * 6) + BoardOffset, Quaternion.identity, PieceParent.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
