using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Canibal : MonoBehaviour
{

    public float speed;
    public bool ground = true;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool facingRight = true;
    

    // Update is called once per frame
    void Update()
    {
        /*Controle de mivimento, basicamente na minha layermask está definido o ground, então ele fica com um objeto em baixo verificando sempre se está no chão
         quando ele verifica que não vai estar ele ativa o flip e gira o personagem para a outra direção */
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        ground = Physics2D.Linecast(groundCheck.position, transform.position, groundLayer);
        if (ground == false)
        {
            speed *= -1;
        }
        if(speed > 0 && !facingRight)
        {
            Flip();
        }
        if (speed < 0 && facingRight)
        {
            Flip();
        }
    }
        //Só pra girar o boneco para o outro lado no X
        void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
