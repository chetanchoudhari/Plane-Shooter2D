using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Shooting : MonoBehaviour
{
    public GameObject playerBullet;
    public GameObject Flash;

    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public float bulletspwamTime;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shoot());
        Flash.SetActive(false);// disable when start 

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire1"))//input given by user
        //{
        //    Instantiate(playerBullet, spawnPoint1.position, Quaternion.identity);
        //    //whenever no need to change rotation Quaternion used
        //    Instantiate(playerBullet, spawnPoint2.position, Quaternion.identity); 
        //}
       


    }
    void Fire()
    {
        Instantiate(playerBullet, spawnPoint1.position, Quaternion.identity);
        //whenever no need to change rotation Quaternion used
        Instantiate(playerBullet, spawnPoint2.position, Quaternion.identity);
    }

    IEnumerator Shoot()// stat coroutine
    {
        while (true)
        {
            yield return new WaitForSeconds(bulletspwamTime);
            Fire();
            Flash.SetActive(true);// enable when fire 
            audioSource.Play();
            //put a audio source on GameObject and play it is in coroutine //audiosource.play is used 


            yield return new WaitForSeconds(0.04f);//to put some time yeild return is used
            Flash.SetActive (false);

        }
        //StartCoroutine(Shoot()); // use for loop without while loop


    }

}
