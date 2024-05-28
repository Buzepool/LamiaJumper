using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloco : MonoBehaviour
{
    
    public GameObject bloco;
    public Transform pontoA;
    private bool isMove;
    public float speedMove;

    // Update is called once per frame
    void Update()
    {
        //quando a isMove for ativa significa que a pedra já bateu no chão, como ele precisa voltar, tranforma a posição dele na do objeto A que fica fixo para onde eu quero que volte a pedra.
        if (isMove == true)
        {
            bloco.transform.position = Vector2.MoveTowards(bloco.transform.position, pontoA.transform.position, speedMove * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //basicamente monitora se o que entra na hitbox é player, se for "olha a peuda", mudando a gravity scale e a massa altera a velocidade que ele cai 
        if (col.gameObject.CompareTag("Player"))
        {
            bloco.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            bloco.GetComponent<Rigidbody2D>().gravityScale = 7;
            bloco.GetComponent<Rigidbody2D>().mass = 400;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //depois qaye cai reseta gavidade e massa e o tipo de corpo pra voltar a pedra pra cima deixando a bool isMove como verdadeira
        if (col.gameObject.layer == 8)
        {
            bloco.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            bloco.GetComponent<Rigidbody2D>().gravityScale = 0;
            bloco.GetComponent<Rigidbody2D>().mass = 0;

            isMove = true;
        }
    }
}