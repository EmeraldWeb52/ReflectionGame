using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Laser : MonoBehaviour
{
    [SerializeField] List<string> reflectables;
    [SerializeField] bool doIndependent;
    public static string LaserDetectorTag;
    public static string OmniButtonTag;
    public static string ExplosiveConversionTag;
    public LineRenderer lineRen;
    public Transform firePoint;
    public const int maxReflections = 20; // safeguard
    public const float StrongLaserCutRange = 0.75f;

    public bool disabled;
    bool dontHideLineRend_flag;
    public float timing;
    delegate void CarriedFunctionality(GameObject With,Vector2 activity);
    void Start()
    {
        lineRen.enabled = true;
        lineRen.useWorldSpace = true;
        //Makes sure there's only one rerefresh (atleast supposed to)
        StopCoroutine(ReRefresh());
        StartCoroutine(ReRefresh());
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.Space) || doIndependent)
        {
            DrawLaser();
            lineRen.enabled = true;
        }
        else if (!dontHideLineRend_flag)
        {
             lineRen.enabled = false;
        }
    }
    public void FiveSecondsOfShooting()
    {
         StartCoroutine(DrawLaserForSeconds(5f));
    }
    public void DisableLaserFor3Seconds() => StartCoroutine(DisableLaserForSeconds(3f));
    public void DisableLaserFor5Seconds() => StartCoroutine(DisableLaserForSeconds(5f));
    public void DisableLaserFor10Seconds() => StartCoroutine(DisableLaserForSeconds(10));
    IEnumerator DrawLaserForSeconds(float seconds)
    {
         timing = Time.time;
         if (dontHideLineRend_flag) yield break;
         dontHideLineRend_flag = true;
          lineRen.enabled = true;
         for (; Time.time <= timing + seconds;)
         {
              DrawLaser();
              yield return null;
         }
         Debug.Log("Done");
         dontHideLineRend_flag = false;
    }

    IEnumerator DisableLaserForSeconds(float seconds)
    {
         disabled = true;
         yield return new WaitForSeconds(seconds);
         disabled = false;
         yield break;
    }
    public void DrawLaser()
    {
         if (disabled) return;
         Vector2 start = firePoint.position;
         Vector2 dir = firePoint.right;
        //list of points(start point and all reflections in order)
        List<Vector3> points = new List<Vector3>();
        points.Clear();
        points.Add(start);

        //OmniButton based variables
        bool Stronglaser = false;  /* for--->>*/
        bool explosionLaser = false;
        bool truedExplosionLaser = false;
        int stupidReflects = 0;
        bool soonToEnd;

     //   short DebugInt = 0;
        //makes sure it only reflect < maxReflections timse
        for (int i = 0; i < maxReflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(start, dir);
            if (hit.collider != null)
            {
                 if (truedExplosionLaser && explosionLaser)
                 {
                      Instantiate(LaserConvers.stExplosion, hit.point, Quaternion.Euler(0,0,0));
                      foreach (Collider2D explodee in Physics2D.OverlapCircleAll(hit.point, 7))
                      {
                           if (explodee.gameObject.GetComponent<Breakable>())
                           {
                                Destroy(explodee.gameObject, 5);
                                if (!explodee.gameObject.GetComponent<Rigidbody2D>()) explodee.gameObject.AddComponent<Rigidbody2D>().AddForce(((Vector2)hit.collider.transform.position - hit.point).normalized * 30);
                                Destroy(explodee);
                                continue;
                           }
                           if (explodee.tag == "Player")
                           {
                                //Gameovering script
                           }
                      }
                 }
                //adds reflection point to list
                points.Add(hit.point);
                //if object is reflectable, set the points to the new data to draw the next segment of the line
                if (reflectables.Contains(hit.collider.tag))
                {
                    dir = Vector2.Reflect(dir, hit.normal);
                    start = hit.point + dir * 0.1f;
          //          DebugInt++;
                    continue;
                }

                if (Stronglaser)
                {
                     Debug.DrawRay(hit.point + dir.normalized / 100, dir);
                     Collider2D coll = hit.collider;
                   RaycastHit2D raycast1 = Physics2D.Raycast(hit.point + dir.normalized * StrongLaserCutRange, -dir, StrongLaserCutRange);
                   if (Vector2.Distance(hit.point, raycast1.point) < StrongLaserCutRange)
                   {
                        points.Add(raycast1.point);
                        start = raycast1.point + dir.normalized / 10;
                        continue;
                   }
                   else
                   {
                        break;
                   }

                }
                //Laser detector
                if (LaserDetectorTag != "" && hit.collider.gameObject.tag == LaserDetectorTag && hit.collider.gameObject.GetComponent<LightSensor>())
                {
                     hit.collider.gameObject.GetComponent<LightSensor>().SetTurnedOn();
          //           DebugInt++;
                     break;
                }
                //Omnibutton detection
                if (OmniButtonTag != "" && hit.collider.gameObject.tag == OmniButtonTag && hit.collider.gameObject.GetComponent<OmniButton>())
                {
          //           DebugInt++;
                     OmniButton omni = hit.collider.gameObject.GetComponent<OmniButton>();
                     omni.SetTurnedOn();
                     if (omni.CutDanger)
                     {
                        Stronglaser = true;
                     }
                     if (omni.Explosiv)
                     {
                          explosionLaser = true;
                     }
                     if (omni.Reflectionableing)
                     {
                          stupidReflects += 2;
                     }
                     if (omni.Reflects)
                     {
                          dir = Vector2.Reflect(dir, hit.normal);
                          start = hit.point + dir * 0.1f;
                          continue;
                     }
                     else soonToEnd = true;
                }

                if (ExplosiveConversionTag != "" && hit.collider.gameObject.tag == ExplosiveConversionTag && hit.collider.gameObject.GetComponent<LaserConvers>() && explosionLaser)
                {
                      start = hit.collider.gameObject.GetComponent<LaserConvers>().CollisionHasHappened(this, hit.point, dir);
                      truedExplosionLaser = true;
                      continue;
                }

                if (stupidReflects > 0)
                {
                     dir = Vector2.Reflect(dir, hit.normal);
                     start = hit.point + dir * 0.1f;
                     stupidReflects--;
               //      DebugInt++;
                     continue;
                }
                break;
            }

            //if laser hits nothing, only go 100 units
            else
            {
                points.Add(start + dir * 100f);
                break;
            }
        }
       // Debug.Log("Loop does " + DebugInt + " times lasering");
        //renders the line
        lineRen.positionCount = points.Count;
        lineRen.SetPositions(points.ToArray());
    }
    //Refreshes Lightsensor when thing happens
    // Currently Getkey, PLEASE CHANGER
    public static IEnumerator ReRefresh()
    {
         for (;;)
         {
             yield return new WaitUntil(() => !Input.GetKey(KeyCode.Space));
             ButtonManagement.Refresh();
             yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));
         }
    }

    //Draws to Gizmos because MANUALLY having to see where to put the mirror, is annoying
    void OnDrawGizmos()
    {
         Vector2 start = firePoint.position;
         Vector2 dir = firePoint.right;
            //list of points(start point and all reflections in order)
            List<Vector3> points = new List<Vector3>();
            points.Clear();
            points.Add(start);


            //makes sure it only reflect <100 timse
            for (int i = 0; i < maxReflections; i++)
            {
                 RaycastHit2D hit = Physics2D.Raycast(start, dir);
                 if (hit.collider != null)
                 {
                     //adds reflection point to list
                     points.Add(hit.point);
                     //if object is reflectable, set the points to the new data to draw the next segment of the line
                     if (reflectables.Contains(hit.collider.tag))
                     {
                         dir = Vector2.Reflect(dir, hit.normal);
                         start = hit.point + dir * 0.05f;
                     }
                     else break;
                 }

                 //if laser hits nothing, only go 100 units
                 else
                 {
                     points.Add(start + dir * 100f);
                     break;
                 }
            }
            //renders the line
            lineRen.positionCount = points.Count;
            lineRen.SetPositions(points.ToArray());

    }
}
