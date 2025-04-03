using UnityEngine;

public class Breakable : MonoBehaviour
{
     public void Break(Vector2 pointTowards)
     {
          Destroy(this.gameObject, 5);
          if (!this.gameObject.GetComponent<Rigidbody2D>()) this.gameObject.AddComponent<Rigidbody2D>().AddForce(pointTowards != Vector2.zero ? pointTowards.normalized * 30 : Vector2.zero);
          Destroy(GetComponent<Collider2D>());
     }
}
