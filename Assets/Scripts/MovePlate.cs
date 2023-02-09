using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    // Posicion del tablero
    int matrixX;
    int matrixY;

    // false= mover, true= atacar
    public bool attack = false;

    public void Start()
    {
        for(int x=0; x<8; x++)
        {
            for (int y=0; y<8; y++)
            {
                SetCoords(x, y);
            }
        }
        if (attack)
        {
            //Cambiar a rojo
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f,0.0f,0.0f,1.0f);
        }
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
        
        SetCoords(matrixX, matrixY);

    }

    //  public void OnMouseUp()
    //  {
    //      controller = GameObject.FindGameObjectWithTag("GameController");
    //
    //      if (attack)
    //      {
    //          GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
    //
    //          if (cp.name == "white_king") controller.GetComponent<Game>().Winner("black","white");
    //          if (cp.name == "black_king") controller.GetComponent<Game>().Winner("white","black");
    //
    //          Destroy(cp);
    //      }
    //
    //      
    //      if (reference.GetComponent<Chessman>().movimiento == "castling_large")
    //      {
    //          GameObject.Find("black_king").GetComponent<Chessman>().SetXBoard(2);
    //          GameObject.Find("black_king").GetComponent<Chessman>().SetYBoard(7);
    //          GameObject.Find("black_king").GetComponent<Chessman>().SetCoords();
    //          GameObject.Find("black_rook1").GetComponent<Chessman>().SetXBoard(3);
    //          GameObject.Find("black_rook1").GetComponent<Chessman>().SetYBoard(7);
    //          GameObject.Find("black_rook1").GetComponent<Chessman>().SetCoords();
    //          controller.GetComponent<Game>().SetPosition(reference);
    //          reference.GetComponent<Chessman>().moved = true;
    //
    //          controller.GetComponent<Game>().NextTurn();
    //
    //          reference.GetComponent<Chessman>().DestroyMovePlates();
    //      }    
    //      else
    //      {
    //          controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
    //          reference.GetComponent<Chessman>().GetYBoard());
    //
    //          reference.GetComponent<Chessman>().SetXBoard(matrixX);
    //          reference.GetComponent<Chessman>().SetYBoard(matrixY);
    //          reference.GetComponent<Chessman>().SetCoords();
    //
    //
    //          controller.GetComponent<Game>().SetPosition(reference);
    //          reference.GetComponent<Chessman>().moved = true;
    //
    //          controller.GetComponent<Game>().NextTurn();
    //
    //          reference.GetComponent<Chessman>().DestroyMovePlates();
    //      }
    //  }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;  
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference() 
    { 
        return reference;
    }
}
