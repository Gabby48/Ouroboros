using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletSpawner : MonoBehaviour
{
    public GameObject pelletPrefab;
    public LayerMask wallLayer;


    [SerializeField] private float bottom, top, leftbound, rightbound;


    // Start is called before the first frame update
    void Start()
    {
        FindBoundsWithRaycasts();
        SpawnPellet();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindBoundsWithRaycasts()
    {
        Vector2 originPoint = Vector2.zero;

        RaycastHit2D hitLeft = Physics2D.Raycast(originPoint, Vector2.left , Mathf.Infinity, wallLayer);
        leftbound = hitLeft.point.x;

        RaycastHit2D hitRight = Physics2D.Raycast(originPoint, Vector2.right, Mathf.Infinity, wallLayer);
        rightbound = hitRight.point.x;

        RaycastHit2D hitDown = Physics2D.Raycast(originPoint, Vector2.down, Mathf.Infinity, wallLayer);
        bottom = hitDown.point.y;

        RaycastHit2D hitUp = Physics2D.Raycast(originPoint, Vector2.up, Mathf.Infinity, wallLayer);
        top = hitUp.point.y;

    }

    public void SpawnPellet()
    {
        float x = Random.Range(leftbound, rightbound);
        float y = Random.Range(bottom, top);

        Vector2 spawnPos = new Vector2(x, y);
        Instantiate(pelletPrefab, spawnPos, Quaternion.identity);
    }
}
