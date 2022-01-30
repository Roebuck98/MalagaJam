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
    public GameObject GameOverCanvas;
    public bool player1;
    
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if(IsPlayer)
        {
            if (player1)
                MainSceneUiManager.instance.player1HpBar.value = Health / 100f;
            else
            {
                MainSceneUiManager.instance.player2HpBar.value = Health / 100f;
            }
        }
        if(Health <= 0)
        {
            if (IsPlayer)
            {
               StartCoroutine(DieCharacter());
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

        float currentTime = Time.time;
        GameOverCanvas.SetActive(true);
        float deathTime = Time.time;
        while (currentTime < deathTime+ GameOverTime)
        {
            currentTime = Time.time;
            yield return new WaitForSeconds(0.1f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
