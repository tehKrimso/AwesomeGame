using System.Collections;
using System.Collections.Generic;
using Infrastructure;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private Game _gameController;
    
    private GameObject playerAlive = null;
    private bool isPlayerAlive = false;
    private Vector3 targetPoint;
    private Quaternion targetRotation;

    //??? ?????? ??????
    [SerializeField] GameObject enemyLaser;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectilePriodTime = 0.3f;
    [SerializeField] AudioClip playerShot;
    [SerializeField] GameObject turretGun;
    [SerializeField][Range(0, 1)] float playerShotVolume = 0.5f;
    [SerializeField] Transform placeToSpawnLaser;
    private float shotCounter;
    private bool _isShooting;

    private Coroutine _shooting;
    private Coroutine _looking;

    void Start()
    {
        _gameController = GameObject.Find("GameController").GetComponent<Game>();
        
        shotCounter = projectilePriodTime;
        
        _gameController.LevelStart.AddListener(FindTarget);
        
    }

    private void FindTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerAlive =
                GameObject.FindGameObjectWithTag("Player"); //????? ??????? ?????? ??????? ?? ????? ?? ????, ???? ????
            isPlayerAlive = true;
            Debug.Log("Found");
            
            playerAlive.GetComponent<PlayerControls>().PlayerIsDead.AddListener(OnPlayerDeath);
        }
    }

    private void OnPlayerDeath(Vector3 deathPosition)
    {
        isPlayerAlive = false;
        _isShooting = false;
        
        playerAlive.GetComponent<PlayerControls>().PlayerIsDead.RemoveListener(OnPlayerDeath);
        playerAlive = null;
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
        if (isPlayerAlive && _looking != null)
        {
            StopCoroutine(_looking);
            _looking = null;
        }
        if (!isPlayerAlive && _shooting != null)
        {
            StopCoroutine(_shooting);
            _shooting = null;
            _isShooting = false;
            return;
        }

        if (!_isShooting && isPlayerAlive)
        {
            _isShooting = true;
            _shooting = StartCoroutine("EnemyFireNonStop");
        }
        if (!isPlayerAlive && _shooting == null && _looking == null)
        {
            _looking = StartCoroutine("LookingForPlayer");
        }
        
        
        /*if (isPlayerAlive)
        {
            
        }
        else
        {
            if (_isShooting)
            {
                _isShooting = false;
                StopCoroutine("EnemyFireNonStop");
            }
        }*/
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
        while (true)
        {
            GameObject laser = Instantiate(enemyLaser, placeToSpawnLaser.position, transform.rotation) as GameObject;
            laser.transform.Rotate(new Vector3(90, 0, 0));
            //AudioSource.PlayClipAtPoint(playerShot, Camera.main.transform.position, playerShotVolume);
            turretGun.GetComponent<AudioSource>().Play();
            laser.GetComponent<Rigidbody>().velocity = -transform.forward * projectileSpeed;
            Destroy(laser, 1f);
            yield return new WaitForSeconds(projectilePriodTime);
        }
        
    }

    IEnumerator LookingForPlayer()
    {
        while (true)
        {
            FindTarget();
            yield return new WaitForSeconds(1f);
        }
    }

    public void SetPlayerStatus(bool setBool)
    {
        isPlayerAlive = setBool;
    }
}
