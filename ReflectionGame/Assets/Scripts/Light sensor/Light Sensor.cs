using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.Events;
public class LightSensor : MonoBehaviour
{
     [SerializeField] SpriteRenderer sprRenderer;
     [SerializeField] SpriteWork sprites;
     [SerializeField] UnityEvent UE;
     bool state;
     bool proven;
     bool activationPerhaps;
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



     void Update()
     {
          if (activationPerhaps && state)
          {
               UE.Invoke();
               activationPerhaps = false;
          }
     }
     //Turns off the Sensor's light and changes sprite,
    public void SetTurnedOn()
    {
         state = true;
         proven = true;
         StopCoroutine("RefreshConsequences");
         if (1 < sprites.sprites.Count && sprRenderer)
         {
              if (sprites.Legality(1)) sprRenderer.sprite = sprites.sprites[1];
         }
    }


    //Turns off the Sensor's light and changes sprite,
    public void SetTurnedOff()
    {
         state = false;
         activationPerhaps = true;
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
         if (proven)
          {
               proven = false;
               yield break;
          }
         yield return new WaitForSeconds(0.5f);
         if (proven)
         {
               proven = false;
               yield break;
         }
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

}
