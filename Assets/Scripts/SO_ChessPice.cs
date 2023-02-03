using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PiceType
{
    BlackQueen,
    BlackKnight,
    BlackBishop,
    BlackKing,
    BlackRook,
    BlackPawn,
    WhiteQueen,
    WhiteKnight,
    WhiteBishop,
    WhiteKing,
    WhiteRook,
    WhitePawn
}

[CreateAssetMenu(fileName = "NewPice", menuName = "Chess/Piece", order = 100)]
public class SO_ChessPice : ScriptableObject
{
    public PiceType piceType;
    public Sprite sprite;
    public void Foo()
    {
        Debug.Log("patata");
    }

}
