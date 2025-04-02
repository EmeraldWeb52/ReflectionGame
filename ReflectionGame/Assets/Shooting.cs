using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Shooting : MonoBehaviour
{

     [SerializeField] AudioSource shooter;
     [SerializeField] Transform shootingPoint;
     bool isReset = true;
     [SerializeField] Animator anim;
     [SerializeField] Animator boomAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    IEnumerator TriggerReset()
    {
         while (true)
         {
              if (anim && !isReset) anim.ResetTrigger("Shot");
              if (boomAnim && !isReset) anim.ResetTrigger("Boom");
              yield return new WaitForSeconds(0.2f);
         }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
         isReset = false;
         if (anim) anim.SetTrigger("Shot");
         if (boomAnim) anim.SetTrigger("Boom");
         shooter.Play();
         RaycastHit2D[] rayhits = Physics2D.CircleCastAll(shootingPoint.transform.position, 0.4f, shootingPoint.right, 300);
         int i = 20;
         foreach (RaycastHit2D raycastHit in rayhits)
         {
              if (i > 0)
              {
                   GameObject explosion = Instantiate(LaserConvers.stExplosion, raycastHit.centroid, Quaternion.Euler(0,0,0));
                   foreach (Transform child in explosion.transform)
                   {
                        if (child.gameObject.GetComponent<SpriteRenderer>())
                        {
                             child.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
                        }
                   }
                   Collider2D Victim = raycastHit.collider;
                   if (Victim.gameObject.GetComponent<Breakable>())
                   {
                        Instantiate(LaserConvers.stExplosion, raycastHit.collider.transform.position, Quaternion.Euler(0,0,0)).transform.localScale /= 5;
                        Destroy(Victim.gameObject, 5);
                        if (!Victim.gameObject.GetComponent<Rigidbody2D>()) Victim.gameObject.AddComponent<Rigidbody2D>().AddForce(((Vector2)Victim.transform.position - raycastHit.point).normalized * 30);
                        Destroy(Victim);
                   }
                   if (!raycastHit.collider.gameObject.GetComponent<UltraLaserBreakable>())
                       i--;
              }
         }
    }
}
