using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
     [SerializeField] private string sceneToLoad;

     void Start()
     {

     }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.transform.gameObject.tag == "Player")
         {
          SceneManager.LoadScene(sceneToLoad);
         }
    }
}
