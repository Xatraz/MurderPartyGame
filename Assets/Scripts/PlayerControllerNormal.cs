using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerNormal : MonoBehaviour {

    IInputSystem inputSystem;

    public float speed;
    public string horizontalCtrl = "Horizontal_P1";
    public string verticalCtrl = "Vertical_P1";

    private Rigidbody2D rb2d;
    public Vector2 movementVector;

    private float inputHorizontal;
    private float inputVertical;
    private Vector2 inputMovementVector;

    private bool leftFrontBlocked;
    private bool leftBackBlocked;
    private bool leftBlocked;
    private bool rightFrontBlocked;
    private bool rightBackBlocked;
    private bool rightBlocked;
    private int playersInTrigger;
    private bool moving;

    private Vector2 moveLeft;
    private Vector2 moveRight;
    private Vector2 moveDown;
    private Vector2 moveUp;

    private Vector2 oldPos;
    private Vector2 newPos;

    private float timeBetweenOldNew = 2f;

    private float startSpeed;

    public GameObject timerScript;


    //private Vector2 touchOrigin = -Vector2.one;

    void Start()
    {
        inputSystem = Manager.GetInputSystem();
        
        rb2d = GetComponent<Rigidbody2D>();
        startSpeed = speed;

        moveLeft = new Vector2(-1, 0);
        moveRight = new Vector2(1, 0);
        moveDown = new Vector2(0, -1);
        moveUp = new Vector2(0, 1);
    }
    void Update()
    {

        inputHorizontal = inputSystem.GetAxisHorizontal(horizontalCtrl);
        inputVertical = inputSystem.GetAxisVertical(verticalCtrl);

        checkBlocked();

/*#if UNITY_ANDROID
        //touch input
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];
            if (myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;
                float x = touchEnd.x - touchOrigin.x;
                float y = touchEnd.y - touchOrigin.y;
                touchOrigin.x = -1;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    inputHorizontal = x > 0 ? 1 : -1;
                }
                else
                {
                    inputVertical = y > 0 ? 1 : -1;
                }
            }
        }
#endif*/


    }
    void FixedUpdate()
    {
        inputMovementVector = new Vector2(inputHorizontal, inputVertical);            

        // Is input (lower)higher then (-)0.8 on any axis
        if (inputMovementVector != Vector2.zero && timerScript.GetComponent<Timer>().playerMayMove)
        {
            //Left
            if (inputHorizontal < -0.8f && (leftBlocked == false || rightBlocked == false))
            {
                movementVector = moveLeft;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            //Right
            if (inputHorizontal > 0.8f && (leftBlocked == false || rightBlocked == false))
            {
                movementVector = moveRight;
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            //Down
            if (inputVertical < -0.8f && (leftBlocked == false || rightBlocked == false))
            {
                movementVector = moveDown;
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            //Up
            if (inputVertical > 0.8f && (leftBlocked == false || rightBlocked == false))
            {
                movementVector = moveUp;
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
        }

        //if (gameObject.activeSelf)
        //    StartCoroutine(CheckMoving());

        //moving the player | Faster movement if in collider
        //playersInTrigger = transform.GetChild(4).GetComponent<TriggerPlayer>().playersInTrigger;

        //speed = startSpeed + playersInTrigger; // * bonusValue ;
        //Debug.Log(speed);

        if (movementVector != Vector2.zero)
            rb2d.MovePosition(rb2d.position + movementVector * speed * Time.deltaTime);
    }

    void OnCollisionExit2D()
    {
        Invoke("StopObject", 0.08f);
    }

    void StopObject()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    }

    void checkBlocked()
    {
        leftFrontBlocked = transform.GetChild(0).GetComponent<Trigger>().isTriggered;
        leftBackBlocked = transform.GetChild(1).GetComponent<Trigger>().isTriggered;
        rightFrontBlocked = transform.GetChild(2).GetComponent<Trigger>().isTriggered;
        rightBackBlocked = transform.GetChild(3).GetComponent<Trigger>().isTriggered;

        if (leftFrontBlocked == false && leftBackBlocked == false)
            leftBlocked = false;
        else
            leftBlocked = true;
        if (rightFrontBlocked == false && rightBackBlocked == false)
            rightBlocked = false;
        else
            rightBlocked = true;
    }
   

    IEnumerator CheckMoving()
    {
        oldPos = rb2d.position;
        yield return new WaitForSeconds(timeBetweenOldNew);
        newPos = rb2d.position;
        if (oldPos == newPos)
        {
            newPos = rb2d.position;
            if (oldPos == newPos)
                gameObject.GetComponent<PlayerDeath>().enabled = true;
        }
        yield return null;
    }
}