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

    [SerializeField] private bool canMoveR = true;
    [SerializeField] private bool canMoveL = true;
    [SerializeField] private bool canMoveU = true;
    [SerializeField] private bool canMoveD = true;
     

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = new Vector3 (moveSpeed, 0, 0);
        canMoveD = false;

        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W) && canMoveU)
        {
            moveDirection = new Vector3 (0, moveSpeed, 0);
            moveRotation = new Vector3 (0,0,rotUP);

            canMoveD = false;

            canMoveR = true;
            canMoveL = true;


        }

        if (Input.GetKey(KeyCode.S) && canMoveD)
        {
            moveDirection = new Vector3(0, -moveSpeed, 0);
            moveRotation = new Vector3(0,0,-rotUP);

            canMoveU = false;

            canMoveL = true;
            canMoveR = true;
        }

        if (Input.GetKey(KeyCode.D) && canMoveR)
        {
            moveDirection = new Vector3(moveSpeed, 0, 0);
            moveRotation = new Vector3(0, 0, 0);

            canMoveL = false;

            canMoveU = true;
            canMoveD = true;


        }

        if (Input.GetKey(KeyCode.A) && canMoveL)
        {
            moveDirection = new Vector3(-moveSpeed, 0, 0); 
            moveRotation = new Vector3(rotLeft,0,rotLeft);

            canMoveR = false;

            canMoveU = true;
            canMoveD = true;
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
