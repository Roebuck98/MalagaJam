using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public static CameraTarget instance;
    private Vector2 pos;
    public Transform player1Pos;
    public Transform player2Pos;
    public float Distance;
    private void Awake()
    {
        instance = this;
        Distance = 5f;
    }
    private void Update()
    {
        if(player1Pos && player2Pos)
        {
            Vector2 newPos = (player1Pos.position + player2Pos.position) * 0.5f;
            transform.position = newPos;
            Distance = Vector2.Distance(player1Pos.position, player2Pos.position);
        }
    }
}
