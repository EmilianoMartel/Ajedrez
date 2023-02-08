using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//preparamos los equipos
public class Team : MonoBehaviour
{
    public SO_ChessPice blackBishop;
    public SO_ChessPice blackKing;
    public SO_ChessPice blackKnight;
    public SO_ChessPice blackPawn;
    public SO_ChessPice blackQueen;
    public SO_ChessPice blackRook;
    public SO_ChessPice whiteBishop;
    public SO_ChessPice whiteKing;
    public SO_ChessPice whiteKnight;
    public SO_ChessPice whitePawn;
    public SO_ChessPice whiteQueen;
    public SO_ChessPice whiteRook;

    public PiceType piceType;

    private GameObject[,] positions = new GameObject[1, 8];
    public GameObject chesspiece;

    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

  //  public void SetBlackTeam()
  //  {
  //      
  //      playerBlack = new GameObject[]{
  //          Create(blackBishop.GetName();,0,7), Create("black_knight",1,7), Create("black_bishop",2,7), Create("black_queen",3,7),
  //          Create("black_king",4,7), Create("black_bishop",5,7), Create("black_knight",6,7), Create("black_rook",7,7),
  //          Create("black_pawn",4,6), Create("black_pawn",5,6), Create("black_pawn",6,6), Create("black_pawn",7,6),
  //          Create("black_pawn",0,6), Create("black_pawn",1,6), Create("black_pawn",2,6), Create("black_pawn",3,6),
  //      };
  //  }
  //
  //      public void SetWhiteTeam()
  //      {
  //         
  //      }
  //
  //  public GameObject Create(string name, int x, int y)
  //  {
  //      GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
  //      Chessman cm = obj.GetComponent<Chessman>();
  //      cm.name = name;
  //      cm.SetXBoard(x);
  //      cm.SetYBoard(y);
  //      cm.Activate();
  //      return obj;
  //  }
  // 
  //  public void SetPosition(SO_ChessPice pice)
  //  {
  //      Chessman cm = pice.GetName();
  // 
  //      positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
  //  }
} //
