using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public enum Statesing
{
     AND,
     OR,
     XOR,
     NOT
}
public class OmniButton : MonoBehaviour
{
     //STatics
     public static GameObject stExplosion;
     [Header("Behaviours")]
     public bool Reflects;
     public bool CutDanger;
     public bool Explosiv;
     public bool Reflectionableing;

     [Header("Functionality")]
     public bool ON;
     [SerializeField] UnityEvent UniEng;
     //References are for LOGIC
     public OmniButton[] References;
     public Statesing State;

     [Header("Necessary")]
     [SerializeField] SpriteWork Abil;
     [SerializeField] SpriteWork States;
     bool proven;

     [SerializeField] SpriteRenderer[] AbilitiesShow;
     [SerializeField] SpriteRenderer StateShow;
     [SerializeField] TextMesh textmesh;

     [Header("Kinda Necessary")]
     [SerializeField] GameObject explosion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         Laser.OmniButtonTag = this.gameObject.tag;
         //Only thing that can cause explosion
         if (explosion && stExplosion == null) stExplosion = explosion;
         for (int i = 0; 4 > i; i++)
         {
              if (Abil.Legality(i) && AbilitiesShow[i]) AbilitiesShow[i].sprite = Abil.sprites[i];
         }
         if (States.Legality((int)State) && StateShow) StateShow.sprite = States.sprites[(int)State];
         StartCoroutine(Checking());
         StartCoroutine(Trivia());
    }

    void Update()
    {
         if (textmesh) textmesh.text = "Current State: " + (ON ? "ON" : "OFF");
    }
    // Checking activates UnityEvent when The OmniButtons are true AND when it's on
    IEnumerator Checking()
    {
         for (;;)
         {
              if (GetCheck(true) && ON)
              {
                   UniEng.Invoke();
                   while (!(GetCheck(true) && ON))
                   {
                         yield return new WaitForSeconds(0.5f);
                   }

              }
              yield return new WaitForSeconds(0.1f);
         }
    }
    IEnumerator Trivia()
    {
         for (;;)
         {
              yield return new WaitForSeconds(3f);
              if (States.Legality((int)State) && StateShow) StateShow.sprite = States.sprites[(int)State];
         }
    }

    public void SetTurnedOn()
    {
         ON = true;
         proven = true;
    }

    public void SetTurnedOff()
    {
         ON = false;
    }


    public void Explosion(Vector2 position, GameObject affected)
    {
         if (stExplosion) Instantiate(stExplosion, position, Quaternion.Euler(0,0,0));
    }

    public bool GetCheck(bool state)
    {
         //Don't chain a NOT to another NOT
         switch (State)
         {
              //ONE is NOT the same as state,
              case Statesing.AND:
                    foreach (OmniButton omni in References)
                    {
                         if (!omni) continue;
                         Debug.Log("Checked OmniButton is " + (omni.ON ? "on" : "off"));
                         if (omni.ON != state)
                         {
                              return false;
                         }
                    }
                    return true;
              //ONE is true, then on
              case Statesing.OR:
                    foreach (OmniButton omni in References)
                    {
                         if (!omni) continue;
                         if (omni.ON == state)
                         {
                              return true;
                         }
                    }
                    return false;
              //EXCLUSIVITY
              case Statesing.XOR:
                    bool oneHas = false;
                   foreach (OmniButton omni in References)
                   {
                        if (!omni) continue;
                        if (!oneHas && omni.ON == state) oneHas = true;
                        if (oneHas && omni.ON == state)
                        {

                             return false;
                        }
                   }
                   return false;
              //opposite of first check
              case Statesing.NOT:
                    foreach (OmniButton omni in References)
                    {
                         if (omni) return omni.GetCheck(!state);
                    }
                    return true;
         }
         return false;
    }
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
         ON = false;
    }
}
