using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static Dictionary<Vector2, Peg> AllPoints = new Dictionary<Vector2, Peg>();
    public static Dictionary<string, bool> pegsActive = new Dictionary<string, bool>(){
	{"up", false},
	{"down", false},
	{"right", false},
    {"left", false},
    {"dash", false},
    {"start", false},
    {"belt", false},
    };

    public static float wireAmountLeft = 10;

    public void Awake() {
        AllPoints.Clear();
    }

    [ContextMenu("Test Gameplay")]
    public void Play() {
        Time.timeScale = 1;
    }

}
