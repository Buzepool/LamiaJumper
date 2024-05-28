using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndaimeVertical : MonoBehaviour
{   private bool moveDown = true;
    public float moveSpeed =  -3f;
    public Transform pointA;
    public Transform pointB;
    
    //Praticamente mesma lógica do andaime horizontal, porém agora vertical, então a gente meche no eixo y 
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > pointA.position.y)
        {
            moveDown = true;
        }
        if (transform.position.y < pointB.position.y)
        {
            moveDown = false;
        }
        if (moveDown) 
        {
            transform.position = new Vector2(transform.position.x, transform.position.y -moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
        }

    }       
}
