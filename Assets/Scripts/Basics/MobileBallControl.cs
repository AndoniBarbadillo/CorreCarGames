using UnityEngine;

public class MobileBallControl : MonoBehaviour
{
    private Vector2 touchStartPos;
    private bool isDragging = false;

    private Rigidbody rb;

    // Velocidad de movimiento de la pelota en X
    public float moveSpeed = 5.0f;
    // Velocidad constante hacia adelante en Z
    public float forwardSpeed = 5.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Movimiento constante hacia adelante en Z
        rb.velocity = new Vector3(0, 0, forwardSpeed);

        // Manejar el movimiento horizontal de la pelota en función del arrastre táctil
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    isDragging = true;
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        Vector2 dragDelta = touch.position - touchStartPos;
                        float movement = dragDelta.x / Screen.width * moveSpeed;
                        rb.velocity = new Vector3(movement, rb.velocity.y, forwardSpeed);
                    }
                    break;

                case TouchPhase.Ended:
                    isDragging = false;
                    rb.velocity = new Vector3(0, 0, forwardSpeed);
                    break;
            }
        }
        else
        {
            // Si no hay toques, detener el movimiento horizontal
            rb.velocity = new Vector3(0, 0, forwardSpeed);
        }
    }
}




