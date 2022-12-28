using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform Player;
    [SerializeField] Vector3 Offset;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Player.position.x + Offset.x, Player.position.y + Offset.y, Player.position.z + Offset.z);
    }
}
