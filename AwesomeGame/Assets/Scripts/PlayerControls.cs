using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;

    Vector3 myVelocity;
    float myGravity = -9.81f;
    bool isPlayerGrounded;
    bool isPressEAvailable = false;
    bool isControlLocked = true;
    bool isJumping = false;
    GameObject myObjectToInteract;
    Vector3 tmp;
    //AudioSource mySteps;
    void Awake()
    {
        //mySteps = GetComponent<AudioSource>();
        //mySteps.Pause();
    }
    void Start()
    {
        //mySteps = GetComponent<AudioSource>();
        isControlLocked = false;
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


            if (Input.GetButtonDown("Jump") & isPlayerGrounded)
            {
                isJumping = true;
                myGravity = 0f;
                //myPlayerAnimator.SetBool("isPlayerJumping", true);
                StartCoroutine("JumpDelayCoroutine");
                tmp = gameObject.transform.position;

            }
            /*            if (Input.GetButtonDown("Cancel")) #menu call
                        {
                            FindObjectOfType<GameStatus>().PauseScreen();
                        }*/
        }

        if (isJumping)
        {
            myDirection.z = 0f;
            myDirection.x = 0f;
            myDirection.y = myJumpHeight;
            myController.Move(myDirection.normalized * myJumpHeight * Time.deltaTime);
        }
    }
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


    public void ActivateControls()
    {
        isControlLocked = false;
    }

    /*private void OnTriggerEnter(Collider other)  if collision with deadly stuff
    {
        if (other.gameObject.tag == "Cop")
        {
            FindObjectOfType<GameStatus>().LooseScreen();
        }
    }*/
}
