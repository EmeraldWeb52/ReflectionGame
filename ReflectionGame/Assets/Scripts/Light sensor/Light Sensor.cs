using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightSensor : MonoBehaviour
{
     [SerializeField] SpriteRenderer sprRenderer;
     [SerializeField] SpriteWork sprites;

     bool state;
     //self explanatory, it's for at start
     void Start()
     {
          SetTurnedOff();
          if (Laser.LaserDetectorTag != this.gameObject.tag)
          {
               Laser.LaserDetectorTag = this.gameObject.tag;
          }
     }
     //this monobehaviour script is gonna be on the collider, so ideal


     //Turns off the Sensor's light and changes sprite,
    public void SetTurnedOn()
    {
         state = true;
         if (1 < sprites.sprites.Count && sprRenderer)
         {
              if (sprites.Legality(1)) sprRenderer.sprite = sprites.sprites[1];
         }
    }


    //Turns off the Sensor's light and changes sprite,
    public void SetTurnedOff()
    {
         state = false;
         if (1 < sprites.sprites.Count && sprRenderer)
         {
              if (sprites.Legality(0)) sprRenderer.sprite = sprites.sprites[0];
         }
    }
    //return state
    public bool GetState()
    {
         return state;
    }
    //IEnumerator for automatic shutoff
    public IEnumerator RefreshConsequences()
    {
         yield return new WaitForSeconds(0.5f);
         SetTurnedOff();
    }

    IEnumerator NukeCoroutine(string Ienum)
    {
         for (byte i = 0; 100 >= i; i++)
         {
              StopCoroutine(Ienum);
              yield return null;
         }
    }
    //Refresh for when ANYTHING IMPORTANT IS MOVED
    public static void Refresh()
    {
         foreach (LightSensor lightSens in Object.FindObjectsByType<LightSensor>(FindObjectsSortMode.None))
         {
              if (lightSens.GetState()) lightSens.StartCoroutine(lightSens.RefreshConsequences());
         }
    }
}
