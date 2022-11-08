using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Content {
    public string ContentName;
    public Sprite FrontImage;
    public Sprite BackImage;
    public string ContentText;
}

[CreateAssetMenu]
public class ScrollViewContentData : ScriptableObject{
    public List<Content> ContentList;
}
