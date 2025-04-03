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
     [SerializeField] ParticleSystem ps;

     [SerializeField] float range = 40f;
     [SerializeField] float shootSize = 0.4f;

     [SerializeField] int maxhitCons = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         boomAnim.SetTrigger("Boom");
         isReset = false;
         StartCoroutine(TriggerReset());
    }

    IEnumerator TriggerReset()
    {
         while (true)
         {
              if (boomAnim && !isReset)
              boomAnim.ResetTrigger("Boom");
              yield return new WaitForSeconds(0.07f);
         }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Shoot()
    {
          if (anim) anim.SetBool("Shot", true);
    }
    void Shootingness()
    {
         if (ps) ps.Play();
         isReset = false;
         if (boomAnim) boomAnim.SetTrigger("Boom");
         shooter.Play();
         RaycastHit2D[] rayhits = Physics2D.CircleCastAll(shootingPoint.transform.position, shootSize, shootingPoint.right, range);
         int i = maxhitCons;
         foreach (RaycastHit2D raycastHit in rayhits)
         {
              if (i > 0)
              {
                   GameObject explosion = Instantiate(Explosionir.stExplosion, raycastHit.centroid, Quaternion.Euler(0,0,0));
                   explosion.transform.localScale /= 2;
                   foreach (Transform child in explosion.transform)
                   {
                        if (child.gameObject.GetComponent<SpriteRenderer>())
                        {
                             child.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
                        }
                   }
                   Collider2D Victim = raycastHit.collider;
                   if (Victim.gameObject.GetComponent<PlayerHealth>()) StartCoroutine(epicDamage(Victim.gameObject.GetComponent<PlayerHealth>(), 50));
                   if (Victim.gameObject.GetComponent<Breakable>())
                   {
                        Instantiate(Explosionir.stExplosion, raycastHit.collider.transform.position, Quaternion.Euler(0,0,0)).transform.localScale /= 1.1f;
                        Victim.gameObject.GetComponent<Breakable>().Break(Vector2.zero);
                   }
                   if (!raycastHit.collider.gameObject.GetComponent<UltraLaserBreakable>())
                       i--;
              }
         }
          anim.SetBool("Shot", false);
    }
    IEnumerator epicDamage(PlayerHealth player, int damage)
    {
         Time.timeScale = 0.01f;
         yield return new WaitForSeconds(0.5f * Time.timeScale);
         player.TakeDamage(damage);
         Time.timeScale = 1f;
    }
    void OnDrawGizmos()
    {
         Gizmos.color = Color.red;
         Gizmos.DrawRay((Vector2)shootingPoint.position + (Vector2)shootingPoint.up * shootSize, shootingPoint.right * range);
         Gizmos.DrawRay((Vector2)shootingPoint.position + -(Vector2)shootingPoint.up * shootSize, shootingPoint.right * range);
    }
}
