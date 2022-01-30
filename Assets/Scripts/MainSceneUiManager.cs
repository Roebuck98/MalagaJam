using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUiManager : MonoBehaviour
{
    public static MainSceneUiManager instance;
    public Slider player1HpBar;
    public Slider player2HpBar;
    public Slider explosionBar;
    public Slider bulletsBar;

    private void Awake()
    {
        instance = this;
    }
    public void UpdateExplosionBar(float t)
    {
        explosionBar.value = t / 1;
    }
    public void UpdateBulletBar()
    {
        bulletsBar.value = Attacker.bulletsToShoot / 10;
    }
}
