using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContoller : MonoBehaviour
{
    CharacterController controller;
    public GameObject groundCheck;
    public Text debug1;
    public Text debug2;

    const float constGravity = -9.81f;
    const float jumpSpeed = 1.0f;
    const float fallSpeed = 4.0f;
    const float moveSpeed = 2.0f;

    public Vector3 gravity;
    public bool onGround;
    public bool flying;
    public bool floating;
    public float onAirTime = 0.0f;
    public float jumpHeight = 10.0f;
    public float power = 0f;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Control();
    }

    void Control()
    {
        onGround = Physics.CheckSphere(groundCheck.transform.position, 0.4f, LayerMask.GetMask("Ground"));
        Vector3 Move = new Vector3();
        if (Input.GetKey(KeyCode.W)) Move += transform.forward * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) Move -= transform.forward * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) Move -= transform.right * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) Move += transform.right * moveSpeed * Time.deltaTime;
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            power = jumpSpeed;
            gravity.y = power;
        }
        if (!onGround)
        {
            power -= Time.deltaTime * fallSpeed;
            gravity.y = power;
        }
        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
            Move *= 2.0f;

        controller.Move(Move);
        controller.Move(gravity);
    }
}
