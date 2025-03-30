using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Laser : MonoBehaviour
{
    [SerializeField] List<string> reflectables;
    public static string LaserDetectorTag;
    public static string OmniButtonTag;
    public LineRenderer lineRen;
    public Transform firePoint;
    public const int maxReflections = 20; // safeguard
    public const float StrongLaserCutRange = 0.75f;
    delegate void CarriedFunctionality(Vector2 activity, GameObject Affected);
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
        if (Input.GetKey(KeyCode.Space))
        {
            DrawLaser();
            lineRen.enabled = true;
        }
        else
        {
            lineRen.enabled = false;
        }
    }

    public void DrawLaser()
    {
         Vector2 start = firePoint.position;
         Vector2 dir = firePoint.right;
        //list of points(start point and all reflections in order)
        List<Vector3> points = new List<Vector3>();
        points.Clear();
        points.Add(start);

        //OmniButton based variables
        bool Stronglaser = false;  /* for--->>*/ float distanceLeft = StrongLaserCutRange;
        bool explosionLaser = false;
        int stupidReflects = 0;


        short DebugInt = 0;
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
                    start = hit.point + dir * 0.1f;
                    DebugInt++;
                    continue;
                }

                if (Stronglaser)
                {
                   RaycastHit2D raycast1 = Physics2D.Raycast(start, dir, distanceLeft);
                   RaycastHit2D raycast2 = Physics2D.Raycast(raycast1.point + dir / 10, dir, distanceLeft - raycast1.distance);
                   Collider2D collider = raycast1.collider;
                   if (raycast2 == false && collider.GetType() != typeof(EdgeCollider2D)) break;
                   else
                   {
                        start = raycast2.point + dir / 10;
                        continue;
                   }
                }
                if (LaserDetectorTag != "" && hit.collider.gameObject.tag == LaserDetectorTag && hit.collider.gameObject.GetComponent<LightSensor>())
                {
                     hit.collider.gameObject.GetComponent<LightSensor>().SetTurnedOn();
                     DebugInt++;
                     break;
                }
                if (OmniButtonTag != "" && hit.collider.gameObject.tag == OmniButtonTag && hit.collider.gameObject.GetComponent<OmniButton>())
                {
                     OmniButton omni = hit.collider.gameObject.GetComponent<OmniButton>();
                     if (omni.Reflects)
                     {
                          dir = Vector2.Reflect(dir, hit.normal);
                          start = hit.point + dir * 0.1f;
                          DebugInt++;
                     }
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
                          stupidReflects++;
                     }
                     continue;
                }

                if (stupidReflects > 0)
                {
                     dir = Vector2.Reflect(dir, hit.normal);
                     start = hit.point + dir * 0.1f;
                     DebugInt++;
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
        Debug.Log("Loop does " + DebugInt + " times lasering");
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
             LightSensor.Refresh();
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
