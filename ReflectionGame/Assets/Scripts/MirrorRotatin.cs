using UnityEngine;
public class MirrorRotatin : MonoBehaviour
{
    public void RotateCertAmount(float amount) => this.transform.Rotate(0,0,amount);
}
