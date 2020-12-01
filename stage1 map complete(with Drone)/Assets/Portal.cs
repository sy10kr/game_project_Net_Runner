using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform m_pos = null;
    [SerializeField] Transform ship = null;

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Player":
                Vector3 checkpos;
                checkpos = m_pos.transform.position;
                ship.transform.position = m_pos.transform.position;
                ship.transform.Rotate(new Vector3(0,90,0));
                    
                    
                break;
        }
    }
}