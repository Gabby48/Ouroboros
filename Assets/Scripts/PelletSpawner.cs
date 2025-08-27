using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PelletSpawner : MonoBehaviour
{
    public GameObject pelletPrefab;
    
    
 


  


    // Start is called before the first frame update
    void Start()
    {
        Pellet.OnPelletCollected += Pellet_OnPelletCollected;

       
        SpawnPellet();
        
    }

    private void Pellet_OnPelletCollected(object sender, System.EventArgs e)
    {
        SpawnPellet();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

   
    public void SpawnPellet()
    {
        float x = UnityEngine.Random.Range(GameHandler.Instance.leftbound, GameHandler.Instance.rightbound);
        float y = UnityEngine.Random.Range(GameHandler.Instance.bottom, GameHandler.Instance.top);

        Vector2 spawnPos = new Vector2(x, y);

        RaycastHit2D hit = Physics2D.Raycast(spawnPos, Vector2.zero);

      
        Instantiate(pelletPrefab, spawnPos, Quaternion.identity);
        
       

     

        
    }
}
