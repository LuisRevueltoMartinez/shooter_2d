using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public GameObject punto_disparo;
    public GameObject proyectil;
    public GameObject proyectil2;
    public float velBala;
    public float couldown;
    float contadorCouldown;
    Vector3 distancia;
    GameObject bala;
    public float tiempoDisparo;
    float contador;
    public float planeHeight = 0f;
    Animator animator;
    AudioSource audio;
    public AudioClip hit;


    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (contadorCouldown <= 0f)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                calcularDistancia();
                this.GetComponent<Movimiento_Player>().enabled = false;
                cambiarVista();
                contador = tiempoDisparo;
                crearBala(proyectil);
                animator.SetBool("attack", true);
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                animator.SetBool("attack", true);
                this.GetComponent<Movimiento_Player>().enabled = false;
                if (contador <= 0)
                {
                    Destroy(bala);
                    crearBala(proyectil2);
                }
                else
                {
                    Destroy(bala);
                    crearBala(proyectil);
                    contador -= Time.deltaTime;
                }
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                animator.SetBool("attack", false);
                calcularDistancia();
                if (cambiarVista())
                {
                    Destroy(bala);
                    if (contador <= 0)
                    {
                        crearBala(proyectil2);
                    }
                    else
                    {
                        crearBala(proyectil);
                    }
                }
                shoot();
                this.GetComponent<Movimiento_Player>().enabled = true;
                contadorCouldown = couldown;
            }
        }
        else
        {
            contadorCouldown -= Time.deltaTime;
        }
    }

    void crearBala(GameObject objeto)
    {
        bala = Instantiate(objeto, punto_disparo.transform.position, Quaternion.identity); //Se crea el proyectil
    }

    void shoot()
    {
        bala.GetComponent<Rigidbody>().velocity = distancia.normalized * velBala; //Se le da velocidad para que se mueva
        audio.clip = hit;
        audio.Play();
        Destroy(bala, 4);
    }

    void calcularDistancia()
    {
        Vector3 clickPos = Input.mousePosition; //Cojo la posición del ratón
        clickPos.z = Camera.main.nearClipPlane; //Actualizamos el valor de la coordenada z al plano más cercano de la camara para que el rayo se cree correctamente

        Ray ray = Camera.main.ScreenPointToRay(clickPos); //Lanzamos un rayo desde la cámara(Fixeada) hasta el punto donde se ha hecho clic

        float distance = (planeHeight - ray.origin.z) / ray.direction.z; //Se calcula la distancia que hay desde el plano que se quiere hacer clic hasta el origen del rayo
        Vector3 intersection = ray.origin + ray.direction * distance; //Se calcula el punto donde se encuentra

        distancia = intersection - punto_disparo.transform.position; //Se calcula la distancia desde el punto de disparo hasta la intersección
    }

    bool cambiarVista()
    {
        if (this.transform.localScale.x == -1 && distancia.x >= 1.1f)
        {
            this.transform.localScale = new Vector3(1, this.transform.localScale.y, 1);
            return true;
        }
        else
        {
            if (distancia.x <= -1.1)
            {
                this.GetComponent<BoxCollider>().enabled = false; //Desactivo temporalmente el BoxCollider para evitar el mensaje de advertencia
                this.transform.localScale = new Vector3(-1, this.transform.localScale.y, 1); // Cambio la escala del Player para darle la vuelta
                this.GetComponent<BoxCollider>().enabled = true; //Lo reactivo de nuevo
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
