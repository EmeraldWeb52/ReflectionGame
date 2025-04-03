using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealthTracker : MonoBehaviour
{
     [SerializeField] PlayerHealth playerHeralth;
     [SerializeField] Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
         if (text && playerHeralth)
         {
              text.text = playerHeralth.Health.ToString() + " / " + playerHeralth.maxHealth.ToString();
         }
         //If you're rude
         if (Input.GetKey(KeyCode.Space))
         {
              Death();
         }
    }

    public void Death()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
