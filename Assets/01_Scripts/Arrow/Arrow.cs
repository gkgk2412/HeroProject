using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : ArrowValue, IPooledObject
{
    public int _damage;

    Rigidbody _rb;

    private static Arrow instance;

    public static Arrow Instance => instance;

    public Arrow()
    {
        //자기 자신에 대한 정보를 static 형태의 instacne 변수에 저장하여 외부에서 접근이 쉽도록 함
        instance = this;
    }

    public void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _damage = 20;
    }

    public void OnObjectSpawn()
    {
        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2.0f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);

        this.GetComponent<Rigidbody>().velocity = force;
    }

    public void Update()
    {
        Vector3 dir = transform.forward;
        transform.position += dir * speed * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player" && other.gameObject.tag != "StopBoss_damage" && other.gameObject.tag != "Bottom" && other.gameObject.tag != "Range" && other.gameObject.tag != "spear")
        {
            this.gameObject.SetActive(false);
        }

        if(other.gameObject.tag == "mushroom" || other.gameObject.tag == "radish" || other.gameObject.tag == "crystal")
        {
            string message = "" + Arrow.Instance._damage;
            Vector3 textPos = new Vector3(other.transform.position.x, other.transform.position.y + 1.4f, other.transform.position.z);

            FloatingTextManager.instance.CreateFloatingDamageText(textPos, message);
        }
    }
}
