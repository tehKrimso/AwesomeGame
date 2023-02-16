using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private GameObject playerAlive = null;
    private bool isPlayerAlive = false;
    private Vector3 targetPoint;
    private Quaternion targetRotation;

    //Для работы лазера
    [SerializeField] GameObject enemyLaser;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectilePriodTime = 0.3f;
    [SerializeField] AudioClip playerShot;
    [SerializeField][Range(0, 1)] float playerShotVolume = 0.5f;
    [SerializeField] Transform placeToSpawnLaser;
    private float shotCounter;
    private bool _isShooting;

    void Start()
    {
        shotCounter = projectilePriodTime;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerAlive = GameObject.FindGameObjectWithTag("Player");   //Хотел сделать меньше вызовов на поиск по тегу, пока лень
            isPlayerAlive = true;
            Debug.Log("Found");
        }
    }
/*    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerAlive = other.gameObject;
            isPlayerAlive = true;
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        //CountDownAndShot();
        Shoot();
    }

    private void CountDownAndShot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Shoot();
            shotCounter = projectilePriodTime;
        }
    }
    private void Shoot()
    {
        if (isPlayerAlive && !_isShooting)
        {
            _isShooting = true;
            StartCoroutine("EnemyFireNonStop");
        }
        else
        {
            _isShooting = false;
            StopCoroutine("EnemyFireNonStop");
        }
    }
    private void FixedUpdate()
    {
        if (isPlayerAlive)
        {
            targetPoint = new Vector3(playerAlive.transform.position.x, transform.position.y, playerAlive.transform.position.z) - transform.position;
            targetRotation = Quaternion.LookRotation(-targetPoint, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
        }
    }

    IEnumerator EnemyFireNonStop()
    {
        GameObject laser = Instantiate(enemyLaser, placeToSpawnLaser.position, transform.rotation) as GameObject;
        laser.transform.Rotate(new Vector3(90, 0, 0));
        AudioSource.PlayClipAtPoint(playerShot, Camera.main.transform.position, playerShotVolume);
        laser.GetComponent<Rigidbody>().velocity = -transform.forward * projectileSpeed;
        Destroy(laser, 1f);
        yield return new WaitForSeconds(projectilePriodTime);


    }
}
