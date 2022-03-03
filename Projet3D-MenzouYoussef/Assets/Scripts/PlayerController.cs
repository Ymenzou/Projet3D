using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed, GravityModifier, JumpPower;
    public CharacterController CharCon; 
    private Vector3 MoveInput;
    public Transform CamTrans;
    public float MouseSensitivity;
    public bool invertX;
    public bool invertY;
    private bool canJump, canDoubleJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    //public Animator anim;
    public GameObject bullet;
    public Transform firePoint;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //MoveInput.x = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime; 
       // MoveInput.z = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
       float yStore = MoveInput.y;
       Vector3 VertMove = transform.forward * Input.GetAxis("Vertical");
       Vector3 HoriMove = transform.right * Input.GetAxis("Horizontal");
        //MoveInput = VertMove * MoveSpeed * Time.deltaTime;
        MoveInput = HoriMove + VertMove;
        MoveInput.Normalize();
        MoveInput = MoveInput * MoveSpeed;
        MoveInput.y = yStore;
        MoveInput.y += Physics.gravity.y * GravityModifier * Time.deltaTime;

        if(CharCon.isGrounded)
        {
            MoveInput.y = Physics.gravity.y * GravityModifier * Time.deltaTime;
        }
        canJump = Physics.OverlapSphere(groundCheckPoint.position, .25f, whatIsGround).Length > 0;

        //Jumping
        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            MoveInput.y = JumpPower;
        }

        CharCon.Move(MoveInput * Time.deltaTime);

        //controler la rotation de la camera
        Vector2 MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * MouseSensitivity ;

        if(invertX)
        {
            MouseInput.x = -MouseInput.x;
        }
        if(invertY)
        {
            MouseInput.y = -MouseInput.y;
        }
        transform.rotation =  Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + MouseInput.x, transform.rotation.eulerAngles.z);
        CamTrans.rotation = Quaternion.Euler(CamTrans.rotation.eulerAngles + new Vector3(-MouseInput.y, 0f, 0f));

        //shooting
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(CamTrans.position, CamTrans.forward, out hit, 50f))
            {
                firePoint.LookAt(hit.point);
            }
            else
            {
                firePoint.LookAt(CamTrans.position + (CamTrans.forward * 30f));
            }
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    }
}
