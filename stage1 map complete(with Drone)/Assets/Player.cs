using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3f;
    public bool space_flag = false;
    public float power = 0f;
    public float skill_num = 0;

    //포탈쓴게 처음인지 확인하는용
    int portal_check = 0;

    //각종 총알 오브젝트, 총쏘는위치, 총 이펙트
    [SerializeField] GameObject Bullet_hacking = null;
    [SerializeField] GameObject Bullet_portal = null;
    [SerializeField] GameObject Bullet_stop = null;

    [SerializeField] Transform Fire_pos = null;
    [SerializeField] GameObject Fire_effect = null;

    //좀비 오브젝트, 소환 위치, 소환 이펙트
    [SerializeField] GameObject Zombie = null;
    [SerializeField] Transform Zombie_pos = null;
    [SerializeField] GameObject Zombie_effect = null;

    //포탈 소환 이펙트
    [SerializeField] GameObject Portal_effect = null;

    //카메라 및 컨트롤 제어
    [SerializeField] CameraControl camera = null;
    void Start()
    {
        camera.hp = 100;
    }
    // Update is called once per frame
    void Update()
    {
        if(camera.control == 0)
            Move();
        if(camera.hp < 1)
        {
            GameObject Instance_effect = Instantiate(Zombie_effect, Zombie_pos.position, Zombie_pos.rotation);
            Destroy(Instance_effect, 5f);
            Destroy(gameObject);
        }
            
    }

    // 움직이는 기능을 하는 메소드
    private void Move()
    {
        if (Input.GetKey(KeyCode.W))  // ↑ 방향키를 누를 때
        {
            // Translate는 현재 위치에서 ()안에 들어간 값만큼 값을 변화시킨다.
            transform.Translate(Vector3.forward * speed * Time.deltaTime * 10);
            // Time.deltaTime은 모든 기기(컴퓨터, OS를 망론하고)에 같은 속도로 움직이도록 하기 위한 것
        }

        if (Input.GetKey(KeyCode.S))  // ↑ 방향키를 누를 때
        {
            // Translate는 현재 위치에서 ()안에 들어간 값만큼 값을 변화시킨다.
            transform.Translate(Vector3.back * speed * Time.deltaTime * 10);
            // Time.deltaTime은 모든 기기(컴퓨터, OS를 망론하고)에 같은 속도로 움직이도록 하기 위한 것
        }

        if (Input.GetKey(KeyCode.D))  // → 방향키를 누를 때
        {
            transform.Rotate(0.0f, 90.0f * Time.deltaTime, 0.0f);
        }

        if (Input.GetKey(KeyCode.A))  // ← 방향키를 누를 때
        {
            transform.Rotate(0.0f, -90.0f * Time.deltaTime, 0.0f);
        }

        
        //스페이스바 버튼 누를때
        if ( Input.GetKey ( KeyCode.Space ))
        {
            power += 0.5f; 
            // Translate는 현재 위치에서 ()안에 들어간 값만큼 값을 변화시킨다.
            transform.Translate(Vector3.up * speed * Time.deltaTime* power);
            // Time.deltaTime은 모든 기기(컴퓨터, OS를 망론하고)에 같은 속도로 움직이도록 하기 위한 것
        }
        //스페이스바 버튼 뗄때
        if ( Input.GetKeyUp ( KeyCode.Space ))
        {
            power = 0f;
        }
        //마우스 왼쪽버튼 눌렀을때
        if (Input.GetMouseButtonDown(0))
        {
            //해킹총알
            if(skill_num == 1)
            {
                GameObject Instance_bullet = Instantiate(Bullet_hacking, Fire_pos.position, Fire_pos.rotation);
                GameObject Instance_effect = Instantiate(Fire_effect, Fire_pos.position, Fire_pos.rotation);

                Destroy(Instance_effect, 0.07f);
            }
            //포탈총알
            if(skill_num == 3)
            {
                GameObject Instance_bullet = Instantiate(Bullet_portal, Fire_pos.position, Fire_pos.rotation);
                GameObject Instance_effect = Instantiate(Fire_effect, Fire_pos.position, Fire_pos.rotation);

                Destroy(Instance_effect, 0.07f);
            }
            if(skill_num == 4)
            {
                GameObject Instance_bullet = Instantiate(Bullet_stop, Fire_pos.position, Fire_pos.rotation);
                GameObject Instance_effect = Instantiate(Fire_effect, Fire_pos.position, Fire_pos.rotation);

                Destroy(Instance_effect, 0.07f);
            }

        }
        //1번 스킬은 해킹
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skill_num = 1;
        }
        //2번 스킬 - 좀비소환
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skill_num = 2;
            Vector3 edit_pos;
            edit_pos = Zombie_pos.position;
            edit_pos.z += 1;
            GameObject Instance_Zombie = Instantiate(Zombie, edit_pos, Zombie_pos.rotation);
            GameObject Instance_effect = Instantiate(Zombie_effect, Zombie_pos.position, Zombie_pos.rotation);

            Destroy(Instance_Zombie, 5f);
            Destroy(Instance_effect, 5f);
        }
        //3번 스킬 - 포탈소환
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            skill_num = 3;
            Vector3 edit_pos; 
            edit_pos = Zombie_pos.position;
            edit_pos.y += 2;
            edit_pos.x += 1;
            GameObject Instance_effect = Instantiate(Portal_effect, edit_pos, Zombie_pos.rotation);
            Destroy(Instance_effect, 1f);
            GameObject.Find("Portal").transform.position = edit_pos;
            GameObject.Find("Portal").transform.rotation = Fire_pos.rotation;
            if(portal_check == 0)
            {
                GameObject.Find("Portal").GetComponent<BoxCollider>().enabled = false;
                portal_check = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            skill_num = 4;
        }




    }

}
