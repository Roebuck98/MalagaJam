using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CharacterHealth : MonoBehaviour
{
    public float Health = 100;
    public bool IsPlayer = false;
    public float BurnTime = 1f;
    public float BurnRate = 0.01f;
    public float GameOverTime = 4f;
    public GameEnding Death;
    public bool player1;
    
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if(IsPlayer)
        {
            if (player1)
                MainSceneUiManager.instance.player1HpBar.fillAmount = Health / 100f;
            else
            {
                MainSceneUiManager.instance.player2HpBar.fillAmount = Health / 100f;
            }
        }
        if(Health <= 0)
        {
            if (IsPlayer)
            {
                //StartCoroutine(DieCharacter());
                Death.CaughtPlayer();
            }
            else
            {
                StartCoroutine(DieEnemy());
            }
        }
    }


    IEnumerator DieCharacter()
    {
        float burnValue = 0.7f;

        while (burnValue >= 0)
        {
            var material = GetComponentInChildren<Renderer>().material;
            material?.SetFloat("_BurnValue", burnValue);
            burnValue -= BurnRate;
            yield return new WaitForSeconds(BurnRate * BurnTime);
        }

        

        
    }

    IEnumerator DieEnemy()
    {
        float burnValue = 0.7f;

        while (burnValue >= 0)
        {
            var material = GetComponentInChildren<Renderer>().material;
            material?.SetFloat("_BurnValue", burnValue);
            burnValue -= BurnRate;
            yield return new WaitForSeconds(BurnRate * BurnTime);
        }
        Destroy(gameObject);
    }
}
