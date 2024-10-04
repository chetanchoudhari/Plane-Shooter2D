using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyScrpit : MonoBehaviour
{
    
    public float speed = 1f;
    public Transform []gunPoint;
    public GameObject enemyBullet;
    public float EnemyBulletSpwamTime = 0.5f;
    public GameObject enemyFlash;
    public GameObject enemyExplosionPrefab;
    public float health = 10f;
    public HealthBar healthBar;
    float barSize = 1f;
    float damage = 0;
    public GameObject damageEffect;
    public GameObject coinPrefab;

    public AudioClip bulletSound;
    public AudioClip damageSound;
    public AudioClip explosionSound;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyShooting());
        enemyFlash.SetActive(false);
        damage = barSize / health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            DamageHealthBar();
            audioSource.PlayOneShot(damageSound);
            Destroy(collision.gameObject);
            GameObject damageVFX = Instantiate(damageEffect,collision.transform.position, Quaternion.identity);
            Destroy(damageVFX,0.05f);
         
            if (health <= 0)
            {
                AudioSource.PlayClipAtPoint(explosionSound,Camera.main.transform.position,0.5f);
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                GameObject enemyExplosion = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(enemyExplosion, 0.4f);

                
            }

        }

    }
    void DamageHealthBar() {

        if (health > 0) 
        {

            health -= 1;
            barSize = barSize - damage;
            healthBar.setSize(barSize);
        
        
        
        }
    }
    void EnemyFIre()
    {
        for (int i = 0; i < gunPoint.Length; i++)
        {
            Instantiate(enemyBullet, gunPoint[i].position, Quaternion.identity);

        }

        //Instantiate(enemyBullet, gunPoint1.position,Quaternion.identity);
        //Instantiate(enemyBullet,gunPoint2.position,Quaternion.identity);

    }
    IEnumerator EnemyShooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(EnemyBulletSpwamTime);
            EnemyFIre();//yield return 
            audioSource.PlayOneShot(bulletSound, 0.5f);
            enemyFlash.SetActive(true);
            yield return new WaitForSeconds(0.04f);
            enemyFlash.SetActive(false);
        }
    }
}
