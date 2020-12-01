using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] float m_angle = 0f;
    [SerializeField] float m_distance = 0f;
    [SerializeField] LayerMask m_layerMask = 0;

    [SerializeField] EnemyAI m_enemy = null;

    void Sight()
    {
        //일정 반경내의 플레이어 콜라이더 검출
        Collider[] t_cols = Physics.OverlapSphere(transform.position, m_distance, m_layerMask);

        //안에 있다면
        if(t_cols.Length > 0)
        {
            Transform t_tfPlayer = t_cols[0].transform;

            Vector3 t_direction = (t_tfPlayer.position - transform.position).normalized;
            float t_angle = Vector3.Angle(t_direction, transform.forward);

            //서로의 시야가 시야각에 들었다면
            if(t_angle < m_angle * 0.5f){
                if(Physics.Raycast(transform.position, t_direction, out RaycastHit t_hit, m_distance)){
                    //Lay에 닿은게 Player라면 장애물 없음!
                    if(t_hit.transform.tag == "Player"){
                        transform.position = Vector3.Lerp(transform.position, t_hit.transform.position, 0.01f);
                        m_enemy.SetTarget(t_tfPlayer);
                    }
                }
            }
            else
            {
                m_enemy.RemoveTarget();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        Sight();
    }
}
