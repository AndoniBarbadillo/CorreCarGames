using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointManager : MonoBehaviour
{
    public Text textoPuntuacion;
    private int puntuacion = 0;
    [SerializeField] GameObject Ball;
    public bool StopRunning;
    public float DistanciadeWin;
    Vector3 PosiciondeObjeto;
    Vector3 PosicionObjetoPuntos;
    private float startTime;
    private Quaternion rotacionInicialCamara;
    public Transform cameraTransform;
    public float rotacionXMaxima = 45f;
    public float velocidadRotacionCamara = 10f;

    public float interpolationDuration = 1f; 
    private float initialTimeScale; 
    private float interpolationStartTime; 
    private bool isInterpolating = false;

    [SerializeField] GameObject RetryCanvas;
    public Text textoPuntuacionFinal;
    [SerializeField] GameObject MainCanvas;
    [SerializeField] GameObject MenuCanvas;
   // [SerializeField] GameObject AdsCanvas;
    [SerializeField] AudioSource Song;
    

    void Start()
    {
        ActualizarPuntuacion();
        StopRunning = false;
        rotacionInicialCamara = cameraTransform.localRotation;
        Time.timeScale = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CuboRojo"))
        {
            RestarPunto();
        }
        else if (other.CompareTag("CuboAzul"))
        {
            SumarPunto();
        }
        else if (other.CompareTag("Duplicador4x"))
        {
            Duplicar4x();
        }
        else if (other.CompareTag("Divisor2x"))
        {
            Divisor2x();
        }

        if (other.CompareTag("WinPlate"))
        {
            int PuntosObtenidos = GetPoints();
            DistanciadeWin = PuntosObtenidos * 1f;
            PosiciondeObjeto = Ball.transform.position;
            PosicionObjetoPuntos = new Vector3(Ball.transform.position.x, Ball.transform.position.y, Ball.transform.position.z + DistanciadeWin);
            StopRunning = true;
            startTime = Time.time;
           
            MainCanvas.SetActive(false);
          //  AdsCanvas.SetActive(true);
        }
    }

    private int GetPoints()
    {
        return puntuacion;
    }

    public void SumarPunto()
    {
        puntuacion++;
        ActualizarPuntuacion();
    }
    public void Duplicar4x()
    {
        puntuacion *= 4;
        ActualizarPuntuacion();
    }
    public void Divisor2x()
    {
        puntuacion /= 2;
        ActualizarPuntuacion();
    }


    public void RestarPunto()
    {
        puntuacion--;
        ActualizarPuntuacion();
    }

    void ActualizarPuntuacion()
    {
        textoPuntuacion.text = "Puntuazioa: " + puntuacion;
    }
    void ActualizarPuntuacionFinal()
    {
        textoPuntuacionFinal.text = "Puntuazioa: " + puntuacion;
    }

    void ShowRetryCanvas()
    {
        GetPoints();
        ActualizarPuntuacionFinal();
        RetryCanvas.SetActive(true);
     //   AdsCanvas.SetActive(true);
    }

    public void GoToScene(string Scene)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(Scene);
        Song.Stop();
    }
    public void PlayButton()
    {
        Time.timeScale = 1;
        MenuCanvas.SetActive(false);
        MainCanvas.SetActive(true);
        Song.Play();

    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }
    void Update()
    {

        if (puntuacion < 0f && !isInterpolating)
        {
            
            initialTimeScale = Time.timeScale;
            interpolationStartTime = 1;
            isInterpolating = true;
            startTime = Time.time;
            MainCanvas.SetActive(false);
            ShowRetryCanvas();
            
        }

        if (isInterpolating)
        {
            float elapsed = Time.time - interpolationStartTime;
            float t = Mathf.Clamp01(elapsed / interpolationDuration); 
            Time.timeScale = Mathf.Lerp(initialTimeScale, 0f, t);

            
            if (t >= 1.0f)
            {
                isInterpolating = false;
            }
        }

        if (StopRunning)
        {
            Invoke("ShowRetryCanvas", 2.5f);

            float elapsedTime = Time.time - startTime;
            float journeyLength = Vector3.Distance(PosiciondeObjeto, PosicionObjetoPuntos);
            float journeyFraction = elapsedTime / 2f;

            Ball.transform.position = Vector3.Lerp(PosiciondeObjeto, PosicionObjetoPuntos, journeyFraction);

            if (journeyFraction >= 1.0f)
            {
                Ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }

            
            float camaraLerpFraction = elapsedTime / 2f; 
            float anguloRotacionX = Mathf.Lerp(0, rotacionXMaxima, camaraLerpFraction);
            Quaternion rotacionNuevaCamara = Quaternion.Euler(anguloRotacionX, 0, 0);
            cameraTransform.localRotation = Quaternion.Lerp(rotacionInicialCamara, rotacionNuevaCamara, camaraLerpFraction);
        }
    }
}
