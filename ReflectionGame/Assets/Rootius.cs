using UnityEngine;

public class Rootius : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (this.gameObject.GetComponent<AudioSource>())
        {
             this.gameObject.GetComponent<AudioSource>().volume *= this.transform.root.localScale.y;
        }
    }
}
