using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtMouse : MonoBehaviour
{

    private GameObject mouse;
    public float speed;
    public float rotationModifier;

    // Start is called before the first frame update
    void Start()
    {
        mouse = GameObject.Find("MousePos");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vectorToTarget = mouse.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
    }
}
