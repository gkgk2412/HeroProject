using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : ArrowValue, IPooledObject
{
    Rigidbody _rb;

    public void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
    }

    public void OnObjectSpawn()
    {
        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2.0f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);

        GetComponent<Rigidbody>().velocity = force;
    }

    public void Update()
    {
        Vector3 dir = transform.forward;
        transform.position += dir * speed * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            this.gameObject.SetActive(false);
        }

        if(other.gameObject.tag == "Monster")
        {
            string message = ""+ damage;
            Vector3 textPos = new Vector3(other.transform.position.x, other.transform.position.y + 1.4f, other.transform.position.z);

            FloatingTextManager.instance.CreateFloatingDamageText(textPos, message);

        }
    }
}
