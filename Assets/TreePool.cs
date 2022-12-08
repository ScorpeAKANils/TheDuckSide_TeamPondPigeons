using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePool : MonoBehaviour
{
    public static TreePool SharedInstance;
    public List<GameObject> m_PinePool;
    public List<GameObject> m_OakPool;
    public GameObject Pine_obj;
    public GameObject Oak_Obj;
    [SerializeField] int AmountPine;
    [SerializeField] int AmountOak;

    //vor instanzieren und speichern der bäume, um sie wieder zu verwenden. 
    // Start is called before the first frame update

    private void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        //spawnen und poolen der Bäume:
        m_PinePool = new List<GameObject>();
        GameObject Pine;
        for (int i = 0; i < AmountPine; i++)
        {

            Pine = Instantiate(Pine_obj);
            Pine.SetActive(false);
            m_PinePool.Add(Pine);
        }

        m_OakPool = new List<GameObject>();
        GameObject Oak;
        for (int i = 0; i < AmountOak; i++)
        {

            Oak = Instantiate(Oak_Obj);
            Oak.SetActive(false);
            m_OakPool.Add(Oak);
        }

        System.GC.Collect(); 

    }


    public GameObject GetPooledPine()
    {
        for (int i = 0; i < AmountPine; i++)
        {
            if (!m_PinePool[i].activeInHierarchy)
            {
                return m_PinePool[i];
            }
        }
        return null; 
    }

    public GameObject GetPooledOak()
    {
        for (int i = 0; i < AmountOak; i++)
        {
            if (!m_OakPool[i].activeInHierarchy)
            {
                return m_OakPool[i];
            }
        }
        return null;
    }


}
