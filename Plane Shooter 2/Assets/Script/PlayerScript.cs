using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 10f;
    public float padding = 0.8f;
    public GameObject Explosion;
    public CoinCount coinCountScript;
    public float health = 20f;
    float barFillAmount = 1f;
    float damage = 0;
    public PlayerHealthBarScript playerHealthbar;
    public GameObject damageEffect;
    float maxX;
    float minY;
    float maxY;
    float minX;
    public GameController gameController;
    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip explosionSound;
    public AudioClip coinSound;


    // Start is called before the first frame update
    void Start()
    {
        FindBoundaries();
        damage = barFillAmount / health;

    }
    void FindBoundaries()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding; // .x means only x value assign to minX;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0,0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0,1, 0)).y -  padding;



    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;//store the vzlue come from the x and y axis
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed; //Input For PLayer

        float newXpos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        float newYpos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);

        transform.position = new Vector2(newXpos , newYpos);

        if (Input.GetMouseButton(0)) 
        {

           Vector2 newPos = Camera.main.ScreenToWorldPoint(new Vector2 (Input.mousePosition.x, Input.mousePosition.y));
            transform.position = Vector2.Lerp(transform.position,newPos, 10 * Time.deltaTime);
            a
        }
        
        

        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag== "EnemyBullet")
        {
            audioSource.PlayOneShot(damageSound,0.3f);
            DamagePlayerHealthbar();
            Destroy(collision.gameObject);
            GameObject damageVfx = Instantiate(damageEffect,collision.transform.position,Quaternion.identity);
            Destroy(damageVfx,0.05f);
            if (health <= 0)
            {
                AudioSource.PlayClipAtPoint(explosionSound,Camera.main.transform.position,0.5f);
                gameController.GameOver();
                Destroy(gameObject);
                GameObject blast = Instantiate(Explosion, transform.position, Quaternion.identity);
                Destroy(blast, 0.05f);
            }

        }
        if (collision.gameObject.tag == "Coin")
        {

            audioSource.PlayOneShot(coinSound, 0.5f);
            coinCountScript.AddCount();
            Destroy(collision.gameObject);
        }
    }
    void DamagePlayerHealthbar()
    {
        if (health > 0)
        {
            health -= 1;
            barFillAmount = barFillAmount - damage;
            playerHealthbar.SetAmount(barFillAmount);

        }
    }
}
//public static float GetAxis(string axisName);