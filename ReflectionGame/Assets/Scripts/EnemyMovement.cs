using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolpoints;
    public float movespeed;
    public int PatrolDestination; 

    // Update is called once per frame
    void Update()
    {
        if(PatrolDestination == 0){
            transform.position = Vector2.MoveTowards(transform.position, patrolpoints[0].position, movespeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, patrolpoints[0].position) < .2f){
                transform.localScale = new Vector3(1, 1, 1);
                PatrolDestination = 1;
            }
        }
        if(PatrolDestination == 1){
            transform.position = Vector2.MoveTowards(transform.position, patrolpoints[1].position, movespeed * Time.deltaTime);
            if(Vector2.Distance(transform.position, patrolpoints[1].position) < .2f){
                transform.localScale = new Vector3(-1, 1, 1);
                PatrolDestination = 0;
            }
        }
    }
}
