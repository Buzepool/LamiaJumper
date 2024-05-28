using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class FallingPlatform : MonoBehaviour
{

    public float PlatFall;

    // Usar para acessar os componentes que est�o dentro do objeto na unity
    private TargetJoint2D target;
    private BoxCollider2D BoxColl;

    // Start is called before the first frame update
    void Start()
    {
        // Da acesso a essas vari�veis para manipularem os componentes
        target = GetComponent<TargetJoint2D>();
        BoxColl = GetComponent<BoxCollider2D>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {   //Se a colis�o for com o player invoca o m�todo falling de acordo com o tempo de platFall
        if (collision.gameObject.tag == "Player")       
        {
            Invoke("Falling", PlatFall);
        }
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {   //uma Layer s� pra destruir as plataformas quando elas caem e tocam nela
        if (collider.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }
    void Falling()
    {   //Desativa e aciona o bool
        target.enabled = false;
        BoxColl.isTrigger = true;
    }
}
