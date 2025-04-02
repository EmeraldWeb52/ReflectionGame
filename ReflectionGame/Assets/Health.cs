using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
     [SerializeField] Image Fillable;

     [SerializeField] PlayerHealth playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
         if (Fillable && playerHealth)
               Fillable.fillAmount = Mathf.Clamp((float)playerHealth.Health / (float)playerHealth.maxHealth, 0, 1);
    }
}
