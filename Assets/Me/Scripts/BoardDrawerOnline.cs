/* CHECK LINES 159, FOR SOME REASON THE MOVE FUNCTION IS CHANGING THE VALUE OF PIECES[], WHEN I'M TRYING TO CHANGE THE VALUE OF TEMPPIECES[] TO LATER SET PIECES[] EQUAL TO TEMPPIECES[] IF THE KING ISN'T IN CHECK*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BoardDrawerOnline : MonoBehaviour
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
    public bool WhiteTurn = true;
    public Vector2 BoardOffset;
    public float squareSize = 100;
    public float trueScale;
    public GameObject PieceParent;
    public GameObject Black_Bishop, Black_King, Black_Knight, Black_Pawn, Black_Queen, Black_Rook, White_Bishop, White_King, White_Knight, White_Pawn, White_Queen, White_Rook;
    public float pieceScale = 0.65f;
    public Vector3 ScaleOffsetForBlurIssue = new Vector3(0.01f, 0.01f, 0.01f);
    public GameObject Yellow_Highlight;
    public GameObject Orange_Highlight;
    public GameObject Red_Highlight;
    public GameObject UIParent;
    public Vector2 highLightedSpace = new Vector2(8, 8);
    public GameObject tempHighlight;
    public ChessPiece[,] pieces = new ChessPiece[8, 8];
    public Vector2 lastDisplayed = new Vector2(8, 8);
    public Vector2 EnPassant = new Vector2(8, 8);
    public ChessPiece EnPassantPiece;
    public GameObject WhitePawnPromo;
    public GameObject BlackPawnPromo;
    public Vector2 PawnPromo = new Vector2(8, 8);
    public bool Freeze = false;
    public GameObject SDE;
    public bool[,] checks;
    public Vector2 movedPiece;
    public GameObject Null_Prefab;
    public GameObject KingHighlight;
    public GameObject WhiteWin;
    public GameObject BlackWin;
    public GameObject Tie;
    public bool mateNotChecked = true;
    public string toBeLoaded;
    public GameObject PauseMenu;
    public GameObject Pause;
    public SendPieceInfo myScriptObject;
    public bool startFreeze = true;
    public string GlobalColor;
    // Start is called before the first frame update
    void Start()
    {
        //myScriptObject = GameObject.Find("PlayerObject(Clone)").GetComponent<SendPieceInfo>();
        //Debug.Log(pieces[7,7]);
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
        //Debug.Log(Black_Bishop.transform.localScale);
        Black_Square.transform.localScale = new Vector3(squareSize /68.5f, squareSize / 68.5f, squareSize / 68.5f)+ScaleOffsetForBlurIssue;
        White_Square.transform.localScale = new Vector3(squareSize / 68.5f, squareSize / 68.5f, squareSize / 68.5f)+ScaleOffsetForBlurIssue;
        Yellow_Highlight.transform.localScale = new Vector3(squareSize / 68.5f, squareSize / 68.5f, squareSize / 68.5f);
        Orange_Highlight.transform.localScale = new Vector3(squareSize / 68.5f * 0.5f, squareSize / 68.5f * 0.5f, squareSize / 68.5f * 0.5f);
        Red_Highlight.transform.localScale = new Vector3(squareSize / 68.5f, squareSize / 68.5f, squareSize / 68.5f);
        KingHighlight.transform.localScale = new Vector3(squareSize / 68.5f, squareSize / 68.5f, squareSize / 68.5f);
        WhitePawnPromo.transform.localScale = new Vector3(squareSize / 86.8f, squareSize / 86.8f, squareSize / 86.8f);
        BlackPawnPromo.transform.localScale = new Vector3(squareSize / 86.8f, squareSize / 86.8f, squareSize / 86.8f);
        WhiteWin.transform.localScale = new Vector3(squareSize / 86.8f, squareSize / 86.8f, squareSize / 86.8f);
        BlackWin.transform.localScale = new Vector3(squareSize / 86.8f, squareSize / 86.8f, squareSize / 86.8f);
        Tie.transform.localScale = new Vector3(squareSize / 86.8f, squareSize / 86.8f, squareSize / 86.8f);
        Pause.transform.localScale = new Vector3(squareSize / 86.8f, squareSize / 86.8f, squareSize / 86.8f);
        PauseMenu.transform.localScale = new Vector3(squareSize / 86.8f, squareSize / 86.8f, squareSize / 86.8f);
        BoardOffset = new Vector2(Canvas_Rect.rect.width/2-squareSize*3.5f, Canvas_Rect.rect.height/2-squareSize * 3.5f);
        //Debug.Log(68.5f+BoardOffset.x);
        //Debug.Log(squareSize);
        DrawBoard();

        spawnPieces();
        WhitePawnPromo.SetActive(false);
        BlackPawnPromo.SetActive(false);
        if (WhiteTurn)
        {
            checks = generateChecks("Black", pieces, Red_Highlight, SDE, squareSize, BoardOffset);
        }
        else
        {
            checks = generateChecks("White", pieces, Red_Highlight, SDE, squareSize, BoardOffset);
        }
        Debug.Log(Screen.height);
        int PausePosX = Screen.width / 2 * -1;
        float swr, shr;
        if (Screen.width > 1330)
        {
            swr = Screen.width / 1330;
        }
        else
        {
            swr = 1;
        }
        if (Screen.height > 804)
        {
            shr = Screen.height / 804;
        }
        else
        {
            shr = 1;
        }
        Pause.transform.position = new Vector2(75*(squareSize / 86.8f)*(swr), Screen.height-75 * (squareSize / 86.8f)*(shr));
        Debug.Log(Pause.transform.position);
        Freeze = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (myScriptObject != null && startFreeze)
        {
            Freeze = false;
            startFreeze = false;
            if (myScriptObject.CheckIfServer())
            {
                GlobalColor = "White";
            }
            else
            {
                GlobalColor = "Black";
            }
        }
        if (WhiteTurn)
        {
            checks = generateChecks("Black", pieces, Red_Highlight, SDE, squareSize, BoardOffset);
        }
        else
        {
            checks = generateChecks("White", pieces, Red_Highlight, SDE, squareSize, BoardOffset);
        }
        //bool mouseWasDown = false;
        //if (Input.GetMouseButtonDown(0))
        //{
        //    mouseWasDown = true;
        //    Debug.Log("UP");
        //}
        if (!Freeze)
        {
            if (mateNotChecked)
            {
                //Debug.Log("NOW CHECKING MATE FOR WHITE?" + WhiteTurn);
                if (WhiteTurn)
                {
                    if (CheckForMate(pieces, "White"))
                    {
                        Freeze= true;
                        BlackWin.SetActive(true);
                    }
                    if (CheckForDraw(pieces, "White"))
                    {
                        Debug.Log("WHITE DRAW!!!");
                        Freeze = true;
                        Tie.SetActive(true);
                    }
                    if (CheckForDraw(pieces, "Black"))
                    {
                        Debug.Log("BLACK DRAW!!!");
                        Freeze = true;
                        Tie.SetActive(true);
                    }
                    //Debug.Log("Draw(B): " + CheckForDraw(pieces, "White"));
                }
                else
                {
                    if (CheckForMate(pieces, "Black"))
                    {
                        Debug.Log("BLACK MATE!!!");
                        Freeze = true;
                        WhiteWin.SetActive(true);
                    }
                    if (CheckForDraw(pieces, "Black"))
                    {
                        Debug.Log("BLACK DRAW!!!");
                        Freeze = true;
                        Tie.SetActive(true);
                    }
                    if (CheckForDraw(pieces, "White"))
                    {
                        Debug.Log("WHITE DRAW!!!");
                        Freeze = true;
                        Tie.SetActive(true);
                    }
                    //Debug.Log("Draw(W): " + CheckForDraw(pieces, "White"));
                }
                mateNotChecked = false;
            }
            bool highlightSuccess = false;
            if (Input.GetMouseButtonUp(0) && ((WhiteTurn && GlobalColor == "White") || (!WhiteTurn && GlobalColor == "Black")))
            {
                //Debug.LogError(myScriptObject.CheckIfServer());
                Vector2 mousePos = Input.mousePosition;
                for (int i = 0; i < 8; i++)
                {
                    for (int o = 0; o < 8; o++)
                    {
                        if (mousePos.x > i * squareSize - squareSize / 2 + BoardOffset.x && mousePos.x < i * squareSize + squareSize / 2 + BoardOffset.x && mousePos.y > o * squareSize - squareSize / 2 + BoardOffset.y && mousePos.y < o * squareSize + squareSize / 2 + BoardOffset.y && highLightedSpace != new Vector2(i, o) && pieces[i, o] != null && ((WhiteTurn && pieces[i, o].color == "White")||(!WhiteTurn && pieces[i, o].color == "Black")))
                        {
                            movedPiece = new Vector2(i, o);
                            //Debug.Log(i + "" + o);
                            highLightedSpace = new Vector2(i, o);
                            if (tempHighlight != null)
                            {
                                Destroy(tempHighlight);
                            }
                            tempHighlight = Instantiate(Yellow_Highlight, new Vector2(i * squareSize, o * squareSize) + BoardOffset, Quaternion.identity, UIParent.transform);
                            highlightSuccess = true;
                            //Debug.Log(pieces[(int)highLightedSpace.x, (int)highLightedSpace.y].thisObject);
                            if (lastDisplayed != new Vector2(8, 8))
                            {
                                pieces[(int)lastDisplayed.x, (int)lastDisplayed.y].DestroyMoves();
                                lastDisplayed = new Vector2(8, 8);
                            }
                            //Debug.Log(pieces[(int)highLightedSpace.x, (int)highLightedSpace.y].name);
                            pieces[(int)highLightedSpace.x, (int)highLightedSpace.y].DisplayMoves(Orange_Highlight, Red_Highlight, UIParent, pieces, EnPassant, checks);
                            //Debug.Log("MOVESDIPLAYED!");
                            lastDisplayed = highLightedSpace;
                        }
                        if (mousePos.x > i * squareSize - squareSize / 2 + BoardOffset.x && mousePos.x < i * squareSize + squareSize / 2 + BoardOffset.x && mousePos.y > o * squareSize - squareSize / 2 + BoardOffset.y && mousePos.y < o * squareSize + squareSize / 2 + BoardOffset.y && highLightedSpace != new Vector2(i, o) && pieces[i, o] != null && !WhiteTurn && pieces[i, o].color == "Black")
                        {
                            //Debug.Log(i + "" + o);
                            highLightedSpace = new Vector2(i, o);
                            if (tempHighlight != null)
                            {
                                Destroy(tempHighlight);
                            }
                            tempHighlight = Instantiate(Yellow_Highlight, new Vector2(i * squareSize, o * squareSize) + BoardOffset, Quaternion.identity, UIParent.transform);
                            highlightSuccess = true;
                            //Debug.Log(pieces[(int)highLightedSpace.x, (int)highLightedSpace.y].thisObject);
                            if (lastDisplayed != new Vector2(8, 8))
                            {
                                pieces[(int)lastDisplayed.x, (int)lastDisplayed.y].DestroyMoves();
                                lastDisplayed = new Vector2(8, 8);
                            }
                            pieces[(int)highLightedSpace.x, (int)highLightedSpace.y].DisplayMoves(Orange_Highlight, Red_Highlight, UIParent, pieces, EnPassant, checks);
                            lastDisplayed = highLightedSpace;
                        }
                    }
                }
                if (!highlightSuccess)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int o = 0; o < 8; o++)
                        {
                            if (mousePos.x > i * squareSize - squareSize / 2 + BoardOffset.x && mousePos.x < i * squareSize + squareSize / 2 + BoardOffset.x && mousePos.y > o * squareSize - squareSize / 2 + BoardOffset.y && mousePos.y < o * squareSize + squareSize / 2 + BoardOffset.y && highLightedSpace != new Vector2(i, o))
                            {
                                //Debug.Log("FLAG!");
                                for (int p = 0; p < 23; p++)
                                {
                                    //Debug.Log(pieces[(int)highLightedSpace.x, (int)highLightedSpace.y].shownMoves[p]+" "+ new Vector2(i, o));
                                    if (highLightedSpace.x != 8 && pieces[(int)highLightedSpace.x, (int)highLightedSpace.y] != null && pieces[(int)highLightedSpace.x, (int)highLightedSpace.y].shownMoves[p] == new Vector2(i, o))
                                    {
                                        //Debug.Log("WOOOHOOOOOOO!");
                                        ChessPiece[,] tempPieces = new ChessPiece[8, 8];
                                        for (int a = 0; a < 8; a++)
                                        {
                                            for (int s = 0; s < 8; s++)
                                            {
                                                if (pieces[a, s] != null)
                                                {
                                                    tempPieces[a, s] = new ChessPiece(pieces[a, s].name, Instantiate(Null_Prefab, PieceParent.transform), pieces[a, s].pos, pieces[a, s].color, squareSize, BoardOffset);
                                                }
                                            }
                                        }
                                        //Debug.Log("0 " + (pieces[(int)movedPiece.x, (int)movedPiece.y] != null));
                                        ArrayList arrayList = pieces[(int)highLightedSpace.x, (int)highLightedSpace.y].Move(new Vector2(i, o), tempPieces, EnPassant, EnPassantPiece, movedPiece, pieces, Red_Highlight, UIParent, squareSize, BoardOffset, WhiteTurn);
                                        mateNotChecked = true;
                                        if ((bool)arrayList[0])
                                        {
                                            //Debug.Log("1 " + (pieces[(int)movedPiece.x, (int)movedPiece.y] != null));
                                            //Debug.Log(arrayList[0]);
                                            tempPieces = (ChessPiece[,])arrayList[1];
                                            Vector2 newMovedPiece = (Vector2)arrayList[5];
                                            //Vector2 movedPiece = highLightedSpace;
                                            //if ((WhiteTurn && !checks[(int)FindKing("White", tempPieces).x, (int)FindKing("White", tempPieces).y]) || (!WhiteTurn && !checks[(int)FindKing("Black", tempPieces).x, (int)FindKing("Black", tempPieces).y]))
                                            //{
                                            //    for (int a = 0; a < 8; a++)
                                            //    {
                                            //        for (int s = 0; s < 8; s++)
                                            //        {
                                            //            if (tempPieces[a, s] != null)
                                            //            {
                                            //                pieces[a, s].pos = tempPieces[a, s].pos;
                                            //                pieces[a, s].firstMove = tempPieces[a, s].firstMove;
                                            //            }
                                            //            else
                                            //            {
                                            //                pieces[a, s] = null;
                                            //            }
                                            //        }
                                            //    }
                                            //Debug.Log(movedPiece);
                                            //Debug.Log(newMovedPiece);
                                            //pieces[(int)movedPiece.x, (int)movedPiece.y].thisObject.transform.position = new Vector2(tempPieces[(int)movedPiece.x, (int)newMovedPiece.y].pos.x * squareSize, tempPieces[(int)newMovedPiece.x, (int)newMovedPiece.y].pos.y * squareSize) + BoardOffset;
                                            EnPassant = (Vector2)arrayList[2];
                                            EnPassantPiece = (ChessPiece)arrayList[3];
                                            PawnPromo = (Vector2)arrayList[4];
                                            Vector2 newPos = (Vector2)arrayList[6];
                                            Vector2 oldPos = (Vector2)arrayList[7];
                                            if (PawnPromo != new Vector2(8, 8))
                                            {
                                                if (pieces[(int)PawnPromo.x, (int)PawnPromo.y].color == "White")
                                                {
                                                    WhitePawnPromo.SetActive(true);
                                                }
                                                else
                                                {
                                                    BlackPawnPromo.SetActive(true);
                                                }
                                                Freeze = true;
                                            }
                                            lastDisplayed = new Vector2(8, 8);
                                            //Debug.Log("OOF" + EnPassant);
                                            //Debug.Log(EnPassant);
                                            pieces = tempPieces;
                                            myScriptObject.ChangeValue(oldPos, newPos);
                                            WhiteTurn = !WhiteTurn;
                                        }
                                        else
                                        {
                                            if (WhiteTurn)
                                            {
                                                FlashKing("White", pieces, KingHighlight, squareSize, BoardOffset, UIParent);
                                            }
                                            else
                                            {
                                                FlashKing("Black", pieces, KingHighlight, squareSize, BoardOffset, UIParent);
                                            }
                                        }
                                        //}
                                        //else
                                        //{
                                        //    Debug.Log("2 " + (pieces[(int)movedPiece.x, (int)movedPiece.y] != null));
                                        //    Debug.Log("NMP" + newMovedPiece);
                                        //    Debug.Log(pieces[(int)newMovedPiece.x, (int)newMovedPiece.y].thisObject.transform.position);
                                        //    Debug.Log(pieces[(int)movedPiece.x, (int)movedPiece.y]);
                                        //    Debug.Log("3 "+(pieces[(int)movedPiece.x, (int)movedPiece.y] != null));
                                        //    pieces[(int)movedPiece.x, (int)movedPiece.y].thisObject.transform.position = new Vector2(pieces[(int)movedPiece.x, (int)movedPiece.y].pos.x * squareSize, pieces[(int)movedPiece.x, (int)movedPiece.y].pos.y * squareSize) + BoardOffset;
                                        //    lastDisplayed = new Vector2(8, 8);
                                        //    Debug.Log("NUH UH");
                                        //}
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    Destroy(tempHighlight);
                    highLightedSpace = new Vector2(8, 8);

                    if (lastDisplayed != new Vector2(8, 8))
                    {
                        pieces[(int)lastDisplayed.x, (int)lastDisplayed.y].DestroyMoves();
                        //lastDisplayed = new Vector2(8, 8);
                    }
                }
            }
        }
    }
    public void FlashKing(string color, ChessPiece[,] pieces, GameObject KingHighlight, float squareSize, Vector2 BoardOffset, GameObject UIHighlight)
    {
        Vector2 kingPos = FindKing(color, pieces);
        GameObject kh = Instantiate(KingHighlight, new Vector2(kingPos.x * squareSize, kingPos.y * squareSize)+BoardOffset, Quaternion.identity, UIHighlight.transform);
        Destroy(kh, 1);
    }
    public void ChangeName(string name)
    {
        pieces[(int)PawnPromo.x, (int)PawnPromo.y].PieceChangeName(name, PieceParent, White_Knight, White_Bishop, White_Rook, White_Queen, Black_Knight, Black_Bishop, Black_Rook, Black_Queen);
        WhitePawnPromo.SetActive(false);
        BlackPawnPromo.SetActive(false);
        Freeze = false;
    }
    public void DrawBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int o = 0; o < 8; o++)
            {
                if (SquareColor)
                {
                    Instantiate(White_Square, new Vector2(i * squareSize * 1f, o * squareSize * 1f) + BoardOffset, Quaternion.identity, SquareParent.transform);
                }
                else
                {
                    Instantiate(Black_Square, new Vector2(i * squareSize * 1f, o * squareSize * 1f) + BoardOffset, Quaternion.identity, SquareParent.transform);
                }
                SquareColor = !SquareColor;
            }
            SquareColor = !SquareColor;
        }
    }
    void spawnPieces()
    {
        if (WhiteStarting)
        {
            pieces[0, 0] = new ChessPiece("Rook", Instantiate(White_Rook, PieceParent.transform), new Vector2(0, 0), "White", squareSize, BoardOffset);
            pieces[7, 0] = new ChessPiece("Rook", Instantiate(White_Rook, PieceParent.transform), new Vector2(7, 0), "White", squareSize, BoardOffset);

            pieces[1, 0] = new ChessPiece("Knight", Instantiate(White_Knight, PieceParent.transform), new Vector2(1, 0), "White", squareSize, BoardOffset);
            pieces[6, 0] = new ChessPiece("Knight", Instantiate(White_Knight, PieceParent.transform), new Vector2(6, 0), "White", squareSize, BoardOffset);

            pieces[2, 0] = new ChessPiece("Bishop", Instantiate(White_Bishop, PieceParent.transform), new Vector2(2, 0), "White", squareSize, BoardOffset);
            pieces[5, 0] = new ChessPiece("Bishop", Instantiate(White_Bishop, PieceParent.transform), new Vector2(5, 0), "White", squareSize, BoardOffset);

            pieces[3, 0] = new ChessPiece("Queen", Instantiate(White_Queen, PieceParent.transform), new Vector2(3, 0), "White", squareSize, BoardOffset);

            pieces[4, 0] = new ChessPiece("King", Instantiate(White_King, PieceParent.transform), new Vector2(4,0), "White", squareSize, BoardOffset);

            for (int i = 0; i < 8; i++)
            {
                pieces[i, 1] = new ChessPiece("Pawn", Instantiate(White_Pawn, PieceParent.transform), new Vector2(i, 1), "White", squareSize, BoardOffset);
            }

            pieces[0, 7] = new ChessPiece("Rook", Instantiate(Black_Rook, PieceParent.transform), new Vector2(0, 7), "Black", squareSize, BoardOffset);
            pieces[7, 7] = new ChessPiece("Rook", Instantiate(Black_Rook, PieceParent.transform), new Vector2(7, 7), "Black", squareSize, BoardOffset);

            pieces[1, 7] = new ChessPiece("Knight", Instantiate(Black_Knight, PieceParent.transform), new Vector2(1, 7), "Black", squareSize, BoardOffset);
            pieces[6, 7] = new ChessPiece("Knight", Instantiate(Black_Knight, PieceParent.transform), new Vector2(6, 7), "Black", squareSize, BoardOffset);

            pieces[2, 7] = new ChessPiece("Bishop", Instantiate(Black_Bishop, PieceParent.transform), new Vector2(2, 7), "Black", squareSize, BoardOffset);
            pieces[5, 7] = new ChessPiece("Bishop", Instantiate(Black_Bishop, PieceParent.transform), new Vector2(5, 7), "Black", squareSize, BoardOffset);

            pieces[3, 7] = new ChessPiece("Queen", Instantiate(Black_Queen, PieceParent.transform), new Vector2(3, 7), "Black", squareSize, BoardOffset);

            pieces[4, 7] = new ChessPiece("King", Instantiate(Black_King, PieceParent.transform), new Vector2(4, 7), "Black", squareSize, BoardOffset);

            for (int i = 0; i < 8; i++)
            {
                pieces[i, 6] = new ChessPiece("Pawn", Instantiate(Black_Pawn, PieceParent.transform), new Vector2(i, 6), "Black", squareSize, BoardOffset);
            }
        }
        //Debug.Log(pieces[1,0].thisObject);
    }

    public static bool[,] generateChecks(string color, ChessPiece[,] pieces, GameObject Red_Highlight, GameObject UIHighlight, float squareSize, Vector2 BoardOffset)
    {
        bool[,] checks = new bool[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int o = 0; o < 8; o++)
            {
                checks[i, o] = false;
            }
        }
        for (int i = 0; i < 8; i++)
        {
            for (int o = 0; o < 8; o++)
            {
                if (pieces[i,o]!=null&&pieces[i, o].color == color)
                {
                    bool[,] tempChecks = pieces[i, o].generatePieceChecks(pieces);
                    for (int s = 0; s < 8; s++)
                    {
                        for (int d = 0; d < 8; d++)
                        {
                            if (checks[s, d] == false && tempChecks[s, d] == true)
                            {
                                checks[s, d] = true;
                            }
                        }
                    }
                }
            }
        }
        //for (int i = 0; i < 8; i++)
        //{
        //    for (int o = 0; o < 8; o++)
        //    {
        //        if (checks[i, o])
        //        {
        //            Instantiate(Red_Highlight, new Vector2(i * squareSize, o * squareSize) + BoardOffset, Quaternion.identity, UIHighlight.transform);
        //        }
        //    }
        //}
        return checks;
    }
    public static Vector2 FindKing(string color, ChessPiece[,] pieces)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int o = 0; o< 8; o++)
            {
                if (pieces[i,o]!=null&&pieces[i,o].name == "King" && pieces[i,o].color == color)
                {
                    return new Vector2(i, o);
                }
            }
        }
        return new Vector2(8,8);
    }
    public bool CheckForMate(ChessPiece[,] pieces, string color)
    {
        if (color == "White")
        {
            bool[,] checks = generateChecks("Black", pieces, Red_Highlight, UIParent, squareSize, BoardOffset);
        }
        else
        {
            bool[,] checks = generateChecks("White", pieces, Red_Highlight, UIParent, squareSize, BoardOffset);
        }
        //Debug.Log(checks[(int)FindKing(color, pieces).x, (int)FindKing(color, pieces).y]);
        if (!checks[(int)FindKing(color, pieces).x, (int)FindKing(color, pieces).y])
        {
            return false;
        }
        for (int i = 0; i < 8; i++)
        {
            for (int o = 0; o < 8; o++)
            {
                if (pieces[i, o] != null && pieces[i,o].color == color)
                {
                    //Debug.Log("in2");

                    ChessPiece[,] tempPieces = new ChessPiece[8, 8];
                    for (int a = 0; a < 8; a++)
                    {
                        for (int s = 0; s < 8; s++)
                        {
                            if (pieces[a, s] != null)
                            {
                                tempPieces[a, s] = new ChessPiece(pieces[a, s].name, Instantiate(Null_Prefab, PieceParent.transform), pieces[a, s].pos, pieces[a, s].color, squareSize, BoardOffset);
                            }
                        }
                    }
                    //if (tempPieces[i, o].name == "King")
                    //{
                    //    Debug.Log("KingTurn");
                    //}
                    //Debug.Log("i" + tempPieces[i, o]);
                    Vector2[] possibleMoves = pieces[i, o].DisplayMovesJustArray(Orange_Highlight, Red_Highlight, UIParent, pieces, EnPassant, checks);
                    Vector2[] actualPossibleMoves = new Vector2[23];
                    for (int a = 0; a < 23; a++)
                    {
                        actualPossibleMoves[a] = new Vector2(possibleMoves[a].x, possibleMoves[a].y);
                    }
                    pieces[i, o].DestroyMoves();
                    //Debug.Log("f" + pieces[i, o]);
                    //Debug.Log("f" + tempPieces[i, o]);
                    //Debug.Log(actualPossibleMoves[0]);
                    Vector2 startingPos = pieces[i, o].pos;
                    for (int p = 0; p < 23; p++)
                    {
                        if (actualPossibleMoves[p] != new Vector2(8,8))
                        {
                            //Debug.Log("Move Checked");
                            //Debug.Log(o);
                            //Debug.Log("l" + pieces[i, o]);
                            //Debug.Log("l"+tempPieces[i, o]);
                            tempPieces[i, o].pos = actualPossibleMoves[p];
                            //Debug.Log("in3");
                            tempPieces[(int)actualPossibleMoves[p].x, (int)actualPossibleMoves[p].y] = tempPieces[i, o];
                            tempPieces[i, o] = null;
                            if (color == "White")
                            {
                                checks = generateChecks("Black", tempPieces, Red_Highlight, UIParent, squareSize, BoardOffset);
                            }
                            else
                            {
                                checks = generateChecks("White", tempPieces, Red_Highlight, UIParent, squareSize, BoardOffset);
                            }
                            if (!checks[(int)FindKing(color, tempPieces).x, (int)FindKing(color, tempPieces).y])
                            {
                                //Debug.Log(pieces[i,o]);
                                //Debug.Log(actualPossibleMoves[p]);
                                for (int a = 0; a < 8; a++)
                                {
                                    for (int s = 0; s < 8; s++)
                                    {
                                        if (tempPieces[a, s] != null)
                                        {
                                            Destroy(tempPieces[a, s].thisObject);
                                        }
                                    }
                                }
                                return false;
                            }
                            else
                            {
                                tempPieces[i, o] = tempPieces[(int)actualPossibleMoves[p].x, (int)actualPossibleMoves[p].y];
                                tempPieces[(int)actualPossibleMoves[p].x, (int)actualPossibleMoves[p].y] = null;
                                tempPieces[i, o].pos = startingPos;
                            }
                        }
                    }
                    for (int a = 0; a < 8; a++)
                    {
                        for (int s = 0; s < 8; s++)
                        {
                            if (tempPieces[a, s] != null)
                            {
                                Destroy(tempPieces[a, s].thisObject);
                            }
                        }
                    }
                }
            }
        }
        return true;
    }
    public bool CheckForDraw(ChessPiece[,] pieces, string color)
    {
        //Debug.Log("NEWCHECKFORDRAW");
        if (color == "White")
        {
            bool[,] checks = generateChecks("Black", pieces, Red_Highlight, UIParent, squareSize, BoardOffset);
        }
        else
        {
            bool[,] checks = generateChecks("White", pieces, Red_Highlight, UIParent, squareSize, BoardOffset);
        }
        //Debug.Log(checks[(int)FindKing(color, pieces).x, (int)FindKing(color, pieces).y]);
        if (checks[(int)FindKing(color, pieces).x, (int)FindKing(color, pieces).y])
        {
            //Debug.Log("incheck");
            return false;
        }
        for (int i = 0; i < 8; i++)
        {
            for (int o = 0; o < 8; o++)
            {
                if (pieces[i, o] != null && pieces[i, o].color == color)
                {
                    //Debug.Log("in2");
                    //Debug.Log("COLOR: " + color);
                    //Debug.Log("PIECECOLOR: "+pieces[i,o].color);


                    ChessPiece[,] tempPieces = new ChessPiece[8, 8];
                    for (int a = 0; a < 8; a++)
                    {
                        for (int s = 0; s < 8; s++)
                        {
                            if (pieces[a, s] != null)
                            {
                                tempPieces[a, s] = new ChessPiece(pieces[a, s].name, Instantiate(Null_Prefab, PieceParent.transform), pieces[a, s].pos, pieces[a, s].color, squareSize, BoardOffset);
                            }
                        }
                    }
                    Vector2 startingPos = pieces[i, o].pos;
                    //if (pieces[i, o].name != "King")
                    //{
                        //Debug.Log("LOGGING PIECES FOR WHITETURN?" + WhiteTurn);
                        //Debug.Log(pieces[i,o].name);
                        //Debug.Log(pieces[i, o].color);
                        Vector2[] possibleMoves = tempPieces[i, o].DisplayMovesJustArray(Orange_Highlight, Red_Highlight, UIParent, pieces, EnPassant, checks);
                        Vector2[] actualPossibleMoves = new Vector2[23];
                        for (int a = 0; a < 23; a++)
                        {
                            actualPossibleMoves[a] = new Vector2(possibleMoves[a].x, possibleMoves[a].y);
                        }
                        for (int p = 0; p < 23; p++)
                        {
                            if (actualPossibleMoves[p]!=new Vector2(8, 8))
                            {
                                tempPieces[i, o].pos = actualPossibleMoves[p];
                                //Debug.Log("in3");
                                tempPieces[(int)actualPossibleMoves[p].x, (int)actualPossibleMoves[p].y] = tempPieces[i, o];
                                tempPieces[i, o] = null;
                                if (color == "White")
                                {
                                    checks = generateChecks("Black", tempPieces, Red_Highlight, UIParent, squareSize, BoardOffset);
                                }
                                else
                                {
                                    checks = generateChecks("White", tempPieces, Red_Highlight, UIParent, squareSize, BoardOffset);
                                }
                                if (!checks[(int)FindKing(color, tempPieces).x, (int)FindKing(color, tempPieces).y])
                                {
                                    //Debug.Log(pieces[i,o]);
                                    //Debug.Log(actualPossibleMoves[p]);
                                    for (int a = 0; a < 8; a++)
                                    {
                                        for (int s = 0; s < 8; s++)
                                        {
                                            if (tempPieces[a, s] != null)
                                            {
                                                Destroy(tempPieces[a, s].thisObject);
                                            }
                                        }
                                    }
                                    return false;
                                }
                                else
                                {
                                    tempPieces[i, o] = tempPieces[(int)actualPossibleMoves[p].x, (int)actualPossibleMoves[p].y];
                                    tempPieces[(int)actualPossibleMoves[p].x, (int)actualPossibleMoves[p].y] = null;
                                    tempPieces[i, o].pos = startingPos;
                                }
                            }
                        }
                    //}
                    //else
                    //{
                    //    kingPosX = pieces[i, o].pos.x;
                    //    kingPosY = pieces[i, o].pos.y;
                    //}
                    for (int a = 0; a < 8; a++)
                    {
                        for (int s = 0; s < 8; s++)
                        {
                            if (tempPieces[a, s] != null)
                            {
                                Destroy(tempPieces[a, s].thisObject);
                            }
                        }
                    }
                }
            }
        }
        return true;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(toBeLoaded);
    }
    public void SHPauseMenu(bool Bool)
    {
        Freeze = Bool;
        PauseMenu.SetActive(Bool);
        Pause.SetActive(!Bool);
    }
    public void tellGameObjectForScript(GameObject go)
    {
        myScriptObject = go.GetComponent<SendPieceInfo>();
    }
    public class ChessPiece
    {
        public string name;
        public Vector2 pos;
        public Vector2 previousPos;
        public string color;
        public bool firstMove = true;
        public GameObject thisObject;
        public Vector2[] shownMoves = new Vector2[23];
        public GameObject[] shownMovesGOs = new GameObject[23];
        public Vector2[] Castles = new Vector2[2];
        public float squareSize;
        public Vector2 BoardOffset;
        public int colorInt;
        public string oppositeColor;
        public ChessPiece(string name, GameObject thisObject, Vector2 pos, string color, float squareSize, Vector2 BoardOffset)
        {
            this.name = name;
            this.pos = pos;
            this.previousPos = pos;
            this.color = color;
            this.thisObject = thisObject;
            this.thisObject.transform.position = new Vector2(pos.x * squareSize, pos.y * squareSize) + BoardOffset;
            this.squareSize = squareSize;
            this.BoardOffset = BoardOffset;
            for (int i = 0; i < 23; i++)
            {
                shownMoves[i] = new Vector2(8, 8);
            }
            if (color == "White")
            {
                this.colorInt = 1;
                oppositeColor = "Black";
            }
            else
            {
                this.colorInt = -1;
                oppositeColor = "White";
            }
            if (name == "King")
            {

            }
        }
        public void DisplayMoves(GameObject Orange_Highlight, GameObject Red_Highlight, GameObject UIParent, ChessPiece[,] pieces, Vector2 EnPassant, bool[,] checks)
        {
            int posX = (int)this.pos.x;
            int posY = (int)this.pos.y;
            switch (this.name)
            {
                case "Pawn":
                    //Debug.Log(posX+" "+posY);
                    if (pieces[posX, posY + 1 * colorInt] == null)
                    {
                        AddFirstNotNull(new Vector2(posX, posY + 1 * colorInt));
                        if (firstMove && pieces[posX, posY + 2 * colorInt] == null)
                        {
                            AddFirstNotNull(new Vector2(posX, posY + 2 * colorInt));
                        }

                    }
                    if (WithinIndices(posX - 1, posY + 1 * colorInt) && pieces[posX - 1, posY + 1 * colorInt] != null && pieces[posX - 1, posY + 1 * colorInt].color == oppositeColor)
                    {
                        AddFirstNotNull(new Vector2(posX - 1, posY + 1 * colorInt));
                    }
                    else
                    {
                        if (WithinIndices(posX - 1, posY + colorInt) && pieces[posX - 1, posY + colorInt] == null && EnPassant == new Vector2(posX - 1, posY + colorInt))
                        {
                            AddFirstNotNull(new Vector2(posX - 1, posY + colorInt));
                        }
                    }
                    if (WithinIndices(posX + 1, posY + 1 * colorInt) && pieces[posX + 1, posY + 1 * colorInt] != null && pieces[posX + 1, posY + 1 * colorInt].color == oppositeColor)
                    {
                        AddFirstNotNull(new Vector2(posX + 1, posY + 1 * colorInt));
                    }
                    else
                    {
                        if (WithinIndices(posX + 1, posY + colorInt) && pieces[posX + 1, posY + colorInt] == null && EnPassant == new Vector2(posX + 1, posY + colorInt))
                        {
                            AddFirstNotNull(new Vector2(posX + 1, posY + colorInt));
                        }
                    }
                    break;
                case "Rook":
                    //Debug.Log("Rook!");
                    for (int a = posX + 1; a < 8; a++) {
                        if (pieces[a, posY] == null)
                        {
                            AddFirstNotNull(new Vector2(a, posY));
                        }
                        else
                        {
                            if (pieces[a, posY] != null && pieces[a, posY].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(a, posY));
                            }
                            break;
                        }
                    }
                    for (int a = posX - 1; a > -1; a--)
                    {
                        if (pieces[a, posY] == null)
                        {
                            AddFirstNotNull(new Vector2(a, posY));
                        }
                        else
                        {
                            if (pieces[a, posY] != null && pieces[a, posY].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(a, posY));
                            }
                            break;
                        }
                    }
                    for (int a = posY + 1; a < 8; a++)
                    {
                        if (pieces[posX, a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX, a));
                        }
                        else
                        {
                            if (pieces[posX, a] != null && pieces[posX, a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX, a));
                            }
                            break;
                        }
                    }
                    for (int a = posY - 1; a > -1; a--)
                    {
                        if (pieces[posX, a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX, a));
                        }
                        else
                        {
                            if (pieces[posX, a] != null && pieces[posX, a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX, a));
                            }
                            break;
                        }
                    }
                    break;
                case "Knight":
                    for (int a = -1; a < 2; a += 2)
                    {
                        int tempX = posX + 2 * a;
                        int tempY = posY + a;
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] == null)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] != null && pieces[tempX, tempY].color == oppositeColor)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                        tempX = posX + 2 * -a;
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] == null)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] != null && pieces[tempX, tempY].color == oppositeColor)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                        tempX = posX + a;
                        tempY = posY + a * 2;
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] == null)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] != null && pieces[tempX, tempY].color == oppositeColor)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                        //Debug.Log("PLS");
                        tempY = posY + 2 * -a;
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] == null)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] != null && pieces[tempX, tempY].color == oppositeColor)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                    }
                    break;
                case "Bishop":
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX + a, posY + a) && pieces[posX + a, posY + a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX + a, posY + a));
                        }
                        else
                        {
                            if (WithinIndices(posX + a, posY + a) && pieces[posX + a, posY + a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX + a, posY + a));
                            }
                            break;
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX + a, posY - a) && pieces[posX + a, posY - a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX + a, posY - a));
                        }
                        else
                        {
                            if (WithinIndices(posX + a, posY - a) && pieces[posX + a, posY - a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX + a, posY - a));
                            }
                            break;
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX - a, posY - a) && pieces[posX - a, posY - a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX - a, posY - a));
                        }
                        else
                        {
                            if (WithinIndices(posX - a, posY - a) && pieces[posX - a, posY - a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX - a, posY - a));
                            }
                            break;
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX - a, posY + a) && pieces[posX - a, posY + a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX - a, posY + a));
                        }
                        else
                        {
                            if (WithinIndices(posX - a, posY + a) && pieces[posX - a, posY + a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX - a, posY + a));
                            }
                            break;
                        }
                    }
                    break;
                case "Queen":
                    for (int a = posX + 1; a < 8; a++)
                    {
                        if (pieces[a, posY] == null)
                        {
                            AddFirstNotNull(new Vector2(a, posY));
                        }
                        else
                        {
                            if (pieces[a, posY] != null && pieces[a, posY].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(a, posY));
                            }
                            break;
                        }
                    }
                    for (int a = posX - 1; a > -1; a--)
                    {
                        if (pieces[a, posY] == null)
                        {
                            AddFirstNotNull(new Vector2(a, posY));
                        }
                        else
                        {
                            if (pieces[a, posY] != null && pieces[a, posY].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(a, posY));
                            }
                            break;
                        }
                    }
                    for (int a = posY + 1; a < 8; a++)
                    {
                        if (pieces[posX, a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX, a));
                        }
                        else
                        {
                            if (pieces[posX, a] != null && pieces[posX, a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX, a));
                            }
                            break;
                        }
                    }
                    for (int a = posY - 1; a > -1; a--)
                    {
                        if (pieces[posX, a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX, a));
                        }
                        else
                        {
                            if (pieces[posX, a] != null && pieces[posX, a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX, a));
                            }
                            break;
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX + a, posY + a) && pieces[posX + a, posY + a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX + a, posY + a));
                        }
                        else
                        {
                            if (WithinIndices(posX + a, posY + a) && pieces[posX + a, posY + a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX + a, posY + a));
                            }
                            break;
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX + a, posY - a) && pieces[posX + a, posY - a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX + a, posY - a));
                        }
                        else
                        {
                            if (WithinIndices(posX + a, posY - a) && pieces[posX + a, posY - a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX + a, posY - a));
                            }
                            break;
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX - a, posY - a) && pieces[posX - a, posY - a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX - a, posY - a));
                        }
                        else
                        {
                            if (WithinIndices(posX - a, posY - a) && pieces[posX - a, posY - a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX - a, posY - a));
                            }
                            break;
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX - a, posY + a) && pieces[posX - a, posY + a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX - a, posY + a));
                        }
                        else
                        {
                            if (WithinIndices(posX - a, posY + a) && pieces[posX - a, posY + a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX - a, posY + a));
                            }
                            break;
                        }
                    }
                    break;
                case "King":
                    //Debug.Log("made it to the king switch part");
                    if (WithinIndices(posX + 1, posY + 1) && (pieces[posX + 1, posY + 1] == null || (pieces[posX + 1, posY + 1] != null && pieces[posX + 1, posY + 1].color == oppositeColor)) && !checks[posX+1,posY+1])
                    {
                        AddFirstNotNull(new Vector2(posX + 1, posY + 1));
                    }
                    //Debug.Log(WithinIndices(posX + 1, posY));
                    //Debug.Log((pieces[posX + 1, posY] == null || (pieces[posX + 1, posY] != null && pieces[posX + 1, posY].color == oppositeColor)));
                    //Debug.Log(!checks[posX + 1, posY]);
                    if (WithinIndices(posX + 1, posY) && (pieces[posX + 1, posY] == null || (pieces[posX + 1, posY] != null && pieces[posX + 1, posY].color == oppositeColor)) && !checks[posX + 1, posY])
                    {
                        //Debug.Log("Didsmth");
                        AddFirstNotNull(new Vector2(posX + 1, posY));
                    }
                    if (WithinIndices(posX + 1, posY - 1) && (pieces[posX + 1, posY - 1] == null || (pieces[posX + 1, posY - 1] != null && pieces[posX + 1, posY - 1].color == oppositeColor)) && !checks[posX + 1, posY - 1])
                    {
                        AddFirstNotNull(new Vector2(posX + 1, posY - 1));
                    }
                    if (WithinIndices(posX, posY - 1) && (pieces[posX, posY - 1] == null || (pieces[posX, posY - 1] != null && pieces[posX, posY - 1].color == oppositeColor)) && !checks[posX, posY - 1])
                    {
                        AddFirstNotNull(new Vector2(posX, posY - 1));
                    }
                    if (WithinIndices(posX - 1, posY - 1) && (pieces[posX - 1, posY - 1] == null || (pieces[posX - 1, posY - 1] != null && pieces[posX - 1, posY - 1].color == oppositeColor)) && !checks[posX - 1, posY - 1])
                    {
                        AddFirstNotNull(new Vector2(posX - 1, posY - 1));
                    }
                    if (WithinIndices(posX - 1, posY) && (pieces[posX - 1, posY] == null || (pieces[posX - 1, posY] != null && pieces[posX - 1, posY].color == oppositeColor)) && !checks[posX - 1, posY])
                    {
                        AddFirstNotNull(new Vector2(posX - 1, posY));
                    }
                    if (WithinIndices(posX - 1, posY + 1) && (pieces[posX - 1, posY + 1] == null || (pieces[posX - 1, posY + 1] != null && pieces[posX - 1, posY + 1].color == oppositeColor)) && !checks[posX - 1, posY + 1])
                    {
                        AddFirstNotNull(new Vector2(posX - 1, posY + 1));
                    }
                    if (WithinIndices(posX, posY + 1) && (pieces[posX, posY + 1] == null || (pieces[posX, posY + 1] != null && pieces[posX, posY + 1].color == oppositeColor)) && !checks[posX, posY + 1])
                    {
                        AddFirstNotNull(new Vector2(posX, posY + 1));
                    }
                    if (firstMove && pieces[0, posY] != null && pieces[0, posY].name == "Rook" && pieces[0, posY].firstMove == true && pieces[1, posY] == null && pieces[2, posY] == null && !checks[2, posY] && pieces[3, posY] == null && !checks[3, posY])
                    {
                        AddFirstNotNull(new Vector2(2, posY));
                        Castles[0] = new Vector2(2, posY);
                        if (Castles[0] != new Vector2(8, 8))
                        {
                            Castles[0] = new Vector2(2, posY);
                        }
                    }
                    if (firstMove && pieces[7, posY] != null && pieces[7, posY].name == "Rook" && pieces[7, posY].firstMove == true && pieces[5, posY] == null && !checks[5, posY] && pieces[6, posY] == null && !checks[6, posY])
                    {
                        AddFirstNotNull(new Vector2(6, posY));
                        Castles[0] = new Vector2(6, posY);
                        if (Castles[0] != new Vector2(8, 8))
                        {
                            Castles[0] = new Vector2(6, posY);
                        }
                        else
                        {
                            Castles[1] = new Vector2(6, posY);
                        }
                    }
                    break;
                case null: break;
            }
            for (int i = 0; i < 23; i++)
            {
                if (shownMoves[i] != new Vector2(8, 8))
                {
                    if (pieces[(int)shownMoves[i].x, (int)shownMoves[i].y] != null || (shownMoves[i] == EnPassant && this.name == "Pawn"))
                    {
                        this.shownMovesGOs[i] = Instantiate(Red_Highlight, new Vector2(shownMoves[i].x * squareSize, shownMoves[i].y * squareSize) + BoardOffset, Quaternion.identity, UIParent.transform);
                    }
                    else
                    {
                        this.shownMovesGOs[i] = Instantiate(Orange_Highlight, new Vector2(shownMoves[i].x * squareSize, shownMoves[i].y * squareSize) + BoardOffset, Quaternion.identity, UIParent.transform);
                    }
                }
            }
            //this.Move(new Vector2(pos.x, pos.y + 2), pieces);
        }
        public Vector2[] DisplayMovesJustArray(GameObject Orange_Highlight, GameObject Red_Highlight, GameObject UIParent, ChessPiece[,] pieces, Vector2 EnPassant, bool[,] checks)
        {
            int posX = (int)this.pos.x;
            int posY = (int)this.pos.y;
            switch (this.name)
            {
                case "Pawn":
                    //Debug.Log(posX+" "+posY);
                    if (pieces[posX, posY + 1 * colorInt] == null)
                    {
                        AddFirstNotNull(new Vector2(posX, posY + 1 * colorInt));
                        if (firstMove && pieces[posX, posY + 2 * colorInt] == null)
                        {
                            AddFirstNotNull(new Vector2(posX, posY + 2 * colorInt));
                        }

                    }
                    if (WithinIndices(posX - 1, posY + 1 * colorInt) && pieces[posX - 1, posY + 1 * colorInt] != null && pieces[posX - 1, posY + 1 * colorInt].color == oppositeColor)
                    {
                        AddFirstNotNull(new Vector2(posX - 1, posY + 1 * colorInt));
                    }
                    else
                    {
                        if (WithinIndices(posX - 1, posY + colorInt) && pieces[posX - 1, posY + colorInt] == null && EnPassant == new Vector2(posX - 1, posY + colorInt))
                        {
                            AddFirstNotNull(new Vector2(posX - 1, posY + colorInt));
                        }
                    }
                    if (WithinIndices(posX + 1, posY + 1 * colorInt) && pieces[posX + 1, posY + 1 * colorInt] != null && pieces[posX + 1, posY + 1 * colorInt].color == oppositeColor)
                    {
                        AddFirstNotNull(new Vector2(posX + 1, posY + 1 * colorInt));
                    }
                    else
                    {
                        if (WithinIndices(posX + 1, posY + colorInt) && pieces[posX + 1, posY + colorInt] == null && EnPassant == new Vector2(posX + 1, posY + colorInt))
                        {
                            AddFirstNotNull(new Vector2(posX + 1, posY + colorInt));
                        }
                    }
                    break;
                case "Rook":
                    //Debug.Log("Rook!");
                    for (int a = posX + 1; a < 8; a++)
                    {
                        if (pieces[a, posY] == null)
                        {
                            AddFirstNotNull(new Vector2(a, posY));
                        }
                        else
                        {
                            if (pieces[a, posY] != null && pieces[a, posY].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(a, posY));
                            }
                            break;
                        }
                    }
                    for (int a = posX - 1; a > -1; a--)
                    {
                        if (pieces[a, posY] == null)
                        {
                            AddFirstNotNull(new Vector2(a, posY));
                        }
                        else
                        {
                            if (pieces[a, posY] != null && pieces[a, posY].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(a, posY));
                            }
                            break;
                        }
                    }
                    for (int a = posY + 1; a < 8; a++)
                    {
                        if (pieces[posX, a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX, a));
                        }
                        else
                        {
                            if (pieces[posX, a] != null && pieces[posX, a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX, a));
                            }
                            break;
                        }
                    }
                    for (int a = posY - 1; a > 0; a--)
                    {
                        if (pieces[posX, a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX, a));
                        }
                        else
                        {
                            if (pieces[posX, a] != null && pieces[posX, a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX, a));
                            }
                            break;
                        }
                    }
                    break;
                case "Knight":
                    for (int a = -1; a < 2; a += 2)
                    {
                        int tempX = posX + 2 * a;
                        int tempY = posY + a;
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] == null)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] != null && pieces[tempX, tempY].color == oppositeColor)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                        tempX = posX + 2 * -a;
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] == null)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] != null && pieces[tempX, tempY].color == oppositeColor)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                        tempX = posX + a;
                        tempY = posY + a * 2;
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] == null)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] != null && pieces[tempX, tempY].color == oppositeColor)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                        //Debug.Log("PLS");
                        tempY = posY + 2 * -a;
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] == null)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                        if (WithinIndices(tempX, tempY) && pieces[tempX, tempY] != null && pieces[tempX, tempY].color == oppositeColor)
                        {
                            AddFirstNotNull(new Vector2(tempX, tempY));
                        }
                    }
                    break;
                case "Bishop":
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX + a, posY + a) && pieces[posX + a, posY + a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX + a, posY + a));
                        }
                        else
                        {
                            if (WithinIndices(posX + a, posY + a) && pieces[posX + a, posY + a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX + a, posY + a));
                            }
                            break;
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX + a, posY - a) && pieces[posX + a, posY - a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX + a, posY - a));
                        }
                        else
                        {
                            if (WithinIndices(posX + a, posY - a) && pieces[posX + a, posY - a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX + a, posY - a));
                            }
                            break;
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX - a, posY - a) && pieces[posX - a, posY - a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX - a, posY - a));
                        }
                        else
                        {
                            if (WithinIndices(posX - a, posY - a) && pieces[posX - a, posY - a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX - a, posY - a));
                            }
                            break;
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX - a, posY + a) && pieces[posX - a, posY + a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX - a, posY + a));
                        }
                        else
                        {
                            if (WithinIndices(posX - a, posY + a) && pieces[posX - a, posY + a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX - a, posY + a));
                            }
                            break;
                        }
                    }
                    break;
                case "Queen":
                    for (int a = posX + 1; a < 8; a++)
                    {
                        if (pieces[a, posY] == null)
                        {
                            AddFirstNotNull(new Vector2(a, posY));
                        }
                        else
                        {
                            if (pieces[a, posY] != null && pieces[a, posY].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(a, posY));
                            }
                            break;
                        }
                    }
                    for (int a = posX - 1; a > -1; a--)
                    {
                        if (pieces[a, posY] == null)
                        {
                            AddFirstNotNull(new Vector2(a, posY));
                        }
                        else
                        {
                            if (pieces[a, posY] != null && pieces[a, posY].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(a, posY));
                            }
                            break;
                        }
                    }
                    for (int a = posY + 1; a < 8; a++)
                    {
                        if (pieces[posX, a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX, a));
                        }
                        else
                        {
                            if (pieces[posX, a] != null && pieces[posX, a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX, a));
                            }
                            break;
                        }
                    }
                    for (int a = posY - 1; a > 0; a--)
                    {
                        if (pieces[posX, a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX, a));
                        }
                        else
                        {
                            if (pieces[posX, a] != null && pieces[posX, a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX, a));
                            }
                            break;
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX + a, posY + a) && pieces[posX + a, posY + a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX + a, posY + a));
                        }
                        else
                        {
                            if (WithinIndices(posX + a, posY + a) && pieces[posX + a, posY + a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX + a, posY + a));
                            }
                            break;
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX + a, posY - a) && pieces[posX + a, posY - a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX + a, posY - a));
                        }
                        else
                        {
                            if (WithinIndices(posX + a, posY - a) && pieces[posX + a, posY - a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX + a, posY - a));
                            }
                            break;
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX - a, posY - a) && pieces[posX - a, posY - a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX - a, posY - a));
                        }
                        else
                        {
                            if (WithinIndices(posX - a, posY - a) && pieces[posX - a, posY - a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX - a, posY - a));
                            }
                            break;
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX - a, posY + a) && pieces[posX - a, posY + a] == null)
                        {
                            AddFirstNotNull(new Vector2(posX - a, posY + a));
                        }
                        else
                        {
                            if (WithinIndices(posX - a, posY + a) && pieces[posX - a, posY + a].color == oppositeColor)
                            {
                                AddFirstNotNull(new Vector2(posX - a, posY + a));
                            }
                            break;
                        }
                    }
                    break;
                case "King":
                    if (WithinIndices(posX + 1, posY + 1) && (pieces[posX + 1, posY + 1] == null || (pieces[posX + 1, posY + 1] != null && pieces[posX + 1, posY + 1].color == oppositeColor)) && !checks[posX + 1, posY + 1])
                    {
                        AddFirstNotNull(new Vector2(posX + 1, posY + 1));
                    }
                    if (WithinIndices(posX + 1, posY) && (pieces[posX + 1, posY] == null || (pieces[posX + 1, posY] != null && pieces[posX + 1, posY].color == oppositeColor)) && !checks[posX + 1, posY])
                    {
                        AddFirstNotNull(new Vector2(posX + 1, posY));
                    }
                    if (WithinIndices(posX + 1, posY - 1) && (pieces[posX + 1, posY - 1] == null || (pieces[posX + 1, posY - 1] != null && pieces[posX + 1, posY - 1].color == oppositeColor)) && !checks[posX + 1, posY - 1])
                    {
                        AddFirstNotNull(new Vector2(posX + 1, posY - 1));
                    }
                    if (WithinIndices(posX, posY - 1) && (pieces[posX, posY - 1] == null || (pieces[posX, posY - 1] != null && pieces[posX, posY - 1].color == oppositeColor)) && !checks[posX, posY - 1])
                    {
                        AddFirstNotNull(new Vector2(posX, posY - 1));
                    }
                    if (WithinIndices(posX - 1, posY - 1) && (pieces[posX - 1, posY - 1] == null || (pieces[posX - 1, posY - 1] != null && pieces[posX - 1, posY - 1].color == oppositeColor)) && !checks[posX - 1, posY - 1])
                    {
                        AddFirstNotNull(new Vector2(posX - 1, posY - 1));
                    }
                    if (WithinIndices(posX - 1, posY) && (pieces[posX - 1, posY] == null || (pieces[posX - 1, posY] != null && pieces[posX - 1, posY].color == oppositeColor)) && !checks[posX - 1, posY])
                    {
                        AddFirstNotNull(new Vector2(posX - 1, posY));
                    }
                    if (WithinIndices(posX - 1, posY + 1) && (pieces[posX - 1, posY + 1] == null || (pieces[posX - 1, posY + 1] != null && pieces[posX - 1, posY + 1].color == oppositeColor)) && !checks[posX - 1, posY + 1])
                    {
                        AddFirstNotNull(new Vector2(posX - 1, posY + 1));
                    }
                    if (WithinIndices(posX, posY + 1) && (pieces[posX, posY + 1] == null || (pieces[posX, posY + 1] != null && pieces[posX, posY + 1].color == oppositeColor)) && !checks[posX, posY + 1])
                    {
                        AddFirstNotNull(new Vector2(posX, posY + 1));
                    }
                    if (firstMove && pieces[0, posY] != null && pieces[0, posY].name == "Rook" && pieces[0, posY].firstMove == true && pieces[1, posY] == null && pieces[2, posY] == null && !checks[2, posY] && pieces[3, posY] == null && !checks[3, posY])
                    {
                        AddFirstNotNull(new Vector2(2, posY));
                        Castles[0] = new Vector2(2, posY);
                        if (Castles[0] != new Vector2(8, 8))
                        {
                            Castles[0] = new Vector2(2, posY);
                        }
                    }
                    if (firstMove && pieces[7, posY] != null && pieces[7, posY].name == "Rook" && pieces[7, posY].firstMove == true && pieces[5, posY] == null && !checks[5, posY] && pieces[6, posY] == null && !checks[6, posY])
                    {
                        AddFirstNotNull(new Vector2(6, posY));
                        Castles[0] = new Vector2(6, posY);
                        if (Castles[0] != new Vector2(8, 8))
                        {
                            Castles[0] = new Vector2(6, posY);
                        }
                        else
                        {
                            Castles[1] = new Vector2(6, posY);
                        }
                    }
                    break;
                case null: break;
            }
            return this.shownMoves;
            //this.Move(new Vector2(pos.x, pos.y + 2), pieces);
        }
        public ChessPiece[,] DeletePiece(ChessPiece[,] pieces, bool ep)
        {
            if (ep)
            {
                pieces[(int)this.pos.x, (int)this.pos.y] = null;
            }
            Destroy(this.thisObject);
            return pieces;
        }
        public bool WithinIndices(int tempX, int tempY)
        {
            if (tempX > -1 && tempX < 8 && tempY > -1 && tempY < 8)
            {
                return true;
            }
            return false;
        }
        public bool WithinIndices(int temp) {
            if (temp > -1 && temp < 8)
            {
                return true;
            }
            return false;
        }
        public void AddFirstNotNull(Vector2 move)
        {
            for (int s = 0; s < 23; s++)
            {
                //Debug.Log(shownMoves[s]);
                if (shownMoves[s] == new Vector2(8, 8))
                {
                    //Debug.Log("whynowork");
                    shownMoves[s] = move;
                    break;
                }
            }
        }
        public void DestroyMoves()
        {
            foreach (var item in shownMovesGOs)
            {
                Destroy(item);
            }
            for (int i = 0; i < 23; i++)
            {
                shownMoves[i] = new Vector2(8, 8);
            }
            for (int i = 0; i < 2; i++)
            {
                Castles[i] = new Vector2(8, 8);
            }
        }
        public ArrayList Move(Vector2 newPos, ChessPiece[,] tempPieces, Vector2 EnPassant, ChessPiece EnPassantPiece, Vector2 movedPiece, ChessPiece[,] pieces, GameObject Red_Highlight, GameObject UIHighlight, float squareSize, Vector2 BoardOffset, bool WhiteTurn)
        {
            this.previousPos = pos;
            ArrayList arrayList = new ArrayList();
            if (this.name == "Pawn" && newPos.x == this.pos.x && newPos.y == this.pos.y + 2 * colorInt)
            {
                //Debug.Log("ENPASSANT");
                //Debug.Log(this.pos);
                EnPassant = new Vector2(this.pos.x, this.pos.y + colorInt);
                EnPassantPiece = this;
                //Debug.Log("EP" + EnPassant);
                //Debug.Log(EnPassant);
            }
            //Debug.Log("1 "+EnPassantPiece.pos);
            tempPieces[(int)newPos.x, (int)newPos.y] = this;
            tempPieces[(int)pos.x, (int)pos.y] = null;
            bool[,] checks = generateChecks(this.oppositeColor, tempPieces, Red_Highlight, UIHighlight, squareSize, BoardOffset);
            //Debug.Log(checks[(int)FindKing("White", tempPieces).x, (int)FindKing("White", tempPieces).y]);
            if ((WhiteTurn && !checks[(int)FindKing("White", tempPieces).x, (int)FindKing("White", tempPieces).y]) || (!WhiteTurn && !checks[(int)FindKing("Black", tempPieces).x, (int)FindKing("Black", tempPieces).y]))
            {
                this.firstMove = false;
                this.pos = newPos;
                arrayList.Add(true);
                this.thisObject.transform.position = new Vector2(pos.x * squareSize, pos.y * squareSize) + BoardOffset;
                if (pieces[(int)pos.x, (int)pos.y] != null)
                {
                    pieces = pieces[(int)pos.x, (int)pos.y].DeletePiece(pieces, false);
                }
                else
                {
                    //Debug.Log("FLAGEP" + EnPassant);
                    if (this.name == "Pawn" && newPos == EnPassant)
                    {
                        pieces = pieces[(int)newPos.x, (int)newPos.y - colorInt].DeletePiece(pieces, true);
                    }
                }
                pieces[(int)pos.x, (int)pos.y] = this;
                pieces[(int)this.previousPos.x, (int)this.previousPos.y] = null;
                //Debug.Log("2 " + (pieces[(int)movedPiece.x, (int)movedPiece.y] != null));
                //Debug.Log("3 " + (pieces[(int)movedPiece.x, (int)movedPiece.y] != null));
                if (Castles[0] == newPos)
                {
                    if (newPos.x == 2)
                    {
                        pieces[0, (int)pos.y].Move(new Vector2(3, pos.y), tempPieces, EnPassant, EnPassantPiece, movedPiece, pieces, Red_Highlight, UIHighlight, squareSize, BoardOffset, WhiteTurn);
                    }
                    if (newPos.x == 6)
                    {
                        pieces[7, (int)pos.y].Move(new Vector2(5, pos.y), tempPieces, EnPassant, EnPassantPiece, movedPiece, pieces, Red_Highlight, UIHighlight, squareSize, BoardOffset, WhiteTurn);
                    }
                }
                else if (Castles[1] == newPos)
                {
                    if (newPos.x == 2)
                    {
                        pieces[0, (int)pos.y].Move(new Vector2(3, pos.y), tempPieces, EnPassant, EnPassantPiece, movedPiece, pieces, Red_Highlight, UIHighlight, squareSize, BoardOffset, WhiteTurn);
                    }
                    if (newPos.x == 6)
                    {
                        pieces[7, (int)pos.y].Move(new Vector2(5, pos.y), tempPieces, EnPassant, EnPassantPiece, movedPiece, pieces, Red_Highlight, UIHighlight, squareSize, BoardOffset, WhiteTurn);
                    }
                }

                this.DestroyMoves();
                arrayList.Add(pieces);
                if (this != EnPassantPiece)
                {
                    EnPassant = new Vector2(8, 8);
                }
                arrayList.Add(EnPassant);
                if (EnPassant != new Vector2(8, 8))
                {
                    arrayList.Add(this);
                }
                else
                {
                    arrayList.Add(null);
                }
                if (this.name == "Pawn" && (this.pos.y == 7 || this.pos.y == 0))
                {
                    arrayList.Add(this.pos);
                }
                else
                {
                    arrayList.Add(new Vector2(8, 8));
                }
                arrayList.Add(this.pos);
                //Debug.Log("4 " + (pieces[(int)movedPiece.x, (int)movedPiece.y] != null));
                //Debug.Log("2 " + EnPassantPiece.pos);
                //Debug.Log(this != EnPassantPiece);
                //Debug.Log("IF" + arrayList[0]);
            }
            else
            {
                arrayList.Add(false);
            }
            Debug.Log("1; Pr" + previousPos + "; P" + pos);
            arrayList.Add(this.pos);
            arrayList.Add(this.previousPos);
            //for (int a = 0; a < 8; a++)
            //{
            //    for (int s = 0; s < 8; s++)
            //    {
            //        if (tempPieces[a, s] != null)
            //        {
            //            Destroy(tempPieces[a, s].thisObject);
            //        }
            //    }
            //}
            return arrayList;
        }
        public void PieceChangeName(string name, GameObject PieceParent, GameObject White_Knight, GameObject White_Bishop, GameObject White_Rook, GameObject White_Queen, GameObject Black_Knight, GameObject Black_Bishop, GameObject Black_Rook, GameObject Black_Queen)
        {
            this.name = name;
            Destroy(this.thisObject);
            if (this.color == "White")
            {
                switch (name)
                {
                    case "Knight":
                        this.thisObject = Instantiate(White_Knight, PieceParent.transform);
                        this.thisObject.transform.position = new Vector2(pos.x * squareSize, pos.y * squareSize) + BoardOffset;
                        break;
                    case "Bishop":
                        this.thisObject = Instantiate(White_Bishop, PieceParent.transform);
                        this.thisObject.transform.position = new Vector2(pos.x * squareSize, pos.y * squareSize) + BoardOffset;
                        break;
                    case "Rook":
                        this.thisObject = Instantiate(White_Rook, PieceParent.transform);
                        this.thisObject.transform.position = new Vector2(pos.x * squareSize, pos.y * squareSize) + BoardOffset;
                        break;
                    case "Queen":
                        this.thisObject = Instantiate(White_Queen, PieceParent.transform);
                        this.thisObject.transform.position = new Vector2(pos.x * squareSize, pos.y * squareSize) + BoardOffset;
                        break;
                }
            }
            else
            {
                switch (name)
                {
                    case "Knight":
                        this.thisObject = Instantiate(Black_Knight, PieceParent.transform);
                        this.thisObject.transform.position = new Vector2(pos.x * squareSize, pos.y * squareSize) + BoardOffset;
                        break;
                    case "Bishop":
                        this.thisObject = Instantiate(Black_Bishop, PieceParent.transform);
                        this.thisObject.transform.position = new Vector2(pos.x * squareSize, pos.y * squareSize) + BoardOffset;
                        break;
                    case "Rook":
                        this.thisObject = Instantiate(Black_Rook, PieceParent.transform);
                        this.thisObject.transform.position = new Vector2(pos.x * squareSize, pos.y * squareSize) + BoardOffset;
                        break;
                    case "Queen":
                        this.thisObject = Instantiate(Black_Queen, PieceParent.transform);
                        this.thisObject.transform.position = new Vector2(pos.x * squareSize, pos.y * squareSize) + BoardOffset;
                        break;
                }
            }
        }
        public bool[,] generatePieceChecks(ChessPiece[,] pieces)
        {
            bool[,] tempChecks = new bool[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int o = 0; o < 8; o++)
                {
                    tempChecks[i, o] = false;
                }
            }
            int posX = (int)this.pos.x;
            int posY = (int)this.pos.y;
            switch (this.name)
            {
                case "Pawn":
                    if (WithinIndices(posX - 1, posY + colorInt))
                    {
                        tempChecks[posX - 1, posY + colorInt] = true;
                    }
                    if (WithinIndices(posX + 1, posY +colorInt))
                    {
                        tempChecks[posX + 1, posY + colorInt] = true;
                    }
                    break;
                case "Rook":
                    for (int a = posX + 1; a < 8; a++)
                    {
                        if (pieces[a, posY] == null || (pieces[a, posY].color == oppositeColor && pieces[a, posY].name == "King"))
                        {
                            tempChecks[a, posY] = true;
                        }
                        else
                        {
                            tempChecks[a, posY] = true;
                            break;
                        }
                    }
                    for (int a = posX - 1; a > -1; a--)
                    {
                        if (pieces[a, posY] == null || (pieces[a, posY].color == oppositeColor && pieces[a, posY].name == "King"))
                        {
                            tempChecks[a, posY] = true;
                        }
                        else
                        {
                            tempChecks[a, posY] = true;
                            break;
                        }
                    }
                    for (int a = posY + 1; a < 8; a++)
                    {
                        if (pieces[posX, a] == null || (pieces[posX, a].color == oppositeColor && pieces[posX, a].name == "King"))
                        {
                            tempChecks[posX, a] = true;
                        }
                        else
                        {
                            tempChecks[posX, a] = true;
                            break;
                        }
                    }
                    for (int a = posY - 1; a > 0; a--)
                    {
                        if (pieces[posX, a] == null || (pieces[posX, a].color == oppositeColor && pieces[posX, a].name == "King"))
                        {
                            tempChecks[posX, a] = true;
                        }
                        else
                        {
                            tempChecks[posX, a] = true;
                            break;
                        }
                    }
                    break;
                case "Knight":
                    for (int a = -1; a < 2; a += 2)
                    {
                        int tempX = posX + 2 * a;
                        int tempY = posY + a;
                        if (WithinIndices(tempX, tempY))
                        {
                            tempChecks[tempX, tempY] = true;
                        }
                        tempX = posX + 2 * -a;
                        if (WithinIndices(tempX, tempY))
                        {
                            tempChecks[tempX, tempY] = true;
                        }
                        tempX = posX + a;
                        tempY = posY + a * 2;
                        if (WithinIndices(tempX, tempY))
                        {
                            tempChecks[tempX, tempY] = true;
                        }
                        //Debug.Log("PLS");
                        tempY = posY + 2 * -a;
                        if (WithinIndices(tempX, tempY))
                        {
                            tempChecks[tempX, tempY] = true;
                        }
                    }
                    break;
                case "Bishop":
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX + a, posY + a))
                            if (pieces[posX + a, posY + a] == null || (pieces[posX + a, posY + a].color == oppositeColor && pieces[posX + a, posY + a].name == "King"))
                            {
                                tempChecks[posX + a, posY + a] = true;
                            }else
                            {
                                tempChecks[posX + a, posY + a] = true;
                                break;
                            }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX + a, posY - a)){
                            if (pieces[posX + a, posY - a] == null || (pieces[posX + a, posY - a].color == oppositeColor && pieces[posX + a, posY - a].name == "King"))
                            {
                                tempChecks[posX + a, posY - a] = true;
                            }else
                            {
                                tempChecks[posX + a, posY - a] = true;
                                break;
                            }
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX - a, posY - a)){
                            if (pieces[posX - a, posY - a] == null || (pieces[posX - a, posY - a].color == oppositeColor && pieces[posX - a, posY - a].name == "King")) {
                                tempChecks[posX - a, posY - a] = true;
                            }else
                            {
                                tempChecks[posX - a, posY - a] = true;
                                break;
                            }
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX - a, posY + a))
                        {
                            if (pieces[posX - a, posY + a] == null || (pieces[posX - a, posY + a].color == oppositeColor && pieces[posX - a, posY + a].name == "King"))
                            {
                                tempChecks[posX - a, posY + a] = true;
                            }
                            else
                            {
                                tempChecks[posX - a, posY + a] = true;
                                break;
                            }
                        }
                    }
                    break;
                case "Queen":
                    for (int a = posX + 1; a < 8; a++)
                    {
                        if (pieces[a, posY] == null || (pieces[a, posY].color == oppositeColor && pieces[a, posY].name == "King"))
                        {
                            tempChecks[a, posY] = true;
                        }
                        else
                        {
                            tempChecks[a, posY] = true;
                            break;
                        }
                    }
                    for (int a = posX - 1; a > -1; a--)
                    {
                        if (pieces[a, posY] == null || (pieces[a, posY].color == oppositeColor && pieces[a, posY].name == "King"))
                        {
                            tempChecks[a, posY] = true;
                        }
                        else
                        {
                            tempChecks[a, posY] = true;
                            break;
                        }
                    }
                    for (int a = posY + 1; a < 8; a++)
                    {
                        if (pieces[posX, a] == null || (pieces[posX, a].color == oppositeColor && pieces[posX, a].name == "King"))
                        {
                            tempChecks[posX, a] = true;
                        }
                        else
                        {
                            tempChecks[posX, a] = true;
                            break;
                        }
                    }
                    for (int a = posY - 1; a > -1; a--)
                    {
                        if (pieces[posX, a] == null || (pieces[posX, a].color == oppositeColor && pieces[posX, a].name == "King"))
                        {
                            if ((pieces[posX, a] != null && pieces[posX, a].color == oppositeColor && pieces[posX, a].name == "King"))
                            {
                                //Debug.Log("HMMMMMM");
                                if (a == 1)
                                {
                                    //Debug.Log("MOREHMM");
                                }
                            }
                            tempChecks[posX, a] = true;
                            //Debug.Log(tempChecks[4, 0]);
                        }
                        else
                        {
                            tempChecks[posX, a] = true;
                            //Debug.Log(tempChecks[4, 0]);
                            break;
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX + a, posY + a))
                            if (pieces[posX + a, posY + a] == null || (pieces[posX + a, posY + a].color == oppositeColor && pieces[posX + a, posY + a].name == "King"))
                            {
                                tempChecks[posX + a, posY + a] = true;
                            }
                            else
                            {
                                tempChecks[posX + a, posY + a] = true;
                                break;
                            }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX + a, posY - a))
                        {
                            if (pieces[posX + a, posY - a] == null || (pieces[posX + a, posY - a].color == oppositeColor && pieces[posX + a, posY - a].name == "King"))
                            {
                                tempChecks[posX + a, posY - a] = true;
                            }
                            else
                            {
                                tempChecks[posX + a, posY - a] = true;
                                break;
                            }
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX - a, posY - a))
                        {
                            if (pieces[posX - a, posY - a] == null || (pieces[posX - a, posY - a].color == oppositeColor && pieces[posX - a, posY - a].name == "King"))
                            {
                                tempChecks[posX - a, posY - a] = true;
                            }
                            else
                            {
                                tempChecks[posX - a, posY - a] = true;
                                break;
                            }
                        }
                    }
                    for (int a = 1; a < 8; a++)
                    {
                        if (WithinIndices(posX - a, posY + a))
                        {
                            if (pieces[posX - a, posY + a] == null || (pieces[posX - a, posY + a].color == oppositeColor && pieces[posX - a, posY + a].name == "King"))
                            {
                                tempChecks[posX - a, posY + a] = true;
                            }
                            else
                            {
                                tempChecks[posX - a, posY + a] = true;
                                break;
                            }
                        }
                    }
                    break;
                case "King":
                    if (WithinIndices(posX + 1, posY + 1))
                    {
                        tempChecks[posX + 1, posY + 1] = true;
                    }
                    if (WithinIndices(posX + 1, posY))
                    {
                        tempChecks[posX + 1, posY] = true;
                    }
                    if (WithinIndices(posX + 1, posY - 1))
                    {
                        tempChecks[posX + 1, posY - 1] = true;
                    }
                    if (WithinIndices(posX, posY - 1))
                    {
                        tempChecks[posX, posY - 1] = true;
                    }
                    if (WithinIndices(posX - 1, posY - 1))
                    {
                        tempChecks[posX - 1, posY - 1] = true;
                    }
                    if (WithinIndices(posX - 1, posY))
                    {
                        tempChecks[posX - 1, posY] = true;
                    }
                    if (WithinIndices(posX - 1, posY + 1))
                    {
                        tempChecks[posX - 1, posY + 1] = true;
                    }
                    if (WithinIndices(posX, posY + 1))
                    {
                        tempChecks[posX, posY + 1] = true;
                    }
                    break;
            }
            return tempChecks;
        }
    }
}