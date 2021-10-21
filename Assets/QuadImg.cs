using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadImg : MonoBehaviour
{
    public Material img;
    MeshRenderer quadMesh;
    // Start is called before the first frame update
    void Start()
    {
        quadMesh = transform.GetComponent<MeshRenderer>();
        quadMesh.material = img;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
