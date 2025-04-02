using UnityEngine;

public class Moveme : MonoBehaviour
{
     void Start() => Destroy(this.gameObject, 1);
    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<Rigidbody2D>()) this.gameObject.GetComponent<Rigidbody2D>().AddForce(this.transform.right * Time.deltaTime * 90000);
    }
}
