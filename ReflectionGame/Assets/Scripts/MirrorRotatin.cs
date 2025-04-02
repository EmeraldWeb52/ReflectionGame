using System.Collections;
using UnityEngine;

public class MirrorRotatin : MonoBehaviour
{
    public void RotateCertAmount(float amount, float speed)
    {
        StartCoroutine(RotateOverTime(amount, speed));
    }

    IEnumerator RotateOverTime(float amount, float speed)
    {
        float rotated = 0;
        float direction = Mathf.Sign(amount); 
        while (Mathf.Abs(rotated) < Mathf.Abs(amount))
        {
            float rotationStep = direction * speed * Time.deltaTime;
            if (Mathf.Abs(rotated + rotationStep) > Mathf.Abs(amount))
                rotationStep = amount - rotated; 

            transform.Rotate(0, 0, rotationStep);
            rotated += rotationStep;
            yield return null;
        }
    }
}
