using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce;
    private Animator anim;

    void Start()
    {   //pra rodar a animção de jump do trampolin
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   //Basicamente só pega o componente do player o rigid body e adiciona o impulso pro pula, de acordo com o jumpForce que eu definir
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("jump");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}
