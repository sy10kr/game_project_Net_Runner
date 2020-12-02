using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_Pass : MonoBehaviour
{
    private GameObject EquipmentObject;
    private Equipment_Control EqScript;

    // Start is called before the first frame update
    void Start()
    {
        EquipmentObject = GameObject.Find("EquipmentObject");
        EqScript = EquipmentObject.GetComponent<Equipment_Control>();
        Debug.Log(EqScript.money);

        EqScript.money = EqScript.money + 100;
        EqScript.stage_clear = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
