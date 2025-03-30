using UnityEngine;

public class ButtonManagement : MonoBehaviour
{
     //Refresh for when ANYTHING IMPORTANT IS MOVED
     public static void Refresh()
     {
          Debug.Log("Refresh LOL");
           foreach (LightSensor lightSens in Object.FindObjectsByType<LightSensor>(FindObjectsSortMode.None))
           {
               if (lightSens.GetState()) lightSens.StartCoroutine(lightSens.RefreshConsequences());
           }
           foreach (OmniButton omni in Object.FindObjectsByType<OmniButton>(FindObjectsSortMode.None))
           {
               if (omni.ON) omni.StartCoroutine(omni.RefreshConsequences());
           }
     }
}
