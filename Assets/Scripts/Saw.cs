using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{   //Velocidade e tempo de movimento da serra
    public float speed;
    public float moveTime;

    private bool dirRight = true;
    private float timer;
  
    
    // Update is called once per frame
    void Update()
    {
        //Basicamente pega e altera a posicição de acordo com timer, que compara com a moveTime definida por mim, quando for maior ou igual ele altera a direção
        if (dirRight) 
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else 
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        timer += Time.deltaTime;
        if (timer >= moveTime) 
        {
            dirRight = !dirRight;
            timer = 0;
        }
    }   
}
