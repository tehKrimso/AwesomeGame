using System.Collections;
using System.Collections.Generic;
using Mechanics;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] CharacterController myController;
    [SerializeField] float myPlayerSpeed = 6f;
    [SerializeField] float myPlayerTurnTime = 0.1f;
    [SerializeField] float myJumpHeight = 10f;
    [SerializeField] float myJumpSpeed = 100f;
    [SerializeField] Transform myCamera;
    float turnSmoothVelocityHolder;
    //[SerializeField] Animator myPlayerAnimator; if there will be animations
    //Для работы лазера
    [SerializeField] GameObject playerLaser;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectilePriodTime = 0.3f;
    [SerializeField] AudioClip playerShot;
    [SerializeField] GameObject playerGun;
    [SerializeField][Range(0, 1)] float playerShotVolume = 0.5f;
    [SerializeField] Transform placeToSpawnLaser;
    //Для проверок на прыжок
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;

    public UnityEvent<Vector3> PlayerIsDead = new UnityEvent<Vector3>();
    
    Vector3 myVelocity;
    float myGravity = -9.81f;
    bool isPlayerGrounded;
    bool isPressEAvailable = false;
    bool isControlLocked = true;
    bool isJumping = false;
    GameObject myObjectToInteract;
    Vector3 tmp;
    //AudioSource mySteps;
    
    //
    private CheckForCircuitOnDeath _checkForCircuit;
    //
    
    void Awake()
    {
        //mySteps = GetComponent<AudioSource>();
        //mySteps.Pause();
        myCamera = Camera.main.transform;
        _checkForCircuit = GetComponent<CheckForCircuitOnDeath>();

    }
    void Start()
    {
        //mySteps = GetComponent<AudioSource>();
        isControlLocked = false;

        //myCamera = Camera.main.transform.parent;
        //mySteps.pitch = 1.4f;
        //if (isControlLocked)
        //{
        //    mySteps.Pause();
        //}
    }
    void Update()
    {
        isPlayerGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isPlayerGrounded && myVelocity.y < 0)
        {
            myVelocity.y = -1f;
        }
        myVelocity.y += myGravity * Time.deltaTime;
        myController.Move(myVelocity * Time.deltaTime);
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var myDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (!isControlLocked)
        {
            if (myDirection.magnitude >= 0.1)
            {
                var myAngleToLook = Mathf.Atan2(myDirection.x, myDirection.z) * Mathf.Rad2Deg + myCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, myAngleToLook, ref turnSmoothVelocityHolder, myPlayerTurnTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                var moveDirection = Quaternion.Euler(0f, myAngleToLook, 0f) * Vector3.forward;
                float speed = 3f;
                if (isJumping)
                {
                    speed = myJumpSpeed;
                }
                else
                {
                    speed = myPlayerSpeed;
                }
                myController.Move(moveDirection * speed * Time.deltaTime);
                //myPlayerAnimator.SetBool("isPlayerWalking", true);

                //mySteps.UnPause();
            }

            else
            {
                //myPlayerAnimator.SetBool("isPlayerWalking", false);
                //mySteps.Pause();

            }

            Jump();

            Shoot();

        }

        if (isJumping)
        {
            myDirection.z = 0f;
            myDirection.x = 0f;
            myDirection.y = myJumpHeight;
            myController.Move(myDirection.normalized * myJumpHeight * Time.deltaTime);
        }
    }

    public void MoveTo(Vector3 position)
    {
        GetComponent<CharacterController>().enabled = false;
        transform.position = position;
        GetComponent<CharacterController>().enabled = true;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") & isPlayerGrounded)
        {
            isJumping = true;
            myGravity = 0f;
            //myPlayerAnimator.SetBool("isPlayerJumping", true);
            StartCoroutine("JumpDelayCoroutine");
            tmp = gameObject.transform.position;

        }
    }

    //Прыжок кулдаун
    IEnumerator JumpDelayCoroutine()
    {
        //myPlayerAnimator.SetBool("isPlayerJumping", true);
        isJumping = true;
        yield return new WaitForSeconds(0.6f);
        //myPlayerAnimator.SetBool("isPlayerJumping", false);
        isJumping = false;
        myGravity = -9.81f;
        //myPlayerSpeed = 6f;
    }
    //Огонь лазером
    IEnumerator FireNonStop()
    {
        while (true)
        {
            GameObject laser = Instantiate(playerLaser, placeToSpawnLaser.position, transform.rotation) as GameObject;
            laser.transform.Rotate(new Vector3(90, 0, 0));
            //AudioSource.PlayClipAtPoint(playerShot, Camera.main.transform.position, playerShotVolume);
            playerGun.GetComponent<AudioSource>().Play();

            laser.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
            Destroy(laser, 5f);
            yield return new WaitForSeconds(projectilePriodTime);
        }

    }
    private void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine("FireNonStop");
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine("FireNonStop");
            //StopAllCoroutines();
        }
    }

    public void ActivateControls()
    {
        isControlLocked = false;
    }

    private void OnTriggerEnter(Collider other) // if collision with deadly stuff
    {
        if (other.gameObject.CompareTag("EnemyLaser"))
        {
            OnPlayerDeath();

            //FindObjectsOfType<TurretController>().SetPlayerStatus(false);
        }
    }

    public void OnPlayerDeath()
    {
        PlayerIsDead?.Invoke(transform.position);

        _checkForCircuit.CheckCircuits();
    }
}
