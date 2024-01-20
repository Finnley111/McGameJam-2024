using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wire : MonoBehaviour
{
    public Vector2 startPosition;
    public SpriteRenderer wireSpriteRenderer;
    public float maxLength;
    public BoxCollider2D boxCollider;
    public HingeJoint2D startJoint;
    public HingeJoint2D endJoint;
    // Start is called before the first frame update
    public void UpdatingCreatingWire(Vector2 ToPosition) {
        transform.position = (ToPosition + startPosition) / 2;

        Vector2 dir = ToPosition - startPosition;
        float angle = Vector2.SignedAngle(Vector2.right, dir);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        float length = dir.magnitude;
        wireSpriteRenderer.size = new Vector2(length, wireSpriteRenderer.size.y);
    }
}
