using UnityEngine;

public class Unfortunate : MonoBehaviour
{
     [SerializeField] GameObject Crytp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
         if (Crytp && -379f >this.transform.position.y)
         {
              Instantiate(Crytp, this.transform.position + this.transform.right * 100, Quaternion.Euler(0,0,0));
              if (this.gameObject.GetComponent<Rigidbody2D>()) Destroy(this.gameObject.GetComponent<Rigidbody2D>());
              Destroy(this);
         }
    }
}
