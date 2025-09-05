using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PelletSpawner : MonoBehaviour
{
    public GameObject pelletPrefab;
    [SerializeField] private float distanceBuffer = 5f;
    
 


  


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
        float x = UnityEngine.Random.Range(GameHandler.Instance.leftbound + distanceBuffer, GameHandler.Instance.rightbound - distanceBuffer);
        float y = UnityEngine.Random.Range(GameHandler.Instance.bottom + distanceBuffer, GameHandler.Instance.top - distanceBuffer);

        
        Vector2 spawnPos = new Vector2(x, y);

        Debug.Log(x);
        Debug.Log(y);
        Collider2D hit = Physics2D.OverlapPoint(spawnPos);
        if (hit != null)
        {
            SpawnPellet();
        }
        if(hit == null)
        {
            Instantiate(pelletPrefab, spawnPos, Quaternion.identity);

        }
      
        
        
       

     

        
    }
}
