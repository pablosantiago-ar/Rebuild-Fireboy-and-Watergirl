using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class player : MonoBehaviour
{

    public float Speed;
    public float jumpForce;

    public bool isGirl;
    public bool isFire;
    public bool isJumping;
    public bool doubleJump;

    public int fase;
    
    private Rigidbody2D rig;
    private Animator anim;

    public AudioSource jumpSoundEffect;
    public AudioSource stepSoundEffect;
    public AudioSource deadSoundEffect;
    public AudioSource reviveSoundEffect;
    public AudioSource getUWSoundEffect;
    public AudioSource UWSoundEffect;
    public AudioSource outUWSoundEffect;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        charSelector();
    }

    void charSelector(){
        if (isFire == false && isGirl == false) {
            anim.SetBool("girl", false);
            anim.SetBool("fire", false);
        }
        if (isFire == true && isGirl == false) {
            anim.SetBool("girl", false);
            anim.SetBool("fire", true);
        }
        if (isFire == false && isGirl == true) {
            anim.SetBool("girl", true);
            anim.SetBool("fire", false);
        }
        if (isFire == true && isGirl == true) {
            anim.SetBool("girl", true);
            anim.SetBool("fire", true);
        }
    }

    void Move() 
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        //walking right
        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }

        //walking left
        if(Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }

        //not walking
        if(Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("walk", false);
        }

    }


    void Jump() 
    {
        if(Input.GetButtonDown("Jump"))
            if(!isJumping)
            {
                jumpSoundEffect.Play();
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                anim.SetBool("jump", true);
            }
            else
            {
                if(doubleJump)
                {
                    jumpSoundEffect.Play();
                    rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
            
    }

    private void walkSound()
    {
        stepSoundEffect.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6 || collision.gameObject.tag == "stairs")
        {
            isJumping = false;
            doubleJump = false;
            anim.SetBool("jump", false);
        }

    }

    private void DieSound()
    {
        deadSoundEffect.Play();
    }

    private void Die(){
        if (fase == 1)
        {
            transform.position = new Vector3(0f,0f,0f);
            rig.bodyType = RigidbodyType2D.Dynamic;
            anim.SetBool("death", false);
            reviveSoundEffect.Play();
        }
        if (fase == 2)
        {
            transform.position = new Vector3(79f,-39f,0f);
            rig.bodyType = RigidbodyType2D.Dynamic;
            anim.SetBool("death", false);
            reviveSoundEffect.Play();
        }
    }

    /* Ap�s o teste:
    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }
    */

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == 4)
        {
            if (isFire == true)
            {
                rig.bodyType = RigidbodyType2D.Static;
                anim.SetBool("death", true);
            }

            getUWSoundEffect.Play();
            UWSoundEffect.Play();
        }

        if (other.gameObject.tag == "Spike")
        {
            if( isFire == false)
            {
                rig.bodyType = RigidbodyType2D.Static;
                anim.SetBool("death", true);
            }
        }

        if (other.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("lvl_1_CS");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == 4){
            outUWSoundEffect.Play();
            UWSoundEffect.Pause();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6 || collision.gameObject.tag == "stairs")
        {
            isJumping = true;
            doubleJump = true;
        }
    }
}
