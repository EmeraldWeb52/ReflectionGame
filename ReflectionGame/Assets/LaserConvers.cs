using UnityEngine;

public class LaserConvers : MonoBehaviour
{
     [SerializeField] GameObject LaserTrail;

    public Vector2 CollisionHasHappened(Laser lasr ,Vector2 position, Vector2 direction)
    {
         Vector2 meinPOsition = (Vector2)this.transform.position;
         Vector2 distance = meinPOsition - position;
         Vector2 result = position + direction * (2 + Mathf.Tan(Vector2.Angle(new Vector2(Mathf.Sign(direction.x), 0), direction)));
         if (LaserTrail) Instantiate(LaserTrail, result, Quaternion.Euler(0,0,90 * -Vector2.Dot(direction, new Vector2(0,1))));
         return result;
    }
}
