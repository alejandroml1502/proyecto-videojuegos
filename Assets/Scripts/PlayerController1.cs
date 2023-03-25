using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public float velocidad;
    public float velocidadMax;
    public float fuerzaSalto;
    public bool colPies = false;
    public float friccionSuelo;

    private Rigidbody2D rPlayer;
    private float h;
    private Animator aPlayer;

    private bool miraDerecha = true;
    private bool enPlataforma = false;
    // Start is called before the first frame update
    void Start()
    {
        rPlayer = GetComponent<Rigidbody2D>();
        
        aPlayer = GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        giraPlayer(h);

        aPlayer.SetFloat("VelocidadX", Mathf.Abs(rPlayer.velocity.x));
        aPlayer.SetFloat("VelocidadY", rPlayer.velocity.y);
        aPlayer.SetBool("TocaSuelo", colPies);

        //Update para crear el salto del persoanje
        colPies = CheckGround.colPies;
        if (Input.GetButtonDown("Jump")&&colPies)
        {
            rPlayer.velocity = new Vector2(rPlayer.velocity.x, 0f);
            rPlayer.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        rPlayer.AddForce(Vector2.right * velocidad * h);
        float limiteVelocidad = Mathf.Clamp(rPlayer.velocity.x, -velocidadMax, velocidadMax);
        rPlayer.velocity = new Vector2(limiteVelocidad, rPlayer.velocity.y);

        if(h == 0 && colPies)
        {
            Vector3 velocidadArreglada = rPlayer.velocity;
            velocidadArreglada.x *= friccionSuelo;
            rPlayer.velocity = velocidadArreglada;
        }
    }
    public void giraPlayer(float horizontal)
    {
        if((horizontal > 0 && !miraDerecha) || horizontal < 0 && miraDerecha)
        {
            miraDerecha = !miraDerecha;
            Vector3 escalaGiro = transform.localScale;
            escalaGiro.x = escalaGiro.x * -1;
            transform.localScale = escalaGiro;
        }
    }

    //Movimiento de personaje y platafdorma
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlataformaMov")
        {
            transform.parent = collision.transform;
            enPlataforma = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlataformaMov")
        {
            transform.parent = null;
            enPlataforma = false;
        }
    }

}