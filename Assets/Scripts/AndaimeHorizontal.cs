using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndaimeHorizontal : MonoBehaviour
{   //booleano pra controle
    private bool moveRight = true;
    //Definir velocidade e o objetos que serão os pontos que decidem até onde vai o andaime
    public float moveSpeed =  -3f;
    public Transform pointA;
    public Transform pointB;
    

    // Update is called once per frame
    //Basicamente os condionais dai, por exemplo se a pos for maior que o ponto ele altera o  valor do booleano para ele ir para o lado contrário
    void Update()
    {
        if (transform.position.x > pointA.position.x)
        {
            moveRight = true;
        }
        if (transform.position.x < pointB.position.x)
        {
            moveRight = false;
        }
        if (moveRight) 
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
        }

    }       
}
