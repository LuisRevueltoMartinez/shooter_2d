using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

        private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player" && !Input.GetKey(KeyCode.S))
        {
            if(other.transform.position.y-this.transform.position.y >= other.transform.localScale.y/2)
            {
                this.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            this.GetComponent <BoxCollider>().isTrigger = true;
        }
    }
}
