using UnityEngine;

public class EndInSeconds : MonoBehaviour
{

     [SerializeField] float seconds = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, seconds);
    }

}
