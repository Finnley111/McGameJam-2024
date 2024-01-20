using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class WireCreator : MonoBehaviour, IPointerDownHandler
{
    bool WireCreationStarted = false;
    public Wire CurrentWire;
    public GameObject WireToInstantiate;
    public Transform wireParent;
    public Peg CurrentStartPoint;
    public Peg CurrentEndPoint;
    public GameObject PegToInstantiate;
    public Transform PegParent;



    public void OnPointerDown(PointerEventData eventData) {
        if (WireCreationStarted == false) {
            if (GameManager.AllPoints.ContainsKey(Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(eventData.position)))) {
                WireCreationStarted = true;
                StartWireCreation(Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(eventData.position)));
            }
        }
        else {
            if (eventData.button == PointerEventData.InputButton.Left) {
                if (GameManager.AllPoints.ContainsKey(CurrentEndPoint.transform.position)) {
                    FinishWireCreation();
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right) {
                WireCreationStarted = false;
                DeleteCurrentWire();
            }
        }
    }

    void StartWireCreation(Vector2 StartPosition) {
        CurrentWire = Instantiate(WireToInstantiate, wireParent).GetComponent<Wire>();
        CurrentWire.startPosition = StartPosition;

        if (GameManager.AllPoints.ContainsKey(StartPosition)) {
            CurrentStartPoint = GameManager.AllPoints[StartPosition];
        }
        else {
            CurrentStartPoint = Instantiate(PegToInstantiate, StartPosition, Quaternion.identity, PegParent).GetComponent<Peg>();
            GameManager.AllPoints.Add(StartPosition, CurrentStartPoint);
        }
        CurrentEndPoint = Instantiate(PegToInstantiate, StartPosition, Quaternion.identity, PegParent).GetComponent<Peg>();
    }

    void FinishWireCreation() {
        if (GameManager.AllPoints.ContainsKey(CurrentEndPoint.transform.position)) {
            Debug.Log("End Point Has Peg");
            Destroy(CurrentEndPoint.gameObject);
            CurrentEndPoint = GameManager.AllPoints[CurrentEndPoint.transform.position];
        }
        else {
            GameManager.AllPoints.Add(CurrentEndPoint.transform.position, CurrentEndPoint);
            Debug.Log("End Point Has No Peg");
        }

        CurrentStartPoint.ConnectedWires.Add(CurrentWire);
        CurrentEndPoint.ConnectedWires.Add(CurrentWire);
        StartWireCreation(CurrentEndPoint.transform.position);
    }

    void DeleteCurrentWire() {
        Destroy(CurrentWire.gameObject);
        Destroy(CurrentEndPoint.gameObject);
    }

    void Update() {
        if (WireCreationStarted) {
            Vector2 endPosition = (Vector2)Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Vector2 dir = endPosition - CurrentWire.startPosition;
            Vector2 clampedPos = CurrentWire.startPosition + Vector2.ClampMagnitude(dir, CurrentWire.maxLength);

            CurrentEndPoint.transform.position = (Vector2)Vector2Int.FloorToInt(clampedPos);
            CurrentEndPoint.pegID = CurrentEndPoint.transform.position;
            CurrentWire.UpdatingCreatingWire(CurrentEndPoint.transform.position);

        }
    }
}
