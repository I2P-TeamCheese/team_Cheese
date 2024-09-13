using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [TextArea(1, 2)]
    public  string[] contents;
    public  Sprite[] sprites;
}
