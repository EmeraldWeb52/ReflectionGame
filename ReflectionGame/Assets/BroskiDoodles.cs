using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class BroskiDoodles : MonoBehaviour
{
     [SerializeField] TextMesh doodles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Away()
    {
         StartCoroutine(DESTRYOOOOOO());
    }

    // Update is called once per frame
    IEnumerator DESTRYOOOOOO()
    {
         if (doodles) doodles.text = "Broski Doodles";
         yield return new WaitForSeconds(1.5f);
         Destroy(this.gameObject);
    }
}
