using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IPooledObject
{
    public float upForce = 1.0f;
    public float sideForce = .1f;
    public float speed = .5f;

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
        transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + speed);
    }
}
