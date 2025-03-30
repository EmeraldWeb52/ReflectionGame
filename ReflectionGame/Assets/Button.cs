using UnityEngine;
using UnityEngine.Events;
public class Button : MonoBehaviour
{
    UnityEvent UE;
    void OnCollisionEnter2D(Collision2D coll)
    {
         if (coll.gameObject.GetComponent<Rigidbody2D>()) UE.Invoke();
    }
}
