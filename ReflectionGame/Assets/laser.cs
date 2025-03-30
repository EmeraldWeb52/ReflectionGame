using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] List<string> reflectables;
    public LineRenderer lineRen;
    public Transform firePoint;
    public int maxReflections = 20; // safeguard

    void Start()
    {
        lineRen.enabled = true;
        lineRen.useWorldSpace = true;
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
                    start = hit.point + dir * 0.1f;
                }
                
                else
                {
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
}
