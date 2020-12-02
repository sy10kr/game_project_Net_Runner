using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] GameObject heal_effect = null;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" )
        {
            Destroy(gameObject);
            GameObject Instance_effect = Instantiate(heal_effect, other.transform.position, Quaternion.identity);
            Destroy(Instance_effect, 2f);
        }

    }
    void Update()
    {
         transform.Rotate(new Vector3(30,45,0) * Time.deltaTime);
    }
}
