using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    public float hp;
    float vida;
    bool llaveA;
    bool llaveB;
    AudioSource audio;
    public AudioClip original;
    public Image barra;
    // Start is called before the first frame update
    void Start()
    {
        vida = hp;
        audio = GetComponent<AudioSource>();
    }

    public void Damage(float damage)
    {
        audio.clip = original;
        audio.Play();
        hp -= damage;
        barra.fillAmount = hp / vida;
        if (hp <= 0)
        {
            SceneManager.LoadScene("Derrota");
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Escudo_A")
        {
            if (llaveA){
                Destroy(collision.gameObject);
            }
        }
        if (collision.transform.tag == "Escudo_B")
        {
            if (llaveB)
            {
                Destroy(collision.gameObject);
            }
        }
        if(collision.transform.tag == "Final")
        {
            SceneManager.LoadScene("Victoria");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Llave_A")
        {
            llaveA = true;
            Destroy(other.gameObject);
        }
        if (other.transform.tag == "Llave_B")
        {
            llaveB = true;
            Destroy(other.gameObject);
        }
    }
}
