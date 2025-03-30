using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu]
public class SpriteWork : ScriptableObject
{
    public List<Sprite> sprites;

    //Checks if sprite's needed index is allowed and not null
    public bool Legality(uint Index)
    {
         if (sprites.Count >= (Index + 1) && sprites.Count != 0)
               return sprites[(int)Index] != null;
         else
               return false;
    }
    public bool Legality(int Index)
    {
         if (Index < 0) return false;
         if (sprites.Count >= (Index + 1) && sprites.Count != 0)
               return sprites[Index] != null;
         else
               return false;
    }
}
