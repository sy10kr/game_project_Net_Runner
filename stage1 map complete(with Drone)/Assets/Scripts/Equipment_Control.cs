using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment_Control : MonoBehaviour
{
    public GameObject equipmentObject;
    public static Equipment_Control Instance;

    
    public int[] skill_state = new int[4] { 1, 1, 0, 0 };
    public int money = 100;
    public int zombie_level = 1;
    public int battery_level = 1;
    public int stage_clear = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(equipmentObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(equipmentObject);
    }

    // Start is called before the first frame update
    public void updateSkillState(int num,int state)
    {
        skill_state[num-1] = state;
    }
    // Start is called before the first frame update
    public void updateSkillState_3()
    {
        skill_state[2] = 1;
    }
    public void updateSkillState_4()
    {
        skill_state[3] = 1;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
