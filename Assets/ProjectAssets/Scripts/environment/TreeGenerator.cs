using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{

    [SerializeField] List<GameObject> TreePos = new List<GameObject>();
    int kindOfTree;
    [SerializeField] int maxPines;
    [SerializeField] int maxOaks;
    int maxTrees;
    int posIndex;
    [SerializeField] int PineCount;
    [SerializeField] int OakCount;
    float TreeSize;
    public bool TreesGenerated = false;
    string tag = "TreePos";
    List<GameObject> PinesInScene = new List<GameObject>();
    List<GameObject> OaksInScene = new List<GameObject>();


    Vector3 Size;
    // Start is called before the first frame update

    private void Start()
    {
        FindChildWithTag(this.gameObject, tag);
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
        if (TreesGenerated == false)
        {
            foreach (GameObject treePos in TreePos)
            {
                TreesGenerated = true;
                kindOfTree = Random.Range(0, 3);
                maxTrees = maxPines + maxOaks;

                if (kindOfTree == 0 | kindOfTree == 2 && PineCount < maxPines)
                {
                    GeneratePines();
                    PineCount++;

                }
                else if (kindOfTree == 1 | kindOfTree == 3 && OakCount < maxOaks)
                {
                    GenerateOaks();
                    OakCount++;
                }
            }
        }
        else
        {
            ReactivateTrees();
        }
    }

    void GeneratePines()
    {
        GameObject Pine = TreePool.SharedInstance.GetPooledPine();
        if (Pine != null)
        {
            TreeSize = Random.Range(1f, 2f);
            Size = new Vector3(TreeSize, TreeSize, TreeSize);
            posIndex = Random.Range(0, TreePos.Count);

            if (TreePos[posIndex].GetComponent<TreePlaced>().treePlaced == false)
            {
                Pine.transform.SetLocalPositionAndRotation(TreePos[posIndex].transform.position, TreePos[posIndex].transform.rotation);
                Pine.SetActive(true);
                Pine.transform.localScale = Size;
                PinesInScene.Add(Pine);
                TreePos[posIndex].GetComponent<TreePlaced>().treePlaced = true;
            }
        }
    }

    void GenerateOaks()
    {
        GameObject Oak = TreePool.SharedInstance.GetPooledOak();
        if (Oak != null)
        {
            TreeSize = Random.Range(1f, 2f);
            Size = new Vector3(TreeSize, TreeSize, TreeSize);
            posIndex = Random.Range(0, TreePos.Count);

            if (TreePos[posIndex].GetComponent<TreePlaced>().treePlaced == false)
            {
                Oak.transform.SetLocalPositionAndRotation(TreePos[posIndex].transform.position, TreePos[posIndex].transform.rotation);
                Oak.SetActive(true);
                Oak.transform.localScale = Size;
                OaksInScene.Add(Oak);
                TreePos[posIndex].GetComponent<TreePlaced>().treePlaced = true;
            }
        }
    }

    void ReactivateTrees()
    {
        foreach (GameObject Pine in PinesInScene)
        {
            Pine.SetActive(true);
        }

        foreach (GameObject Oak in OaksInScene)
        {
            Oak.SetActive(true);
        }
    }

    void DespawnTrees()
    {
        foreach (GameObject Pine in PinesInScene)
        {
            Pine.SetActive(false);
        }

        foreach (GameObject Oak in OaksInScene)
        {
            Oak.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GeneratedTrees();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DespawnTrees();
        }
    }
}