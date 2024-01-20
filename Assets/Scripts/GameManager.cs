using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Dictionary<Vector2, Peg> AllPoints = new Dictionary<Vector2, Peg>();

    public void Awake() {
        AllPoints.Clear();
    }

    [ContextMenu("Test Gameplay")]
    public void Play() {
        Time.timeScale = 1;
    }

}
