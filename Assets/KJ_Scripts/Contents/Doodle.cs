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
    public Dictionary<int, Texture> matDic = new Dictionary<int, Texture>();
    //public Vector3 offset;
    // pos.position = Camera.main.transform.position + offset;


    private bool isMoving;

    int maxNum;

    private void Awake()
    {
        if (isImageDoodle)
        {
            maxNum = ImageLoader.instance.MaxImgaeDoodle;

        }
        else
        {
            maxNum = VideoLoader.instance.MaxVideoDoodle;
        }

        for (int i = 0; i < textures.Length; i++)
        {
            matDic.Add(i, textures[i]);
        }
        //mesh = GetComponentInChildren<MeshRenderer>();
    }
    private void Start()
    {
        isMoving = false;
        mesh.material.mainTexture = RandomDoodleMat();
        StartCoroutine(ARDoodlePosReset());
    }

    IEnumerator ARDoodlePosReset()
    {
        while (true)
        {
            Vector3 temp;
            yield return new WaitForSeconds(2f);
            if (!isMoving)
            {

                temp = transform.position;
                if (0.5f <= temp.x || temp.x <= -0.5f ||
                    0.5f <= temp.y || temp.y <= -0.5f ||
                    0.5f <= temp.z || temp.z <= -0.5f)
                {
                    transform.localPosition = Vector3.zero;
                }


            }
        }
    }
    public Texture RandomDoodleMat()
    {
        int randomInt = Random.Range(0, textures.Length);
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
        isMoving = true;
        transform.DOMove(parent.transform.position, 1f, false);
        StartCoroutine(PARENTBACK());
    }
    IEnumerator PARENTBACK()
    {
        yield return new WaitForSeconds(1f);
        transform.parent = parent.transform;
        setDir();
        isMoving = false;
    }
    public void GoToCamera()
    {
        Transform _pos = GameObject.Find("ContentsFocusPos").transform;
        isMoving = true;
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
        if (tempDir) // �������ΰ�
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        else //���ʺ��ΰ�
        {
            transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
    }
    public int getRandomNum()
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