using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ResourceManager
{
    public static Sprite GetSprite(string _path)
    {
        return Resources.Load<Sprite>($"Sprites/{_path}");
    }
    public static Sprite GetSpriteToAtlas(string _path,string _name)
    {
        return Resources.Load<SpriteAtlas>($"Atlas/{_path}").GetSprite(_name);
    }

    public static void UnloadAsset()
    {
        Resources.UnloadUnusedAssets();
    }
}
