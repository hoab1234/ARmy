using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Doodle : MonoBehaviour
{
    private GameObject parent;
    [SerializeField]
    private MeshRenderer mesh;
    public bool isImageDoodle;
    public Texture[] textures;
    public Dictionary<int, Texture> matDic =  new Dictionary<int, Texture>();
    //public Vector3 offset;
    // pos.position = Camera.main.transform.position + offset;



    int maxNum;

    private void Awake()
    {
        if(isImageDoodle)
        {
            maxNum = ImageLoader.instance.MaxImgaeDoodle;
            
        }
        else
        {
            maxNum = VideoLoader.instance.MaxVideoDoodle;
        }

      

        for (int i = 1; i < textures.Length+1; i++)
        {
            matDic.Add(i, textures[i-1]);
        }
        //mesh = GetComponentInChildren<MeshRenderer>();
    }
    private void Start()
    {
        mesh.material.mainTexture = RandomDoodleMat();
    }
    public Texture RandomDoodleMat()
    {
        int randomInt =  Random.Range(1, textures.Length+1);
        
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
        setDir();
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
        transform.localPosition = Vector3.zero;

    }
    public void setDir()
    {
        bool tempDir = transform.parent.GetComponent<TransMove>().getDir();
        if (tempDir) // 오른쪽인가
        {
            print("tempDIR IS TRUE");
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        else //왼쪽벽인가
        {
            print("tempDIR IS FALSE");
            transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
    }
    public int getRandomNum()
    {
        int _index;
        _index = Random.Range(0, maxNum);
        print("maxnum =======" + maxNum);
        print("_index =========" + _index);
        if (isImageDoodle)
        {
            if (TransSetManager.inst.ImgtransCheck[_index] == true)
            {
                _index = getRandomNum();
            }
            TransSetManager.inst.ImgtransCheck[_index] = true;
        }
        else
        {
            if (TransSetManager.inst.VideotransCheck[_index] == true)
            {
                _index = getRandomNum();
            }
            TransSetManager.inst.VideotransCheck[_index] = true;
        }
        return _index;
    }
    int index;
    internal int getPosIndex()
    {
        index = getRandomNum();
        return index; 
    }

    public void EnableToggleBillborad(bool tempbool)
    {
        if (tempbool)
            gameObject.GetComponent<Billboard>().enabled = true;
        else
            gameObject.GetComponent<Billboard>().enabled = false;
    }


}









/*int getRandomNum()
{
    int _index;
    _index = Random.Range(0, maxNum);
    if (isImageDoodle)
    {
        if (TransSetManager.inst.ImgtransCheck[_index] == true)
        {
            _index = getRandomNum();
        }
        TransSetManager.inst.ImgtransCheck[_index] = true;
    }
    else
    {
        if (TransSetManager.inst.VideotransCheck[_index] == true)
        {
            _index = getRandomNum();

        }
        TransSetManager.inst.VideotransCheck[_index] = true;
    }
    return _index;
}*/


/*


int getRandomNum()
{
    int _index;
    _index = Random.Range(0, maxNum);
    if (isUseTrans[_index] == true)
    {
        _index = getRandomNum();
    }
    isUseTrans[_index] = true;
    return _index;
}*/