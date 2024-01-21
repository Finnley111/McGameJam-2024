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

        if (SceneManager.GetActiveScene().buildIndex == 2){
                GameManager.wireAmountLeft = 10f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4){
            GameManager.wireAmountLeft = 7.5f;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 6){
            GameManager.wireAmountLeft = 5.5f;
        }
    }
}
