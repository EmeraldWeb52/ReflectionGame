using UnityEngine;

public class ButtonManagement : MonoBehaviour
{
     public static GameObject activat;
     [SerializeField] GameObject getReal;

     public static AudioClip stButtonClickSound;
     public static AudioClip stOmniButtonClickSound;

     [SerializeField] AudioClip buttonClickSound;
     [SerializeField] AudioClip omniButtonClickSound;
     void Start()
     {
          if (getReal && !activat) activat = getReal;
          if (buttonClickSound && !stButtonClickSound) stButtonClickSound = buttonClickSound;
          if (omniButtonClickSound && !stOmniButtonClickSound) stOmniButtonClickSound = omniButtonClickSound;
     }

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

     public static void buttonPressed(Vector2 happen)
     {
          if (activat) Instantiate(activat, happen, Quaternion.Euler(0,0,0));
     }

     public static void buttonClicked(Vector2 happen)
     {
          if (!stButtonClickSound)
          {
               Debug.Log("No static button click audio. not having a buttonManagement Gameobject or that without Audioclips causes this log, just so you know");
               return;
          }
          GameObject gameObject = new GameObject("ButtonAudio, comes from ButtonManagement ;)",typeof(AudioSource));
          gameObject.transform.position = happen;
          AudioSource audioski = gameObject.GetComponent<AudioSource>();
          audioski.clip = stButtonClickSound;
          audioski.Play();
          Destroy(gameObject, stButtonClickSound.length * 1.4f);
     }

     public static void omniButtonClicked(Vector2 happen)
     {
          //if wastes time, just go back
          if (!stOmniButtonClickSound)
          {
               Debug.Log("No static omni button audio click. not having a buttonManagement Gameobject or that without Audioclips causes this log, just so you know");
               return;
          }
          GameObject gameObject = new GameObject("OmniButtonAudio, comes from ButtonManagement ;)" ,typeof(AudioSource));
          gameObject.transform.position = happen;
          AudioSource audioski = gameObject.GetComponent<AudioSource>();
          audioski.clip = stOmniButtonClickSound;
          audioski.Play();
          Destroy(gameObject, stOmniButtonClickSound.length * 1.4f);
     }
}
