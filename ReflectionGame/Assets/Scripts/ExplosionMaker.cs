using UnityEngine;

public class Explosionir : MonoBehaviour
{
     public static GameObject stExplosion;
     [SerializeField] GameObject explosion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         if (explosion && !stExplosion) stExplosion = explosion;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
