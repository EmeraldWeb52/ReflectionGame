using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int Health;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage){
        Health -= damage;
        if (Health <= 0){
            Destroy(gameObject);
        }
    }
}
