using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dustParticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("WaitActive", 1.0f);
    }

    void WaitActive()
    {
        this.gameObject.SetActive(false);
    }
}
