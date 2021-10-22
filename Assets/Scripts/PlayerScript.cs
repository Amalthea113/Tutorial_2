using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    public Text lives;

    private int scoreValue = 0;
    private int numlives = 3;
    public Text winText;
    public Text loseText;
    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;
    public bool gameOver;
    private bool facingRight = true;
    Animator anim;
    float hozMovement;
    float vertMovement;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        lives.text = "Lives: " + numlives.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        gameOver = false;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        
        if (Input.GetKeyDown(KeyCode.W)){
             anim.SetInteger("State", 2);
         }
        if (Input.GetKeyUp(KeyCode.W)){
            anim.SetInteger("State", 0);
         }

        if (Input.GetKeyDown(KeyCode.D)){
             anim.SetInteger("State", 1);
         }
        if (Input.GetKeyUp(KeyCode.D)){
            anim.SetInteger("State", 0);
         }
        if (Input.GetKeyDown(KeyCode.A)){
             anim.SetInteger("State", 1);
         }
        if (Input.GetKeyUp(KeyCode.A)){
            anim.SetInteger("State", 0);
         }

         if (facingRight == false && hozMovement > 0)
   {
     Flip();
   }
else if (facingRight == true && hozMovement < 0)
   {
     Flip();
   }

    }


    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
    void FixedUpdate()
    {
        hozMovement = Input.GetAxis("Horizontal");
        vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.gameObject.CompareTag("Coin"))
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            collision.collider.gameObject.SetActive(false);
            if (scoreValue == 4){
            transform.position = new Vector2(90, -0.07f); 
            numlives = 3;
            lives.text = "Lives: " + numlives.ToString();
            }
        }
        
        if (scoreValue >= 8 && gameOver==false){
            winText.gameObject.SetActive(true);
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
            gameOver=true;
            
        }
         if (collision.gameObject.CompareTag("Enemy"))
        {
            numlives -= 1;
            lives.text = "Lives: " + numlives.ToString();
            collision.collider.gameObject.SetActive(false);
        }
        if(numlives <= 0){
             loseText.gameObject.SetActive(true);
             Destroy(gameObject);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {   
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3.5f), ForceMode2D.Impulse);
            }
        }
    }
    
}
