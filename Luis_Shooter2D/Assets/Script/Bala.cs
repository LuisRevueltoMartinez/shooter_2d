using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour 
{ 
    public float damage;

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemigo>().Damage(damage);
        }
        else
        {
            if (other.transform.tag == "Player")
            {
                other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                other.gameObject.GetComponent<Jugador>().Damage(damage);
                other.gameObject.GetComponent<Animator>().SetTrigger("hit");
            }
        }
        if(other.transform.tag != "Plataforma")
        {
            Destroy(this.gameObject);
        }
        
    }
}

