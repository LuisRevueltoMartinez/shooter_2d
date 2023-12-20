using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public float timer;
    float contador;

    // Start is called before the first frame update
    void Start()
    {
        contador = timer;
    }

    // Update is called once per frame
    void Update()
    {
         if(contador <= 0) 
        {
            GameObject enemigo = Instantiate(enemy, this.transform.position, this.transform.rotation);
            enemigo.GetComponent<Enemigo>().target = player;
            contador = timer;
        }
        else
        {
            contador -= Time.deltaTime;
        }
    }
}
