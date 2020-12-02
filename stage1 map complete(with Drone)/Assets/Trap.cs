using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bullet_stop")
        {
            //해당 오브젝트중 스크립트 이름인 Turret을 가져와서 disable시킴
            gameObject.GetComponent<Animation>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        
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
        gameObject.GetComponent<Animation>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
