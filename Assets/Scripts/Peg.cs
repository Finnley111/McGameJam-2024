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

    private void OnTriggerStay2D() {
        Debug.Log("I am being hit");
    }
}
