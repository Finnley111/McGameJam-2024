using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResetLevel : MonoBehaviour
{
    void OnMouseDown(){
        Debug.Log("Sprite Clicked");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.pegsActive = new Dictionary<string, bool>(){
        {"up", false},
        {"down", false},
        {"right", false},
        {"left", false},
        {"dash", false},
        {"start", false},
        {"belt", false},
        };
    }
}
