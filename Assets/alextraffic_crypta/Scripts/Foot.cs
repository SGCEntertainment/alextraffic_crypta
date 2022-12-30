using UnityEngine;

public class Foot : MonoBehaviour
{
    private Vector2 InitPosition { get; set; }
    private Vector2 TargetPosition { get; set; }
    private Rigidbody2D Rigidbody { get; set; }

    private const float speed = 10.0f;

    private void Awake()
    {
        InitPosition = transform.position;
        Rigidbody = GetComponent<Rigidbody2D>();

        TargetPosition = InitPosition;
    }

    private void OnMouseDrag()
    {
        if(!GameManager.GameStarted)
        {
            return;
        }

        TargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        if (!GameManager.GameStarted)
        {
            return;
        }

        TargetPosition = InitPosition;
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = Vector2.MoveTowards(Rigidbody.position, TargetPosition, Time.deltaTime * speed);
        Rigidbody.MovePosition(newPosition);
    }
}
