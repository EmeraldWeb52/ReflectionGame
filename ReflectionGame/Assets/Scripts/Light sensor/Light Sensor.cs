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
     }
     //this monobehaviour script is gonna be on the layered collider, so ideal


     //Turns off the Sensor's light and changes sprite,
    public void SetTurnedOn()
    {
         StopCoroutine(RefreshConsequences());
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
         yield return new WaitUntil(() => (new WaitForSeconds(0.5f) == null));
         SetTurnedOff();
    }
    //Refresh for when ANYTHING IMPORTANT IS MOVED
    public static void Refresh()
    {
         foreach (LightSensor lightSens in Object.FindObjectsByType<LightSensor>(FindObjectsSortMode.None))
         {
              lightSens.StartCoroutine(lightSens.RefreshConsequences());
         }
    }
}
