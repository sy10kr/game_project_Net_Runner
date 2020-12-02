using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent m_enemy = null;

    //웨이 포인트 및 컨트롤 선언
    [SerializeField] Transform[] m_tfWayPoints = null;
    [SerializeField] CameraControl camera = null;

    int m_count = 0;
    int drone_control = 0;

    //위험지역에 들어온 타겟
    Transform m_target = null;

    //파괴될때 이펙트
    [SerializeField] GameObject enemyDestroy_effect = null;
    public void SetTarget(Transform p_target)
    {
        CancelInvoke();
        m_target = p_target;
    }

    public void RemoveTarget()
    {
        m_target = null;
        InvokeRepeating("MoveToNextWayPoint", 0f, 2f);
    }
    

    void MoveToNextWayPoint()
    {
        if(m_target == null)
        {
            //속도가 0이되면
            if (m_enemy.velocity == Vector3.zero)
            {
                //다음 목적지로(다돌면 다시 처음지역으로)
                m_enemy.SetDestination(m_tfWayPoints[m_count++].position);

                if (m_count >= m_tfWayPoints.Length)
                    m_count = 0;
            }
            else
            {
                ;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Bullet_hacking":
                GameObject.Find("Sub Camera").transform.position = transform.position;
                camera.SetSub();
                camera.control = 1;
                drone_control = 1;
                Destroy(gameObject, 10.0f);
                break;
            case "Enemy" :
            if(camera.control == 1 && drone_control == 1)
            {
                other.gameObject.SetActive(false);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            break;
            case "Turret" :
            if(camera.control == 1 && drone_control == 1)
            {
                other.gameObject.SetActive(false);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            break;
            case "Trap" :
            if(camera.control == 1 && drone_control == 1)
            {
                other.gameObject.SetActive(false);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            break;
        }
        
    }
    //파괴될때 컨트롤 되돌려줌
    void OnDestroy()
    {
        GameObject Instance_effect = Instantiate(enemyDestroy_effect, transform.position, Quaternion.identity);
        Destroy(Instance_effect, 1.0f);
        camera.control = 0;
        drone_control = 0;
        camera.SetMain();
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

        
        //스페이스바 버튼 누를때
        if ( Input.GetKey ( KeyCode.Space ))
        {
            power += 10.0f; 
            // Translate는 현재 위치에서 ()안에 들어간 값만큼 값을 변화시킨다.
            transform.Translate(Vector3.up * 3f * Time.deltaTime* power);
            // Time.deltaTime은 모든 기기(컴퓨터, OS를 망론하고)에 같은 속도로 움직이도록 하기 위한 것
        }
        
        //스페이스바 버튼 뗄때
        if ( Input.GetKeyUp ( KeyCode.Space ))
        {
            power = 0f;
        }
    }

    void Start()
    {
        m_enemy = GetComponent<NavMeshAgent>();
        //시작되면 2초마다 움직이게 조정
        InvokeRepeating("MoveToNextWayPoint", 0f, 2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(camera.control == 1 && drone_control == 1)
        {
            //타겟과 웨이포인트 해제
            m_target = null;

            //nav 자동 회전 기능과 속도 0으로 변경
            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.updateRotation = false;
            m_enemy.velocity = Vector3.zero;

            //InvokeRepeating해제
            CancelInvoke();
            Move();
            GameObject.Find("Sub Camera").transform.position = transform.position;
            GameObject.Find("Sub Camera").transform.rotation = transform.rotation;
        }
        else if(m_target != null)
        {
            m_enemy.SetDestination(m_target.position);
        }
    }
}
