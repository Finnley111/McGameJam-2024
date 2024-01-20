using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Peg : MonoBehaviour
{
    public bool Runtime = true;
    public List<Wire> ConnectedWires;
    public Vector2 pegID;

    void Start() {
        if (Runtime == false) {
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
}
