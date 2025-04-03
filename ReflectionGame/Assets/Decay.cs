using UnityEngine;

public class Decay : MonoBehaviour
{
     float decaySpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         decaySpeed = transform.localScale.y / 30;
    }

    // Update is called once per frame
    void Update()
    {
         transform.localScale -= Vector3.up * (decaySpeed * Time.deltaTime);
         if (0 > transform.localScale.y)
         {
              Destroy(this.gameObject);
         }
    }
}
