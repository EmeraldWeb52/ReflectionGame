using System;
using UnityEngine;

public class playerInput : MonoBehaviour
{
    [NonSerialized]public float horiMove;
    [NonSerialized] public bool doJump = false;
    void Update()
    {
        horiMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            doJump = true;
        }
    }

   
}
