using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterHealth : MonoBehaviour
{
    public int Health = 100;
    public bool IsPlayer = false;
    
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            if (IsPlayer)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
