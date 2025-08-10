using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveTimer = 0f;
    [SerializeField] private float moveTimerMax = 0.5f;

    [SerializeField] private float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveTimerMax)
        {
            MoveObject();
            moveTimer = 0f;
           
        }
        
    }


    private void MoveObject()
    {

        
        Vector3 moveDir = new Vector3(moveSpeed, 0,0);

        transform.position += moveDir;

    }
    
}
