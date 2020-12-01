using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_damage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet_stop"))
        {
            //해당 오브젝트중 스크립트 이름인 Turret을 가져와서 disable시킴

            gameObject.GetComponent<Animation>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;

        
            Invoke("Init_trap", 5f);
        }
    }

    public void Init_trap()
    {
        //다시 복구
        gameObject.GetComponent<Animation>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
