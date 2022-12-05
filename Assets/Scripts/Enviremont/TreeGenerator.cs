using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{

    [SerializeField] List<GameObject> TreePos = new List<GameObject>();
    public GameObject[] Tree;
    int kindOfTree; 
    [SerializeField] int TreeCount; 
    int posIndex;
     float TreeSize;
    string tag = "TreePos";
 
    Vector3 Size; 
    // Start is called before the first frame update

    private void Start()
    {
        FindChildWithTag(this.gameObject, tag);

   


        GeneratedTrees();
  
    }

    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        GameObject child = null;

        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                TreePos.Add(child);

            }
        }

        return child;


    }
    void GeneratedTrees()
    {
        TreeCount = Random.Range(10, TreePos.Count);
        for (int i = 0; i <= TreeCount; i++)
        {
            TreeSize = Random.Range(50f, 100f);
            Size = new Vector3(TreeSize, TreeSize, TreeSize);
            kindOfTree = Random.Range(0, Tree.Length - 1);
            posIndex = Random.Range(0, TreePos.Count);
            if (TreePos[posIndex].GetComponent<TreePlaced>().treePlaced == false)
            {
                var tree = Instantiate(Tree[kindOfTree], TreePos[posIndex].transform.position, TreePos[posIndex].transform.rotation);
                tree.transform.localScale = Size;
                TreePos[posIndex].GetComponent<TreePlaced>().treePlaced = true;
            }


        }
        System.GC.Collect(); 
    }


}
