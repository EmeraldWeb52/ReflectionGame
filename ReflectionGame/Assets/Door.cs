using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
     [SerializeField] private Vector2 Position;
    public bool isTeleporter;
     void Start()
     {

     }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.transform.gameObject.tag == "Player" && isTeleporter)
         {
             collision.transform.position = Position;
         }
    }

    public void Open()
    {
         gameObject.SetActive(false);
    }

    public void Close()
    {
        gameObject.SetActive(true);
    }
}
