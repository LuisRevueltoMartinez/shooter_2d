using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_Player : MonoBehaviour
{
    Rigidbody myRigid;
    public float vel = 6; //Velocidad de movimiento
    public float salto = 6; //Fuerza de salto
    public GameObject lim_izq; //Limite que el player puede moverse a izq
    public GameObject lim_der; //Limite que el player puede moverse a der
    public GameObject cuerpo; //Un GameObject con el cuerpo del player para girarlo y cambiar de mano el bastón
    Animator animator;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        myRigid = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A) && this.transform.position.x > lim_izq.transform.position.x) //El player se mueve a la izquierda
        {
            animator.SetBool("move",false);
            this.transform.Translate(Vector3.left * vel * Time.deltaTime, Space.World);
            this.GetComponent<BoxCollider>().enabled = false; //Desactivo temporalmente el BoxCollider para evitar el mensaje de advertencia
            this.transform.localScale = new Vector3(-1,this.transform.localScale.y,1); // Cambio la escala del Player para darle la vuelta
            this.GetComponent<BoxCollider>().enabled = true; //Lo reactivo de nuevo
            cuerpo.transform.localScale = new Vector3(1, cuerpo.transform.localScale.y,-1);
        }
        else {
            if (Input.GetKey(KeyCode.D) && this.transform.position.x < lim_der.transform.position.x) //El player se mueve a la derecha
            {
                animator.SetBool("move", false);
                this.transform.Translate(Vector3.right * vel * Time.deltaTime, Space.World);
                this.transform.localScale = new Vector3(1, transform.localScale.y, 1); // Cambio la escala del Player para darle la vuelta
                cuerpo.transform.localScale = new Vector3(1, cuerpo.transform.localScale.y, 1);
            }
            else
            {
                animator.SetBool("move", true);
            }
        }
        
        if(Input.GetKeyDown(KeyCode.Space)) //El player salta
        {
            RaycastHit hit;
            if (Physics.Raycast(this.transform.position, Vector3.down, out hit, 1.38f)) //Se comprueba si está tocando el suelo
            {
                if (hit.collider.tag == "Suelo" || hit.collider.tag == "Plataforma")
                {
                    myRigid.velocity = Vector3.up * salto;
                }
            }
        }
    }
}
