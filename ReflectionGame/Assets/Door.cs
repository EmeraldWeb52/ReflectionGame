using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
     [SerializeField] private Vector2 Position;

     void Start()
     {

     }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.transform.gameObject.tag == "Player")
         {
             collision.transform.position = Position;
         }
    }

    public void Open()
    {
         if (this.GetComponent<Collider2D>()) this.GetComponent<Collider2D>().enabled = true;
    }

    public void Close()
    {
         if (this.GetComponent<Collider2D>()) this.GetComponent<Collider2D>().enabled = false;
    }
}
