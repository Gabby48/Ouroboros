using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Movement : MonoBehaviour
{
    [SerializeField] private  LayerMask bodyLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask tailLayer;


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


   [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private Transform head;
   [SerializeField] private GameObject tailPrefab;
    

    [SerializeField] private float spawnGap = 6.4f;

    [SerializeField] private int snakeSize = 0;

   

    [SerializeField] private float bodyhitCooldownTimer;
    [SerializeField] private float bodyhitCooldownTimerMax = 1f;

    [SerializeField] private float detectBuffer = 1f;
    [SerializeField] private float spawnBuffer = 5f;
    
    
    public List<Transform> bodyParts;

    public int stepsPerSegment = 1;

    public static event EventHandler OnCycleComplete;


    
    
    public struct SnakeFrame
    {
        public Vector3 position;
        public Quaternion rotation;

        public SnakeFrame(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }
       
    }

    private List<SnakeFrame> history = new List<SnakeFrame>();

    void Awake()
    {
      
        
    }

    // Start is called before the first frame update
    void Start()
    {
        bodyParts.Add(head);
        GameObject tailObj = Instantiate(tailPrefab, head.position - Vector3.right * spawnGap, Quaternion.identity, head.parent);
        bodyParts.Add(tailObj.transform);
        snakeSize = 0;
        Pellet.OnPelletCollected += Pellet_OnPelletCollected;

        moveDirection = new Vector3 (moveSpeed, 0, 0);
        canMoveD = false;
        history.Clear();    

        
    }


    private void Pellet_OnPelletCollected(object sender, System.EventArgs e)
    {
        MultiGrow();
       
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

        bodyhitCooldownTimer += Time.deltaTime;

        if (moveTimer >= moveTimerMax)
        {
            

            MoveObject(moveDirection,moveRotation);
            moveTimer = 0f;

          

            
           
        }

     
        


    }


    void FixedUpdate()
    {
      

    }


    private void MoveObject(Vector3 moveDir, Vector3 moveRot)
    {

        head.position += moveDir;
        head.rotation = Quaternion.Euler(moveRot);

        history.Insert(0, new SnakeFrame(head.position,head.rotation));

        if(history.Count > bodyParts.Count * stepsPerSegment +1)
        {
            history.RemoveAt(history.Count - 1);
        }


        for (int i = 1; i < bodyParts.Count;i++)
        {
            int index = i * stepsPerSegment;

            if(index < history.Count)
            {
                bodyParts[i].position = history[index].position;
                bodyParts[i].rotation = history[index].rotation;
            }
        }

    }


    

  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(((1 << other.gameObject.layer) & bodyLayer) != 0)
        {
            float touchDistance = Vector2.Distance(transform.position, other.transform.position);

            if (touchDistance > 0.5f && bodyhitCooldownTimer >= bodyhitCooldownTimerMax) 
            {
                bodyhitCooldownTimer = 0f;
                
                Transform bodyHit = other.transform;

                int index = bodyParts.IndexOf(bodyHit);

                Shrink(index);

                
            }
            
        }

        if (((1 << other.gameObject.layer) & wallLayer) != 0)
        {

            Debug.Log(head.position);
          
                if (head.position.x >= GameHandler.Instance.rightbound - detectBuffer)
                {
                    head.position = new Vector3(-75, head.position.y, head.position.z);
                }
                else if (head.position.x <= GameHandler.Instance.leftbound + detectBuffer)
                {
                    head.position = new Vector3(75, head.position.y, head.position.z);
                }


                if (head.position.y >= GameHandler.Instance.top - detectBuffer)
                {
                    head.position = new Vector3(head.position.x, -35, head.position.z);
                }
                else if (head.position.y <= GameHandler.Instance.bottom + detectBuffer)
                {
                    
                    head.position = new Vector3(head.position.x, 35, head.position.z);
                }


        }

        if(((1 << other.gameObject.layer) & tailLayer) != 0)
        {
            OnCycleComplete?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 0f;
            Debug.Log("You've Completed the Cycle");
        }

    }

    private void WallChange()
    {

    }


    public void Grow()
    {
        Transform tailPart = bodyParts[bodyParts.Count - 1];

        GameObject newBody = Instantiate(bodyPrefab, tailPart.position, Quaternion.identity, head.parent);

       

        bodyParts.Insert(bodyParts.Count - 1, newBody.transform);

        snakeSize++;


    }

    public void MultiGrow(int index = 1)
    {
        for(int i = 0; i < index; i++)
        {
            Debug.Log("The player has grown");
            Grow();
        }
    }

    public void Shrink(int index = -1)
    {
        if(index == -1)
        {
            index = bodyParts.Count - 2;

        }

        if(index>0 && index < bodyParts.Count - 1)
        {
            Transform parttoRemove = bodyParts[index];
            Destroy(parttoRemove.gameObject);
            bodyParts.RemoveAt(index);
            snakeSize--;
        }

        Debug.Log("character has shrunk");

    }

    public void QuickShrink()
    {
        snakeSize = 0;
        for (int i = bodyParts.Count-1 ; i > 0; i--)
        {
            Shrink(i);
        }

        Debug.Log("We have shrunk to base");
    }




}
