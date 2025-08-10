using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveTimer = 0f;
    [SerializeField] private float moveTimerMax = 0.1f;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotUP = 90f;
    [SerializeField] private float rotLeft = 180f;

    [SerializeField] Vector3 moveDirection;
    [SerializeField] Vector3 moveRotation;
     

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = new Vector3 (moveSpeed, 0, 0);

        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = new Vector3 (0, moveSpeed, 0);
            moveRotation = new Vector3 (0,0,rotUP);

        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDirection = new Vector3(0, -moveSpeed, 0);
            moveRotation = new Vector3(0,0,-rotUP);
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection = new Vector3(moveSpeed, 0, 0);
            moveRotation = new Vector3(0, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection = new Vector3(-moveSpeed, 0, 0); 
            moveRotation = new Vector3(rotLeft,0,rotLeft);    
        }

        moveTimer += Time.deltaTime;
        if (moveTimer >= moveTimerMax)
        {
            MoveObject(moveDirection,moveRotation);
            moveTimer = 0f;
           
        }
        
    }


    private void MoveObject(Vector3 moveDir, Vector3 moveRot)
    {

        
        transform.position += moveDir;
        transform.rotation = Quaternion.Euler(moveRot);
    }

   
    
}
