using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public static CameraTarget instance;
    private Vector2 pos;
    public Transform player1Pos;
    public Transform player2Pos;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        Vector2 newPos = (player1Pos.position - player2Pos.position) * 0.5f;
        transform.position = newPos;
    }
}
