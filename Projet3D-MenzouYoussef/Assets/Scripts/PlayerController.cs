using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;
    public CharacterController CharCon; 
    private Vector3 MoveInput;
    public Transform CamTrans;
    public float MouseSensitivity;
    public bool invertX;
    public bool invertY;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //MoveInput.x = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime; 
       // MoveInput.z = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
       Vector3 VertMove = transform.forward * Input.GetAxis("Vertical");
       Vector3 HoriMove = transform.right * Input.GetAxis("Horizontal");
        //MoveInput = VertMove * MoveSpeed * Time.deltaTime;
        MoveInput = HoriMove + VertMove;
        MoveInput.Normalize();
        MoveInput = MoveInput * MoveSpeed;

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
    }
}