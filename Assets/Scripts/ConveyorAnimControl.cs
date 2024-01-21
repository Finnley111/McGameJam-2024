using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorAnimControl : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.pegsActive["belt"] && GameManager.pegsActive["start"]){
            GetComponent<Animator>().enabled = true;
        }
    }
}
