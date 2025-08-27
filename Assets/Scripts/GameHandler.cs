using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public LayerMask wallLayer;
  

    public float bottom, top, leftbound, rightbound;

    [SerializeField] private float spawnBuffer = 3f;
    
    public static GameHandler Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The game has begun");
        FindBoundsWithRaycasts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindBoundsWithRaycasts()
    {
        Vector2 originPoint = Vector2.zero;

        RaycastHit2D hitLeft = Physics2D.Raycast(originPoint, Vector2.left, Mathf.Infinity, wallLayer);
        leftbound = hitLeft.point.x + spawnBuffer;

        Debug.Log("leftbound" + leftbound);
        

        RaycastHit2D hitRight = Physics2D.Raycast(originPoint, Vector2.right, Mathf.Infinity, wallLayer);
        rightbound = hitRight.point.x - spawnBuffer;
        Debug.Log("rightbound" + rightbound);


        RaycastHit2D hitDown = Physics2D.Raycast(originPoint, Vector2.down, Mathf.Infinity, wallLayer);
        bottom = hitDown.point.y + spawnBuffer;
        Debug.Log("bottom" + bottom);

        RaycastHit2D hitUp = Physics2D.Raycast(originPoint, Vector2.up, Mathf.Infinity, wallLayer);
        top = hitUp.point.y - spawnBuffer;
        Debug.Log("top" + top);
        



    }
}
