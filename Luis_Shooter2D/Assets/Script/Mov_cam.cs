using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_cam : MonoBehaviour
{
    public GameObject target; //Objetivo al que sigue la c�mara
    Vector3 posicion; //Posici�n de la c�mara con respecto el objetivo
    //Los siguientes valores son para obtener los l�mites de la c�mara
    public GameObject maxCam; //Objeto vacio que se encuentra en el mayor valor de X que llegar� la c�mara
    float min; //Menor valor que puede obteter la x
    float max; //Mayor valor que puede obtener la x
    public float lookup; //Valor que guarda el tiempo que tiene que estar presionando W para mirar arriba
    float time; //Contador que guarta el tiempo que matiene pulsado W
    public float lookup_range; //Valor que guarda la distancia en la que hace el lookup
    public float lookup_speed; //Velocidad de la c�mara al mirar arriba


    // Start is called before the first frame update
    void Start()
    {
        //Se establece el m�ximo y el m�nimo
        min = this.transform.position.x;
        max = maxCam.transform.position.x;
        //Se crea el vector posici�n de la c�mara
        posicion = this.transform.position - target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 newposition = target.transform.position + posicion; //Vector que guarda la nueva posici�n de la c�mara



            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.Space)) //Se comprueba que el player est� quieto
            {
                if (Input.GetKeyDown(KeyCode.W)) //Si pulsa W se establece el valor del contador
                {
                    time = lookup;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    if (time <= 0) //Cuando el contador acaba se mueve la c�mara hacia arriba
                    {
                        this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, newposition.y + lookup_range, newposition.z), lookup_speed * Time.deltaTime);
                    }
                    else //Si el contador todav�a no ha llegado a 0 se sigue descontando tiempo
                    {
                        time -= Time.deltaTime;
                    }
                }
                else //Cuando se deja de pulsar W se mueve correctamente a su posic�n e y
                {
                    this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, newposition.y, this.transform.position.z), lookup_speed * Time.deltaTime);
                }
            }
            else
            {
                //Esto ocurre cuando se pulsa A, S, D o Espacio
                if (newposition.x >= min && newposition.x <= max) //Comprueba que la c�mara no haya llegado a los l�mites
                {
                    this.transform.position = newposition;
                }
                else
                {
                    this.transform.position = new Vector3(this.transform.position.x, newposition.y, newposition.z); //Si la c�mara a llegado a los l�mites el valor de x no cambia pero el de y s� (Al moverse en horizontal el z siempre ser� el mismo)
                }
            }
        }
    }
}
