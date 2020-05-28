using UnityEngine;


public class LogicaJugadorMouse : MonoBehaviour
{
public LayerMask mascMov;

    Camera cam;
	public GameObject camPos;
    MotorJugador motor;
    LevelManager levelManager;
    void Start()
    {
        cam = Camera.main;
        levelManager = LevelManager.instance;
        if (motor == null)
        {
            motor = gameObject.AddComponent<MotorJugador>();
        }
        else
        {
            motor = GetComponent<MotorJugador>();
        }

        cam.GetComponentInParent<CameraController>().SetTarget(gameObject);
    }

    void Update()
    {

        if (EstadosJuego.EstadoActual() == EstadosJuego.Estado.EXPLORAR)
        {
            CheckMousse();
            CheckPuerta();
            CheckSala();
        }
       
    }

   

    void CheckMousse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.collider.CompareTag("Muro"))
                {
                    motor.MoverAlPunto(hit.point);
                }

            }

        }
    }


    public void CheckPuerta()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2.0f))
        {
            if (hit.collider.tag == "Puerta")
            {
                //Debug.Log("pulsa espacio para continuar");
                if (Input.GetKey(KeyCode.Space))
                {
                    //Debug.Log("Estoy abriendo la puerta " + hit.collider.GetComponentInParent<Puerta>());
                    Puerta puerta = hit.collider.GetComponentInParent<Puerta>();
                    puerta.GetComponentInChildren<Animator>().SetBool("open", true);
                    levelManager.abrirPuerta(puerta);
                    
                }
            }
        }
    }

    void CheckSala()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2.0f))
        {
            if (hit.collider.tag == "Combate")
            {
                Debug.Log("combate");
                motor.MoverAlPunto(transform.position); //con esto lo dejamos quieto cuando se active el evento
                levelManager.activarCombate();
            }
        }
    }

}
