using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatPerMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Debug.Log(mf.sharedMesh.subMeshCount);
        MeshRenderer mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
