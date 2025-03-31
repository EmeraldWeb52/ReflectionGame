using UnityEngine;
using UnityEngine.Events;
public class Amazing : MonoBehaviour
{
     [SerializeField] UnityEvent UnitEvent;
    // Update is called once per frame
    void OnTriggerEnter2D()
    {
         UnitEvent.Invoke();
    }
}
