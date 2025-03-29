using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu]
public class SpriteWork : ScriptableObject
{
    public List<Sprite> sprites;

    //Checks if sprite's needed index is allowed
    public bool Legality(uint Index)
    {
         if (sprites.Count >= (Index + 1) && sprites.Count != 0)
               return sprites[(int)Index] != null;
         else
               return false;
    }
}
