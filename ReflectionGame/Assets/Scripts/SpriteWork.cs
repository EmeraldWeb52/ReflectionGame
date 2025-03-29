using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu]
public class SpriteWork : ScriptableObject
{
    public List<Sprite> sprites;

    public bool Legality(int Index)
    {
         if (sprites.Count >= (Index + 1))
               return sprites[Index] != null;
         else
               return false;
    }
}
