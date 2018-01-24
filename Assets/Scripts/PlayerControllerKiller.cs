using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerKiller : MonoBehaviour
{
    IInputSystem inputSystem;
    public float speed;
    public SpriteRenderer sprite;

    public string horizontalCtrl = "Horizontal_P1";
    public string verticalCtrl = "Vertical_P1";
    public string fire1 = "Fire1_P1";

    public GameObject timerScript;

    //public Vector2 vBefore;
    //public Vector2 vAfter;
    private Rigidbody2D rb2d;
    private Vector2 movementVector;
    private float inputHorizontal;
    private float inputVertical;
    private bool inputFire1;
    private RaycastHit2D rayHit;
    private Vector2 rayDir = Vector2.up;
    private float rayLen = 10;

    //private float threshold = 0.2f;

    void Start()
    {
        inputSystem = Manager.GetInputSystem();

        rb2d = GetComponent<Rigidbody2D>();
        sprite.color = Color.white;

        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void Update()
    {
        //Movement Input
        inputHorizontal = inputSystem.GetAxisHorizontal(horizontalCtrl);
        inputVertical = inputSystem.GetAxisVertical(verticalCtrl);

        //Raycast Inpit
        rayHit = Physics2D.Raycast(transform.position, rayDir, rayLen);
        //inputFire1 = Input.GetButtonDown(fire1);
        inputFire1 = inputSystem.GetFire1(fire1);

        //Raycast
        if (inputFire1 == true && rayHit.collider != null)
        {
            if(rayHit.collider.tag == "Player")
            {
                rayHit.collider.gameObject.GetComponent<PlayerDeath>().enabled = true;
                timerScript.SendMessage("KillerBonusTime");
            }
        }
    }

    void FixedUpdate()
    {
        //Movement
        Vector2 inputMovementVector = new Vector2(inputHorizontal, inputVertical);

        if (inputMovementVector != Vector2.zero && timerScript.GetComponent<Timer>().playerMayMove)
        {
            //if(Mathf.Abs(inputVertical) > threshold) || Mathf.Abs(inputHorizontal) > threshold)
            if (inputHorizontal > 0.2f)
            {
                movementVector = new Vector2(inputHorizontal, 0);
                transform.rotation = Quaternion.Euler(0, 0, 180);
                rayDir = Vector2.right;
            }
            if (inputHorizontal < -0.2f)
            {
                movementVector = new Vector2(inputHorizontal, 0);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rayDir = Vector2.left;
            }
            if (inputVertical > 0.2f)
            {
                movementVector = new Vector2(0, inputVertical);
                transform.rotation = Quaternion.Euler(0, 0, -90);
                rayDir = Vector2.up;
            }
            if (inputVertical < -0.2f)
            {
                movementVector = new Vector2(0, inputVertical);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                rayDir = Vector2.down;
            }
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(inputHorizontal, inputVertical, 0),new Vector3(0,1,0)) , 2);

        }
        else if (inputMovementVector == Vector2.zero)
        {
            movementVector = Vector2.zero;
        }

        if (movementVector != Vector2.zero)
        {
            rb2d.MovePosition(rb2d.position + movementVector * speed * Time.deltaTime);
        }
    }

    void OnCollisionExit2D()
    {
        Invoke("StopObject", 0.08f);
    }

    void StopObject()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}