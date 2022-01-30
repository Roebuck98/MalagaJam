using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUiManager : MonoBehaviour
{
    public static MainSceneUiManager instance;
    public Image player1HpBar;
    public Image player2HpBar;
    public Image explosionBar;
    public Image bulletsBar;
    public Image swapButtonImage;
    private void Awake()
    {
        instance = this;
    }
    public void UpdateExplosionBar(float t)
    {
        explosionBar.fillAmount = t / 1;
        if(explosionBar.fillAmount >= 1)
        {
            swapButtonImage.enabled = true;
        }
        else
        {
            swapButtonImage.enabled = false;
        }
    }
    public void UpdateBulletBar()
    {
        bulletsBar.fillAmount = Attacker.bulletsToShoot / 10;
    }
}
