using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
// Start is called before the first frame update
    UnityEngine.AI.NavMeshAgent m_enemy = null;

    //컨트롤 선언
    [SerializeField] CameraControl camera = null;
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject Ride_pos = null;
    private Rigidbody rb;

    int m_count = 0;
    int drone_control = 0;

    private void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Bullet_riding":
                rb = GetComponent<Rigidbody> ();
                GameObject.Find("Sub Camera").transform.position = transform.position;
                camera.SetSub();
                rb.velocity = new Vector3(0, 0, 0);
                camera.control = 1;
                drone_control = 1;
                break;
        }
        
    }
    
    private void Move()
    {
        float power = 1.0f;

        if (Input.GetKey(KeyCode.W))  // ↑ 방향키를 누를 때
        {
            // Translate는 현재 위치에서 ()안에 들어간 값만큼 값을 변화시킨다.
            transform.Translate(Vector3.forward * 1 * Time.deltaTime * 10);
            // Time.deltaTime은 모든 기기(컴퓨터, OS를 망론하고)에 같은 속도로 움직이도록 하기 위한 것
        }

        if (Input.GetKey(KeyCode.S))  // ↑ 방향키를 누를 때
        {
            // Translate는 현재 위치에서 ()안에 들어간 값만큼 값을 변화시킨다.
            transform.Translate(Vector3.back * 1 * Time.deltaTime * 10);
            // Time.deltaTime은 모든 기기(컴퓨터, OS를 망론하고)에 같은 속도로 움직이도록 하기 위한 것
        }

        if (Input.GetKey(KeyCode.D))  // → 방향키를 누를 때
        {
            transform.Rotate(0.0f, 180.0f * Time.deltaTime, 0.0f);
        }

        if (Input.GetKey(KeyCode.A))  // ← 방향키를 누를 때
        {
            transform.Rotate(0.0f, -180.0f * Time.deltaTime, 0.0f);
        }
        if (Input.GetKey(KeyCode.F))
        {
            player.transform.position = Ride_pos.transform.position;
            player.SetActive(true);
            camera.control = 0;
            drone_control = 0;
            camera.SetMain();
        }
        
        //스페이스바 버튼 누를때
        if ( Input.GetKey ( KeyCode.Space ))
        {
            power += 2.0f; 
            // Translate는 현재 위치에서 ()안에 들어간 값만큼 값을 변화시킨다.
            transform.Translate(Vector3.up * 3f * Time.deltaTime* power);
            // Time.deltaTime은 모든 기기(컴퓨터, OS를 망론하고)에 같은 속도로 움직이도록 하기 위한 것
        }        
        //스페이스바 버튼 뗄때
        if ( Input.GetKeyUp ( KeyCode.Space ))
        {
            power = 0;
        }

        //스페이스바 버튼 누를때
        if ( Input.GetKey ( KeyCode.LeftShift ))
        {
            power -= 2.0f; 
            // Translate는 현재 위치에서 ()안에 들어간 값만큼 값을 변화시킨다.
            transform.Translate(Vector3.up * 3f * Time.deltaTime* power);
            // Time.deltaTime은 모든 기기(컴퓨터, OS를 망론하고)에 같은 속도로 움직이도록 하기 위한 것
        }        
        //스페이스바 버튼 뗄때
        if ( Input.GetKeyUp ( KeyCode.LeftShift ))
        {
            power = 0;
        }
        
    }

    void Start()
    {
        m_enemy = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(camera.control == 1 && drone_control == 1)
        {
            player.SetActive(false);
            Move();
            GameObject.Find("Sub Camera").transform.position = transform.position;
            GameObject.Find("Sub Camera").transform.rotation = transform.rotation;
        }
    }
}
