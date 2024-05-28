using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Controle da velocidade, força de pula e acesso aos componentes
    public float Speed;
    public float JumpForce;
    private Rigidbody2D rig;
    private Animator Anim;
    //Pra verificar quando o personagem tá no chão e ele poder pular denovo sem delay
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    //Controle da lógica de pulo
    private bool IsJumping;
    private bool DoubleJumping;
    float movement;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    void Update()
    {   //pega o input do teclado e faz o movimento usand o rigibody multiplicando a velocidade definida pelo eixo
        movement = Input.GetAxis("Horizontal");
        CheckGround();
        
        Jump();
    }
    private void FixedUpdate()
    {
        /*Só a lógica de andar normal, quando foi < que 0f é pq ele está andando pra esquerda então no vector 3 ( x,y,z) altera y pra 180 que faz o player virar
        se não ouver movimento walk fica como false daí ele n fica parado com animação de andar*/
        rig.velocity = new Vector2(movement * Speed, rig.velocity.y);
        if (movement > 0f)
        {
            Anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (movement < 0f)
        {
            Anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else
        {
            Anim.SetBool("walk", false);
        }
    }

   

    void CheckGround()
    {
        bool wasJumping = IsJumping; // Armazena o estado anterior de IsJumping
        IsJumping = !Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Se o player estava no chão e agora está no ar  ativa a animação de pulo
        if (IsJumping && !wasJumping)
        {
            Anim.SetBool("jump", true);
        }

        // Se o player estava no ar e agora está no chão desativa as animações de pulo e pulo duplo
        if (!IsJumping && wasJumping)
        {
            Anim.SetBool("jump", false);
            Anim.SetBool("DoubleJump", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!IsJumping)
            {
                // Primeiro pulo, só confere o impulso no rigidbody e ativa animação
                rig.velocity = new Vector2(rig.velocity.x, 0f);
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                IsJumping = true;
                Anim.SetBool("jump", true);
                DoubleJumping = true;
            }
            else if (DoubleJumping)
            {
                // Segundo pulo confere novamente o impulso e agora ativa a animação de double jump
                rig.velocity = new Vector2(rig.velocity.x, 0f);
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                Anim.SetBool("DoubleJump", true);
                DoubleJumping = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Basicamente pega que quando entrar em colisão 
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            
        }
        // Basicamente pega que quando entrar em colisão com alguns desses objetos que possuem essas tags o player é destruido e mostra o gameOver
        else if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Saw") || collision.gameObject.CompareTag("Bloco"))
        {
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
        //pra transformar o player em obj filho da plataforma e o movimento dele ficar suave nela
        else if (collision.gameObject.CompareTag("Platform"))
        {
            gameObject.transform.parent = collision.transform;
        }
        // Aqui verfica se o player atingiu o objeto com tag enemy na parte de cima, o ponto y se for elimina ele se n for e o player atingir/for atingido pelo inimigo ele morre.
        else if (collision.gameObject.CompareTag("Enemy"))
        {
           
            float contactPointY = collision.contacts[0].point.y;
            float playerBottomY = transform.position.y - (GetComponent<Collider2D>().bounds.size.y / 2);

            if (contactPointY < playerBottomY)
            {
                // O player atingiu o inimigo de cima
                Destroy(collision.gameObject);
                DoubleJumping = true; // Permitir que o player pule novamente
                rig.velocity = new Vector2(rig.velocity.x, 0f);
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            }
            else
            {
                // O player foi atingido pelo inimigo
                GameController.instance.ShowGameOver();
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {   // quando sair de objeto com tag plataforma ele larga de ser obj filho 
        if (collision.gameObject.CompareTag("Platform"))
        {
            gameObject.transform.parent = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {   //pra garantir, quando entra em contato com a layer 10, que é uma layer de inimigos, ele desativa todos os componentes do player e o destruir.
        if (collision.gameObject.layer == 10)
        {
            rig.velocity = Vector2.zero;
            rig.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            collision.gameObject.GetComponent<Canibal>().enabled = false;
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            Destroy(collision.gameObject, 1f);
        }
    }

}
