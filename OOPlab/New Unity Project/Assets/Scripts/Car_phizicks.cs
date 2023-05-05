using System.Collections;
using System.Diagnostics;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Car_phizicks : MonoBehaviour
{


    public GameObject win_panel;
    
    public int current_level;


    //public static  float fuel_current;
    //public static float fuel_start = 3;
    //public float fuel_consumption = 0.1f;
    public int angle_speed = 35;

    public float fuel_size;
    public float fuel_usage;
    private float fuel_current;

    public GameObject fuel_progress_bar;




    public Rigidbody2D rb;

    
    

    public KeyCode Gas = KeyCode.D;
    public KeyCode Stop = KeyCode.A;
    public KeyCode Fast_Stop = KeyCode.Space;
    public KeyCode Rotate_to_right = KeyCode.W;
    public KeyCode Rotate_to_left = KeyCode.S;

    

    WheelJoint2D[] wheelJoints;

    JointMotor2D back_wheel;
    JointMotor2D front_wheel;



    private float max_speed = -3500;
    private float max_back_speed = 2500;

    private float get_Stop = -700;

    private float acceleration = 700;
    private float deacceleration = -700;

    

    public float brake_force = 450f;
    private float gravity = 9.8f;

    public float axiesZ = 0f;

    

    public bool ground = false;

    public bool bridge = false;

    public LayerMask map;
    public LayerMask br;

    public Transform back_wheel_Layer;


    

    void Start()
    {
        
        fuel_current = fuel_size;

        wheelJoints = gameObject.GetComponents<WheelJoint2D>();

        back_wheel = wheelJoints[1].motor;
        front_wheel = wheelJoints[0].motor;

        
    }

    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    private void Update()
    {

        ground  = Physics2D.OverlapCircle(back_wheel_Layer.transform.position, 0.25f, map);
        bridge = Physics2D.OverlapCircle(back_wheel_Layer.transform.position, 0.25f, br);

    }
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



    private void FixedUpdate()
    {

        
        front_wheel.motorSpeed = back_wheel.motorSpeed;

        axiesZ = transform.localEulerAngles.z;

        if (axiesZ >= 180)
        {
            axiesZ = axiesZ - 360;
        }
        if (fuel_current <= 0)
        {
            if (back_wheel.motorSpeed < 0)
            {
                back_wheel.motorSpeed += 500;
            }
            else if (back_wheel.motorSpeed > 0)
            {
                back_wheel.motorSpeed -= 500;
            }
            back_wheel.motorSpeed = 0;

        }
        else if (fuel_current >= 0)
        {
            fuel_current -= fuel_usage * Time.deltaTime;
            if (ground == true || bridge == true)
            {


                if (Input.GetKey(Gas) == true)
                {
                    back_wheel.motorSpeed = Mathf.Clamp(back_wheel.motorSpeed - (acceleration - gravity * Mathf.PI * (axiesZ / 2)) * Time.deltaTime, max_speed, max_back_speed);

                    if (back_wheel.motorSpeed < -1400 && Input.GetKey(Stop) == true)
                    {
                        back_wheel.motorSpeed = back_wheel.motorSpeed + 100;
                    }

                }

                if (Input.GetKey(Stop) == true && Input.GetKey(Fast_Stop) == false)
                {
                    back_wheel.motorSpeed = Mathf.Clamp(back_wheel.motorSpeed - (deacceleration - 200 - gravity * Mathf.PI * (axiesZ / 2)) * Time.deltaTime, max_speed, max_back_speed);

                }

                if ((Input.GetKey(Gas) == false && back_wheel.motorSpeed < 0) || (Input.GetKey(Gas) == false && back_wheel.motorSpeed == 0 && axiesZ < 0))
                {
                    back_wheel.motorSpeed = Mathf.Clamp(back_wheel.motorSpeed - (get_Stop - gravity * Mathf.PI * (axiesZ / 2)) * Time.deltaTime, max_speed, 0);
                }
                else if ((Input.GetKey(Gas) == false && back_wheel.motorSpeed > 0) || (Input.GetKey(Gas) == false && back_wheel.motorSpeed == 0 && axiesZ > 0))
                {
                    back_wheel.motorSpeed = Mathf.Clamp(back_wheel.motorSpeed - (-deacceleration - gravity * Mathf.PI * (axiesZ / 2)) * Time.deltaTime, 0, max_back_speed);
                }


                if (back_wheel.motorSpeed > 800 && Input.GetKey(Gas) == true)
                {
                    back_wheel.motorSpeed = back_wheel.motorSpeed - 900;
                }


                if (back_wheel.motorSpeed < -800 && Input.GetKey(Stop) == true)
                {
                    back_wheel.motorSpeed = back_wheel.motorSpeed + 900;
                }
            }
            else
            {
                if (back_wheel.motorSpeed > 0)
                {

                    back_wheel.motorSpeed = Mathf.Clamp(back_wheel.motorSpeed - brake_force * Time.deltaTime, 0, max_back_speed);
                }
                else if (back_wheel.motorSpeed < 0)
                {

                    back_wheel.motorSpeed = Mathf.Clamp(back_wheel.motorSpeed + brake_force * Time.deltaTime, max_speed, 0);
                }
            }

            if (Input.GetKey(Gas) == true && ground == false)
            {

                back_wheel.motorSpeed = Mathf.Clamp(back_wheel.motorSpeed - (acceleration - gravity * Mathf.PI * (axiesZ / 2)) * Time.deltaTime, max_speed, max_back_speed);

                if (back_wheel.motorSpeed < -1400 && Input.GetKey(Stop) == true)
                {
                    back_wheel.motorSpeed = back_wheel.motorSpeed + 200;
                }
            }

            if (Input.GetKey(Rotate_to_right) == true || (Input.GetKey(Rotate_to_right) == true && Input.GetKey(Gas) == true))
            {

                transform.Rotate(-Vector3.forward * angle_speed * Time.deltaTime, Space.Self);


                //transform.Rotate(0, 0, -45f * Time.deltaTime);



            }
            else if (Input.GetKey(Rotate_to_left) == true || (Input.GetKey(Rotate_to_left) == true && Input.GetKey(Stop) == true))
            {


                transform.Rotate(Vector3.forward * angle_speed * Time.deltaTime, Space.Self);


            }


            if (Input.GetKey(Fast_Stop) == true && back_wheel.motorSpeed > 0)
            {

                back_wheel.motorSpeed = Mathf.Clamp(back_wheel.motorSpeed - 1000 * Time.deltaTime, 0, max_back_speed);
            }
            else if (Input.GetKey(Fast_Stop) == true && back_wheel.motorSpeed < 0)
            {

                back_wheel.motorSpeed = Mathf.Clamp(back_wheel.motorSpeed + 1000 * Time.deltaTime, max_speed, 0);
            }
        }
        

        fuel_progress_bar.transform.localScale = new Vector2(fuel_current / fuel_size, 1);

        wheelJoints[1].motor = back_wheel;
        wheelJoints[0].motor = front_wheel;



    }
    


    



    private void OnTriggerEnter2D(Collider2D trigger)
    {


        if(trigger.gameObject.tag == "coins")
        {
            Destroy(trigger.gameObject);
        }


        

        //if (collision.gameObject.CompareTag("Land"))
        //{


        //    StartCoroutine(waiter(collision));
        //}


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //if (collision.gameObject.CompareTag("Fuel"))
        //{

        //    fuel_current = fuel_start;

        //    Destroy(collision.gameObject);
        //}

        if (collision.gameObject.CompareTag("Finish"))
        {


            PlayerPrefs.SetInt(current_level.ToString(), 1);
            PlayerPrefs.Save();

            win_panel.SetActive(true);
        }

        if (collision.gameObject.CompareTag("InvizVall"))
        {
            back_wheel.motorSpeed = 0;
        }

    }

    //.............................................................................
    private void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    IEnumerator waiter(Collider2D collision)
    {
        yield return new WaitForSeconds(5);

        if (this.GetComponentInChildren<PolygonCollider2D>().IsTouching(collision))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }
    }


}
