using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using WiimoteApi;

public class Ball : MonoBehaviour
{
    private Vector3 POSITION_INIT;
    public float speed;
    private Rigidbody rigidBody;
    private bool thrown = false;
    public float horizontalSpeed;
    private Wiimote wiimote;
    private int index = 3;
    private Vector3[] terrain = new Vector3[]{
        new Vector3(-18, 0.1f,-10),
        new Vector3(-12, 0.1f,-10),
        new Vector3(-6, 0.1f,-10),
        new Vector3(0, 0.1f,-10),
        new Vector3(6, 0.1f,-10),
        new Vector3(12, 0.1f,-10),
        new Vector3(18, 0.1f,-10)
    };

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        POSITION_INIT = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        WiimoteManager.FindWiimotes();
        if (!WiimoteManager.HasWiimote())
        {
            if (!thrown)
            {
                float xAxis = Input.GetAxis("Horizontal");
                Vector3 position = transform.position;
                position.x += xAxis * horizontalSpeed;
                transform.position = position;
            }
            if (!thrown && Input.GetKeyDown(KeyCode.Space))
            {
                thrown = true;
                rigidBody.isKinematic = false;
                rigidBody.velocity = new Vector3(0, 0, speed);
            }
        }else{
            wiimote = WiimoteManager.Wiimotes[0];
            if (wiimote != null)
            {
                wiimote.SendPlayerLED(true, false, false, true);
                wiimote.SendDataReportMode(InputDataType.REPORT_EXT21);
            }

            int ret;
            do
            {
                ret = wiimote.ReadWiimoteData();
            } while (ret > 0);

            if (!thrown)
            {
                if (wiimote.Button.d_right)
                {
                    Vector3 position = transform.position;
                    position.x += horizontalSpeed;
                    position.x = (position.x > 0.9f) ? 0.9f : position.x;
                    transform.position =  position;
                }
                if (wiimote.Button.d_left)
                {
                    Vector3 position = transform.position;
                    position.x -= horizontalSpeed;
                    position.x = (position.x < -0.9f) ? -0.9f : position.x;
                    transform.position = position;
                }
            }
           
            // goal
            if (!thrown && wiimote.Button.b)
            {
                thrown = true;
                rigidBody.isKinematic = false;
                rigidBody.velocity = new Vector3(0, 0, speed);
            }
            
            // plus and minus  speed
            if (wiimote.Button.plus)
            {
                speed = (++speed > 20) ? 20 : speed;
            }

            if (wiimote.Button.minus)
            {
                speed = (--speed < 10) ? 10 : speed;
            }

            // change of terrain
            if (wiimote.Button.d_up)
            {
                index = Random.Range(0,7);//(++index > 6) ? 6 : index;
                transform.position = terrain[index];
            }

            if (wiimote.Button.d_down)
            {
                index = Random.Range(0,7);//(--index < 0) ? 0 : index;
                transform.position = terrain[index];
            }
        }
    }

    void FixedUpdate()
    {
        if (thrown && rigidBody.IsSleeping())
        {
            SceneManager.LoadScene("Scene 1");
        }
    }

    public void Reset(object _ball)
    {
        rigidBody.isKinematic = true;
        rigidBody.velocity = Vector3.zero;
    }
}