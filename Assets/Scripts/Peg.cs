using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Peg : MonoBehaviour
{
    public bool Runtime = true;
    public List<Wire> ConnectedWires;
    public Vector2 pegID;
    public Rigidbody2D rbd; 
    public bool isPlug; 
    public bool isFinalPeg;
    public int connections = 0;
    public bool isActivated = false;
    public string pegType;
    

    void Start() {
        if (Runtime == false) {
            rbd.bodyType = RigidbodyType2D.Static;
            pegID = transform.position;
            if (GameManager.AllPoints.ContainsKey(pegID) == false) {
                GameManager.AllPoints.Add(pegID, this);
            }
        }
    }
    void Update()
    {
        if (Runtime == false)
        {
            if (transform.hasChanged == true)
            {
                transform.hasChanged = false;
                transform.position = Vector3Int.RoundToInt(transform.position);
            }
        }
    }

    private void OnTriggerEnter2D() {
        connections += 1;
        isActivated = true;
        GameManager.pegsActive[pegType] = true;

        if (isFinalPeg) {
            GameManager.pegsActive["start"] = true;
        }
    }

}
