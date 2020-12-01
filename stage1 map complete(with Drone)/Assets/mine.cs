using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mine : MonoBehaviour
{
    //지뢰 이펙트
    [SerializeField] GameObject mine_effect = null;
        
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameObject Instance_effect = Instantiate(mine_effect, transform.position, Quaternion.identity);
            Destroy(Instance_effect, 0.5f);
        }
        if (other.CompareTag("Zombie"))
        {
            Destroy(gameObject);
            GameObject Instance_effect = Instantiate(mine_effect, transform.position, Quaternion.identity);
            Destroy(Instance_effect, 0.5f);
        }
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            GameObject Instance_effect = Instantiate(mine_effect, transform.position, Quaternion.identity);
            Destroy(Instance_effect, 0.5f);
        }
    }
}
