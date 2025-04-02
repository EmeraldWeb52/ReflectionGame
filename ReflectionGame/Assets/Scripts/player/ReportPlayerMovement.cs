using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerMovementReporter : MonoBehaviour
{
     Vector2 lastPost;
     [SerializeField] float CheckingInterval;
     [SerializeField] float distanceToReport;
     void Start()
     {
          StartCoroutine(CheckInt());
     }


     IEnumerator CheckInt()
     {
          while (true)
          {
               if (Vector2.Distance(this.transform.position, lastPost) > distanceToReport)
               {
                    //Light sensor is false, it's a refresh for all
                    ButtonManagement.Refresh();
                    lastPost = this.transform.position;
               }
               yield return new WaitForSeconds(0.3f);
          }
     }

     void OnCollisionStay2D(Collision2D coll)
     {
          if (coll.gameObject.GetComponent<OmniButton>()) coll.gameObject.GetComponent<OmniButton>().ON = true;
     }

     void OnCollisionEnter2D(Collider2D coll)
     {
          if (coll.gameObject.GetComponent<OmniButton>())
          {
               ButtonManagement.omniButtonClicked(this.transform.position);
          }
          if (coll.gameObject.GetComponent<Button>())
          {
               ButtonManagement.buttonClicked(this.transform.position);
          }
     }
}
