using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mine : MonoBehaviour
{
    //지뢰 이펙트
    [SerializeField] GameObject mine_effect = null;
        
    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "Player" )
        {
            Destroy(gameObject);
            GameObject Instance_effect = Instantiate(mine_effect, transform.position, Quaternion.identity);
            Destroy(Instance_effect, 0.5f);
        }
        if (other.gameObject.tag == "Zombie" )
        {
            Destroy(gameObject);
            GameObject Instance_effect = Instantiate(mine_effect, transform.position, Quaternion.identity);
            Destroy(Instance_effect, 0.5f);
        }
        if (other.gameObject.tag == "Enemy" )
        {
            Destroy(gameObject);
            GameObject Instance_effect = Instantiate(mine_effect, transform.position, Quaternion.identity);
            Destroy(Instance_effect, 0.5f);
        }
    }
}
