using UnityEngine;

public class PlayerMobileMovement : MonoBehaviour
{
    public float forwardSpeed = 5.0f; // Velocidad de movimiento hacia adelante
    public float horizontalSpeed = 5.0f; // Velocidad de movimiento lateral

    private Vector3 touchStartPos;
    private Vector3 currentTouchPos;

    void Update()
    {
        // Mueve el personaje hacia adelante constantemente
        transform.Translate(Vector3.right * forwardSpeed * Time.deltaTime);

        // Comprueba si se ha tocado la pantalla
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Controla el movimiento solo en el eje horizontal (izquierda y derecha)
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                currentTouchPos = touch.position;
                float horizontalInput = (currentTouchPos.x - touchStartPos.x) / Screen.width;
                MoveCharacter(horizontalInput);
            }
        }
    }

    void MoveCharacter(float horizontalInput)
    {
        // Calcula el desplazamiento horizontal limitando la velocidad
        float horizontalMovement = horizontalInput * horizontalSpeed * Time.deltaTime;

        // Mueve el personaje solo en los ejes X e Y (horizontal)
        transform.Translate(new Vector3(horizontalMovement, 0, 0));
    }
}
