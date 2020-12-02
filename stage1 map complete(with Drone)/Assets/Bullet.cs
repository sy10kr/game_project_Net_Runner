using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject enemyhit_effect = null;
    [SerializeField] GameObject portalhit_effect = null;
    [SerializeField] GameObject stop_effect = null;
    public float Bullet_speed = 1.0f;
    public float zeroTime = 0f;
    public float beatTime = 5f;

    void Start()
    {
        Destroy(gameObject, 5f);

    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Bullet_speed);
    }

    public void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Enemy":
                if(gameObject.tag == "Bullet_hacking")
                {
                    GameObject Instance_effect = Instantiate(enemyhit_effect, other.transform.position, Quaternion.identity);
                    Destroy(Instance_effect, 1.0f);
                    Destroy(gameObject);
                }
                break;

            case "Fire":
                Debug.Log("불벽과 충돌함");
                Bullet_speed =-10f;
                Destroy(gameObject);
                break;

            case "Ice":
                Destroy(gameObject);
                break;

            case "ground":
                Destroy(gameObject);
                break;
            
            case "Respawn":
                if(gameObject.tag == "Bullet_portal")
                {
                    //이펙트 나오는곳 세부위치 조정
                    Vector3 editpos;
                    editpos = other.transform.position;
                    editpos.y += 2;
                    GameObject.Find("Portal2").transform.position = editpos;
                    GameObject Instance_effect2 = Instantiate(portalhit_effect, other.transform.position, Quaternion.identity);
                    Destroy(Instance_effect2, 1.0f);
                    Destroy(other.gameObject);
                    GameObject.Find("Portal").GetComponent<BoxCollider>().enabled = true;
                }
                break;
            case "Turret":
                if(gameObject.tag == "Bullet_stop")
                {
                    //이펙트 나오는곳 세부위치 조정
                    Vector3 hiteditpos;
                    Vector3 stopeditpos;
                    hiteditpos = other.transform.position;
                    hiteditpos.x += 2;
                    hiteditpos.z += 1;
                    stopeditpos = hiteditpos;
                    stopeditpos.y -=4.5f;
                    GameObject Instance_effect3 = Instantiate(enemyhit_effect, hiteditpos, Quaternion.identity);
                    GameObject Instance_effect4 = Instantiate(stop_effect, stopeditpos, Quaternion.identity);
                    Destroy(Instance_effect3, 1.0f);
                    Destroy(Instance_effect4, 5f);
                    Destroy(gameObject);
                }
                break;

            case "Trap":
                if(gameObject.tag == "Bullet_stop")
                {
                    //이펙트 나오는곳 세부위치 조정
                    Vector3 hiteditpos;
                    Vector3 stopeditpos;
                    hiteditpos = other.transform.position;
                    //hiteditpos.x += 2;
                    //hiteditpos.z += 1;
                    stopeditpos = hiteditpos;
                    stopeditpos.y -=4.5f;
                    GameObject Instance_effect3 = Instantiate(enemyhit_effect, hiteditpos, Quaternion.identity);
                    GameObject Instance_effect4 = Instantiate(stop_effect, stopeditpos, Quaternion.identity);
                    Destroy(Instance_effect3, 1.0f);
                    Destroy(Instance_effect4, 5f);
                    Destroy(gameObject);
                }
                break;

            case "Drone":
                if(gameObject.tag == "Bullet_riding")
                {
                    Bullet_speed = 0.1f;
                    Destroy(gameObject);
                }
                break;

        }
    }
}
