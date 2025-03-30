using UnityEngine;

public class tempDebug : MonoBehaviour
{
    public KeyCode a;
    public KeyCode b;

    public float rotSpeed;
    void Update()
    {
        if (Input.GetKey(a))
        {
            transform.Rotate(0, 0, rotSpeed);
        }
        if (Input.GetKey(b))
        {
            transform.Rotate(0, 0, -rotSpeed);
        }

    }
}
