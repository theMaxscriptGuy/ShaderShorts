using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quickVal : MonoBehaviour
{
    public Wave val;
    // Start is called before the first frame update
    void Start()
    {
        val = GetComponent<Wave>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            val.speed++;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            val.speed--;
        }
    }
}
