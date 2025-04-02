using UnityEngine;

public class LaserConvers : MonoBehaviour
{
     [Header("Laser")]
     public static AudioClip stLaserConversionSound;
     [SerializeField] AudioClip laserConversionSound;
     [SerializeField] GameObject LaserTrail;
     void Start()
     {
          Laser.ExplosiveConversionTag = this.gameObject.tag;
          if (laserConversionSound && !stLaserConversionSound) stLaserConversionSound = laserConversionSound;
     }


    public Vector2 CollisionHasHappened(Laser lasr ,Vector2 position, Vector2 direction)
    {
         Vector2 meinPOsition = (Vector2)this.transform.position;
         Destroy(this.gameObject.AddComponent<AudioSource>(), stLaserConversionSound.length * 1.1f);
         this.gameObject.GetComponent<AudioSource>().clip = stLaserConversionSound;
         this.gameObject.GetComponent<AudioSource>().Play();
         Vector2 distance = meinPOsition - position;
         Vector2 result = position + direction * (2.8f + Mathf.Tan(Vector2.Angle(new Vector2(Mathf.Sign(direction.x), 0), direction)));
         if (LaserTrail) Instantiate(LaserTrail, result, Quaternion.Euler(0,0,Mathf.Sign(direction.x) * Vector2.Angle(direction.normalized, new Vector2(0,1))));
         if (lasr) lasr.DisableLaserFor3Seconds();
         return result;
    }
}
