using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    //포신만 움직이기 위한 Transform
    [SerializeField] Transform m_tfGunBody = null;
    //범위
    [SerializeField] float m_range = 0f;
    //레이어 판별용
    [SerializeField] LayerMask m_layerMask = 0;
    [SerializeField] float m_spinSPeed = 0f;
    [SerializeField] float m_fireRate = 0f;
    float m_currentFireRate;

    //공격 대상
    Transform m_tfTarget = null;

    //공격위치, 쏘는 이펙트, 총알
    [SerializeField] Transform m_firepos = null;
    [SerializeField] GameObject fire_effect = null;
    [SerializeField] GameObject Bullet = null;
    [SerializeField] GameObject Destroy_effect = null;

    void SearchEnemy()
    {
        //객체 주변 collider 검출
        Collider[] t_cols = Physics.OverlapSphere(transform.position, m_range, m_layerMask);
        //가장 가까운 타겟
        Transform t_shortestTarget = null;

        if(t_cols.Length > 0)
        {
            float t_shortestDistance = Mathf.Infinity;
            foreach(Collider t_colTarget in t_cols)
            {
                //거리비교
                float t_distance = Vector3.SqrMagnitude(transform.position - t_colTarget.transform.position);
                if(t_shortestDistance > t_distance)
                {
                    t_shortestDistance = t_distance;
                    t_shortestTarget = t_colTarget.transform;
                }
            }
        }

        m_tfTarget = t_shortestTarget;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_currentFireRate = m_fireRate;
        //0.5초마다 반복 실행
        InvokeRepeating("SearchEnemy",0f,0.5f);        
    }

    // Update is called once per frame
    void OnDestroy()
    {
        GameObject Instance_effect = Instantiate(Destroy_effect, transform.position, Quaternion.identity);
        Destroy(Instance_effect, 1.0f);
    }

    void Update()
    {
        if(m_tfTarget == null)
        {
            m_tfGunBody.Rotate(new Vector3(0,45,0) * Time.deltaTime);
        }
           
        else
        {
            //들어온 방향 바라보기
            Quaternion t_lookRotation = Quaternion.LookRotation(m_tfTarget.position - m_tfGunBody.position);
            Vector3 t_euler = Quaternion.RotateTowards(m_tfGunBody.rotation, t_lookRotation, m_spinSPeed * Time.deltaTime).eulerAngles;

            m_tfGunBody.rotation = Quaternion.Euler(0, t_euler.y, 0);

            //사격용
            Quaternion t_fireRotation = Quaternion.Euler(0, t_lookRotation.eulerAngles.y, 0);
            if(Quaternion.Angle(m_tfGunBody.rotation, t_fireRotation) < 5f)
            {
                m_currentFireRate -=Time.deltaTime;
                if(m_currentFireRate <= 0)
                {
                    m_currentFireRate = m_fireRate;

                    GameObject Instance_effect = Instantiate(fire_effect, m_firepos.position, Quaternion.identity);
                    GameObject Instance_bullet = Instantiate(Bullet, m_firepos.position, m_firepos.rotation);
                   
                    Destroy(Instance_effect, 0.5f);
                    Destroy(Instance_bullet, 1.0f);

                }
            }
            
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bullet_stop")
        {
            //해당 오브젝트중 스크립트 이름인 Turret을 가져와서 disable시킴
            gameObject.GetComponent<Turret>().enabled = false;
            Invoke("Init_trap", 5f);
        }
        if(other.gameObject.tag == "Zombie")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    public void Init_trap()
    {
        //다시 복구
        gameObject.GetComponent<Turret>().enabled = true;
    }
}