using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string GameState = "Ʃ�丮��";
    public string GameStatePrint;

    void Start()
    {
        GameState = "Ʃ�丮��";
    }

    void Update()
    {
        GameStatePrint = GameState;
    }
}