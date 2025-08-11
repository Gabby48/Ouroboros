using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pellet : MonoBehaviour
{
    public static event EventHandler OnPelletCollected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PelletCollect()
    {
       OnPelletCollected?.Invoke(this,EventArgs.Empty);
        Destroy(gameObject);
    }

}
