using UnityEngine;
public  class Chessman : MonoBehaviour
{
    public SO_ChessPice pice;

    public PiceType piceType;

    //referencias
    public GameObject controller;
    public GameObject movePlate;

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
                //     CastlingLarge();
                //     CastlingShort();
                break;
            case "black_rook":
            case "white_rook":
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

    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

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

    //public void CastlingLarge() //funcion para el enroque LARGO ARREGLAR!!!!!
    //{
    //    public int a;
    //    for(f=1; f<4; f++)
    //    {
    //
    //    }
    //    switch (this.name)
    //    {
    //        case "black_king":
    //            if (xBoard != 6 && yBoard != 7)
    //            {
    //                castling_l_black = true;
    //            }            
    //
    //            break;
    //            
    //    }
    //    
    //    }
}
