using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour
{
    private int points = 0;

    [SerializeField] private Text pointsText;

    public int getPoints()
    {
        return points;
    }

    public void setPoints(int n)
    {
        points = n;
    }

    public void UpdatePoints()
    {
        pointsText.text = "Points: " + points;
    }

}
