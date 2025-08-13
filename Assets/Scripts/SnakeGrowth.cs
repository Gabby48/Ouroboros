using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SnakeGrowth : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private Transform head;
    [SerializeField] private Transform tail;

    [SerializeField] private Transform snakeTransform;
    [SerializeField] private List<Transform> bodyParts = new List<Transform>();
    [SerializeField] private float spawnGap = 6.4f;

    [SerializeField] private int snakeSize = 0;

    [SerializeField] List<Vector2Int> snakeMovePosList;



    // Start is called before the first frame update
    void Start()
    {
        bodyParts.Add(head);
        bodyParts.Add(tail);
        snakeSize = 0;
        Pellet.OnPelletCollected += Pellet_OnPelletCollected;
    }

    private void Pellet_OnPelletCollected(object sender, System.EventArgs e)
    {
        Grow();
        snakeSize++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grow()
    {
        Transform tailPart = bodyParts[bodyParts.Count - 1];

        GameObject newBody = Instantiate(bodyPrefab,tailPart.position, tailPart.rotation, transform);
        bodyParts.Insert(bodyParts.Count - 1, newBody.transform);

        Vector3 localPos = tailPart.localPosition;

        localPos.x -= spawnGap;

        tailPart.localPosition = localPos;
    

        
    }
}
