using UnityEngine;
public  class Chessman : MonoBehaviour
{
   // public SO_ChessPice blackBishop;
   // public SO_ChessPice blackKing;
   // public SO_ChessPice blackKnight;
   // public SO_ChessPice blackPawn;
   // public SO_ChessPice blackQueen;
   // public SO_ChessPice blackRook;
   // public SO_ChessPice whiteBishop;
   // public SO_ChessPice whiteKing;
   // public SO_ChessPice whiteKnight;
   // public SO_ChessPice whitePawn;
   // public SO_ChessPice whiteQueen;
   // public SO_ChessPice whiteRook;

    public PiceType piceType;

    //referencias
    public GameObject controller;
    public GameObject movePlate;
       

    public Sprite sprite;

    //posiciones
    private int xBoard = -1;
    private int yBoard = -1;

    //variable para ver si sos piezas "negras" o "blancas"
    private string player;

    //variable para el enroque
    private bool castling_l_black = false;
    private bool castling_l_white = false;
    private bool castling_s_black = false;
    private bool castling_s_white = false;
    public bool moved = false;
    public string movimiento;


    //referencias para todos los sprites
    [SerializeField] private Sprite black_queen;
    [SerializeField] private Sprite black_knight;
    [SerializeField] private Sprite black_bishop;
    [SerializeField] private Sprite black_king;
    [SerializeField] private Sprite black_rook;
    [SerializeField] private Sprite black_pawn;
    [SerializeField] private Sprite white_queen;
    [SerializeField] private Sprite white_knight;
    [SerializeField] private Sprite white_bishop;
    [SerializeField] private Sprite white_king;
    [SerializeField] private Sprite white_rook;
    [SerializeField] private Sprite white_pawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");     

        SetCoords();

        //Choose correct sprite based on piece's name
        
        switch (this.name)
        {
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_rook1": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; break;
            case "black_rook2": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; break;
            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_rook1": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; break;
            case "white_rook2": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; break;
        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        //separacion de piezas
        x *= 1.1f;
        y *= 1.1f;

        //lugar donde empieza la torre blanca
        x += -3.85f;
        y += -3.85f;

        this.transform.position = new Vector3(x, y, -1.0f);

    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp() //cuando este el mouse arriba de el objeto hace esto
    {
        Debug.Log(moved);

        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            DestroyMovePlates();

            InitiateMovePlates();
        }
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, -1);
                break;
            case "black_king":
            case "white_king":
                SurroundMovePlate();
                CastlingLarge();
                //     CastlingShort();
                break;
            case "black_rook1":
            case "black_rook2":
            case "white_rook1":
            case "white_rook2":
                CastlingLarge();
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate(xBoard, yBoard - 1);
                if (yBoard == 6)
                {
                    PawnMovePlate(xBoard, yBoard - 2);
                }
                break;
            case "white_pawn":
                PawnMovePlate(xBoard, yBoard + 1);
                if (yBoard == 1)
                {
                    PawnMovePlate(xBoard, yBoard + 2);
                }
                break;

        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        movimiento = "normal";
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chessman>().player != player)
        {
            MovePlateAttackSpawn(x, y);
        }
    }

    public void LMovePlate()
    {
        movimiento = "normal";
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        movimiento = "normal";
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 0);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 0);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (cp.GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(int x, int y) //movimiento y ataque peon
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {            
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }
            //ataque
            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);                
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        //tama;o de la matriz para los movimientos del tablero igual que en SetCors()
        x *= 1.1f;
        y *= 1.1f;

        //lugar donde empieza la torre blanca
        x += -3.85f;
        y += -3.85f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);


    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        //tama;o de la matriz para los movimientos del tablero igual que en SetCors()
        x *= 1.1f;
        y *= 1.1f;

        //lugar donde empieza la torre blanca
        x += -3.85f;
        y += -3.85f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);

    }

    public void CastlingLarge() //funcion para el enroque LARGO ARREGLAR!!!!!
    {
        Game sc = controller.GetComponent<Game>();

        if(GameObject.Find("black_king").GetComponent<Chessman>().moved == false 
            && GameObject.Find("black_rook1").GetComponent<Chessman>().moved == false 
            && sc.GetPosition(1,7) == null && sc.GetPosition(2, 7) == null && sc.GetPosition(3, 7) == null)
        {
            movimiento = "castling_large";
            GameObject.Find("black_king").GetComponent<Chessman>().MovePlateSpawn(2,7);
            GameObject.Find("black_rook1").GetComponent<Chessman>().MovePlateSpawn(3,7);
        //    ASI HACEMOS QUE SE MUEVE
        //    GameObject.Find("black_king").GetComponent<Chessman>().SetXBoard(2);
        //    GameObject.Find("black_king").GetComponent<Chessman>().SetYBoard(7);
        //    GameObject.Find("black_king").GetComponent<Chessman>().SetCoords();
        //    GameObject.Find("black_rook1").GetComponent<Chessman>().SetXBoard(3);
        //    GameObject.Find("black_rook1").GetComponent<Chessman>().SetYBoard(7);
        //    GameObject.Find("black_rook1").GetComponent<Chessman>().SetCoords();

        }

        
    }
}
