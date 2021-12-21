using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementMoveToTarget : MonoBehaviour
{
    public Transform target;
    public float speed;
    Vector3 targetPos;
    Camera cam;

    Clone.Core.customerServedUI customerServedUI;
    private void Start()
    {
        customerServedUI = FindObjectOfType<Clone.Core.customerServedUI>();
        target = customerServedUI.target;
        if (cam == null)
        {
            cam = customerServedUI.cam;
        }
    }

    private void Update()
    {
        targetPos = cam.ScreenToWorldPoint(new Vector3(target.position.x, target.position.y, cam.transform.position.z * -1));
        transform.forward = -Camera.main.transform.forward;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if (transform.position == targetPos)
        {
            FindObjectOfType<Clone.Core.GameManager>().incresementOfCustomerServed();
            Destroy(this.gameObject);
        }
    }
}
