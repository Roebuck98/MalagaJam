using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CharacterHealth : MonoBehaviour
{
    public int Health = 100;
    public bool IsPlayer = false;
    public float BurnTime = 1f;
    public float BurnRate = 0.01f;
    public float GameOverTime = 4f;
    public GameObject GameOverCanvas;
    
    public void TakeDamage(int damage)
    {
        Health -= damage;
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


    IEnumerator BurnCharacter()
    {
        float burnValue = 0.7f;

        while(burnValue >= 0)
        {
            var material = GetComponentInChildren<Renderer>().material;
            material.SetFloat("_BurnValue", burnValue);
            burnValue -= BurnRate;
            yield return new WaitForSeconds(BurnRate * BurnTime);
        }
        Destroy(gameObject);
    }

    IEnumerator ShowGameOver()
    {
        float currentTime = Time.time;
        GameOverCanvas.SetActive(true);
        while (currentTime < Time.time + GameOverTime)
        {
            yield return new WaitForSeconds(0.1f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator DieCharacter()
    {
        float burnValue = 0.7f;

        while (burnValue >= 0)
        {
            var material = GetComponentInChildren<Renderer>().material;
            material.SetFloat("_BurnValue", burnValue);
            burnValue -= BurnRate;
            yield return new WaitForSeconds(BurnRate * BurnTime);
        }

        float currentTime = Time.time;
        GameOverCanvas.SetActive(true);
        while (currentTime < Time.time + GameOverTime)
        {
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
            material.SetFloat("_BurnValue", burnValue);
            burnValue -= BurnRate;
            yield return new WaitForSeconds(BurnRate * BurnTime);
        }
        Destroy(gameObject);
    }
}
