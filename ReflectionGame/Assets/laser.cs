using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] List<string> reflectables;
    public static string LaserDetectorTag;
    public LineRenderer lineRen;
    public Transform firePoint;
    public int maxReflections = 20; // safeguard

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
            DrawLaser(firePoint.position, firePoint.right);
            lineRen.enabled = true;
        }
        else
        {
            lineRen.enabled = false;
        }
    }

    void DrawLaser(Vector2 start, Vector2 dir)
    {
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

                else
                {
                    if (LaserDetectorTag != "" && hit.collider.gameObject.tag == LaserDetectorTag && hit.collider.gameObject.GetComponent<LightSensor>())
                    {
                         hit.collider.gameObject.GetComponent<LightSensor>().SetTurnedOn();
                    }
                    break;
                    //add code for hitting non reflectable here
                }
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
