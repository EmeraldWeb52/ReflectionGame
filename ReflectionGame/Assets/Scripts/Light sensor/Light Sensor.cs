using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightSensor : MonoBehaviour
{
     [SerializeField] SpriteRenderer sprRenderer;
     [SerializeField] SpriteWork sprites;

     bool state;
     void Start()
     {
          SetTurnedOff();
     }
     //this monobehaviour script is gonna be on the layered collider, so ideal
    public void SetTurnedOn()
    {
         StopCoroutine(RefreshConsequences());
         state = true;
         if (1 < sprites.sprites.Count && sprRenderer)
         {
              if (sprites.Legality(1)) sprRenderer.sprite = sprites.sprites[1];
         }
    }



    public void SetTurnedOff()
    {
         state = false;
         if (1 < sprites.sprites.Count && sprRenderer)
         {
              if (sprites.Legality(0)) sprRenderer.sprite = sprites.sprites[0];
         }
    }
    public bool GetState()
    {
         return state;
    }
    public IEnumerator RefreshConsequences()
    {
         yield return new WaitUntil(() => (new WaitForSeconds(0.5f) == null));
         SetTurnedOff();
    }
    public static void Refresh()
    {
         foreach (LightSensor lightSens in Object.FindObjectsByType<LightSensor>(FindObjectsSortMode.None))
         {
              lightSens.StartCoroutine(lightSens.RefreshConsequences());
         }
    }
}
