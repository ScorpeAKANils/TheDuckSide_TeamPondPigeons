using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MouseCrosshairRotation : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] GameObject player;
    //[SerializeField] Component movescript;
    MOVE playerMove;

    void Awake()
    {
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMove = player.GetComponent<MOVE>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 GunPos = Camera.main.WorldToScreenPoint(this.transform.position);
        Vector3 direction = Input.mousePosition - GunPos;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        Debug.Log(angle);
        if (!playerMove.MovesRight)
        {
            if (angle < -70) angle = -70;
            if (angle > 70) angle = 70;
            this.transform.rotation = Quaternion.AngleAxis(-angle + 20, Vector3.forward);
        }
        else
        {
            if (angle < 110 && angle > 0) angle = 110;
            if (angle > -110 && angle < 0) angle = -110;
            this.transform.rotation = Quaternion.AngleAxis(angle + 200, Vector3.back);
        }
    }
}


//Debug.Log(Input.mousePosition);
//Debug.Log(mouseCursorPos);
//Vector3 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//Debug.Log(mouseCursorPos + "-" + transform.position + "=" + direction );
//Debug.Log(playerMove.MovesRight);
//Debug.Log(angle);
//Debug.Log(playerMove.MovesRight);
