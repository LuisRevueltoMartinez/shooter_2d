using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemigo : MonoBehaviour
{
    public float hp;
    NavMeshAgent agent;
    public GameObject target;
    public float time;
    float contador;
    public float damage;
    float contactTimer;
    float z;
    public GameObject muerte;
    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        z = this.transform.position.z;
        audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.z != z) { 
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, z);
        }
        if (contador <= 0 && target != null)
        {
            agent.SetDestination(target.transform.position);
        }
        else
        {
            contador -= Time.deltaTime;
        }
        
    }

    public void Damage(float damage)
    {
        audio.Play();
        hp -= damage;
        if (hp <= 0)
        {
            GameObject death = Instantiate(muerte, this.transform.position, this.transform.rotation);
            Destroy(death, 1);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.transform.tag == "Player")
        {
            agent.ResetPath();
            contador = time;
            collision.gameObject.GetComponent<Jugador>().Damage(damage);
            collision.gameObject.GetComponent<Animator>().SetTrigger("hit");
            contactTimer = time - 0.1f;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (contactTimer <= 0)
        {
            if (collision.gameObject.transform.tag == "Player")
            {
                agent.ResetPath();
                contador = time;
                collision.gameObject.GetComponent<Jugador>().Damage(damage);
                collision.gameObject.GetComponent<Animator>().SetTrigger("hit");
                contactTimer = time - 0.1f;
            }
        }
        else
        {
            contactTimer -= Time.deltaTime;
        }
    }
   
}
