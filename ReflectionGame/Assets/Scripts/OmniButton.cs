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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         Laser.OmniButtonTag = this.gameObject.tag;
         //Only thing that can cause explosion

         for (int i = 0; 4 > i; i++)
         {
              if (Abil.Legality(i) && AbilitiesShow[i]) AbilitiesShow[i].sprite = Abil.sprites[i];
         }
         if (States.Legality((int)State) && StateShow) StateShow.sprite = States.sprites[(int)State];
         OmniColors();
         StartCoroutine(Checking());
         StartCoroutine(Trivia());
    }

    void OnDrawGizmosSelected()
    {
         if (States.Legality((int)State) && StateShow) StateShow.sprite = States.sprites[(int)State];
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
                   if (StateShow && UniEng.GetPersistentEventCount() > 0) StateShow.color /= 5;
                   yield return new WaitForSeconds(3f);
                   if (StateShow && UniEng.GetPersistentEventCount() > 0) StateShow.color = new Color(255,255,255,1);
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

    void OmniColors()
    {
         if (AbilitiesShow.Length < 4)
         {
              Debug.Log("Bad size");
         }
         if (AbilitiesShow[0])
              if (Reflects)
              {
                   AbilitiesShow[0].color = new Color(255,255,255,1);
              }
              else
              {
                   AbilitiesShow[0].color /= 5;
              }
         if (AbilitiesShow[1])
             if (CutDanger)
             {
                  AbilitiesShow[1].color = new Color(255,255,255,1);
             }
             else
             {
                  AbilitiesShow[1].color /= 5;
             }
        if (AbilitiesShow[2])
            if (Explosiv)
            {
                 AbilitiesShow[2].color = new Color(255,255,255,1);
            }
            else
            {
                 AbilitiesShow[2].color /= 5;
            }
       if (AbilitiesShow[3])
          if (Reflectionableing)
          {
               AbilitiesShow[3].color = new Color(255,255,255,1);
          }
          else
          {
               AbilitiesShow[3].color /= 5;
          }
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
                        if (!oneHas && omni.ON == state)
                        {
                             oneHas = true;
                             continue;
                        }
                        if (oneHas && omni.ON == state)
                        {

                             return false;
                        }
                   }
                   return true;
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
    //To make sure it's not foke
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
