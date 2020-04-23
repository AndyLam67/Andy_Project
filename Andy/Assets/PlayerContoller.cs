using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    const float constGravity = -9.81f;
    public float moveSpeed = 2.0f;
    public Vector3 gravity;
    CharacterController Controller;
    public bool onGround;
    public bool floating;
    public float onAirTime = 0.0f;
    public GameObject groundCheck;
    public float jumpHeight = 1000.0f;
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Control();
    }

    void Control()
    {
        onGround = Physics.CheckSphere(groundCheck.transform.position, 0.4f, LayerMask.GetMask("Ground"));
        Vector3 Move = new Vector3();
        Move += transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        Move += transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        //Jump
        if (Input.GetKey(KeyCode.Space))
        {
            //https://math.stackexchange.com/questions/222578/how-to-calculate-initial-jump-velocity-for-reaching-specific-height
            //v=√2gh
            gravity.y = Mathf.Sqrt(jumpHeight * -2 * constGravity) *Time.deltaTime;
            onAirTime = 0;
        }
        else
        {
            //v = 1/2*g*t^2      
            if (!onGround)
            {
                onAirTime += Time.deltaTime;
                gravity.y = 0.5f * constGravity * onAirTime * onAirTime;
            }
        }
        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
            Move *= 2.0f;

        Controller.Move(Move);
        Controller.Move(gravity);
    }
}
