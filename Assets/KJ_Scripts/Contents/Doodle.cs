using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Doodle : MonoBehaviour
{
    private GameObject parent;
    [SerializeField]
    private MeshRenderer mesh;
    
    public Texture[] textures;
    public Dictionary<int, Texture> matDic;
    //public Vector3 offset;
    // pos.position = Camera.main.transform.position + offset;

    private void Awake()
    {
        matDic = new Dictionary<int, Texture>();
        for(int i = 1; i < textures.Length+1; i++)
        {
            matDic.Add(i, textures[i-1]);
        }
        //mesh = GetComponentInChildren<MeshRenderer>();
        mesh.material.mainTexture = RandomDoodleMat();
    }
    public Texture RandomDoodleMat()
    {
        int randomInt =  Random.Range(1, textures.Length+1);
        print(randomInt);
        return matDic[randomInt];
        
    }
    public GameObject getParent()
    {
        return parent;
    }

    internal void setParent(GameObject parent)
    {
        this.parent = parent;
    }

    public void GoBack()
    {
        transform.DOMove(parent.transform.position, 1f, false);
        StartCoroutine(PARENTBACK());
    }
    IEnumerator PARENTBACK()
    {
        yield return new WaitForSeconds(1f);
        transform.parent = parent.transform;
    }
    public void GoToCamera()
    {
        Transform _pos = GameObject.Find("ContentsFocusPos").transform;
        transform.DOMove(_pos.position, 1f, false);
        transform.parent = _pos;
        StartCoroutine(PARENTISCAMERA());
    }
    IEnumerator PARENTISCAMERA()
    {
        yield return new WaitForSeconds(1f);
    }
}
