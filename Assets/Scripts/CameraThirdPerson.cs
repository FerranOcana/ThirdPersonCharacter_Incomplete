using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraThirdPerson : MonoBehaviour
{
    public Transform target;

    public float startDistanciaCam;
    public float minRotationX = -60;
    public float maxRotationX = 60;
    public float sensibilidadX = 1;
    public float sensibilidadY = 1;
    public float camVelocity = 10;
    public LayerMask mask;

    float distanciaCam;
    float rotationY;
    float rotationX;
    bool isReturning = false;
    void Start()
    {
        startDistanciaCam = Vector3.Distance(transform.position, target.position);
        distanciaCam = startDistanciaCam;
    }
    // Update is called once per frame
    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * sensibilidadY;
        rotationX += Input.GetAxis("Mouse Y") * sensibilidadX;
        rotationX = Mathf.Clamp(rotationX, minRotationX, maxRotationX);
        Quaternion rotacionTotal = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 vectorDirector = transform.position - target.position;
        if (Physics.SphereCast(target.position, 0.5f, vectorDirector, out RaycastHit hitInfo, startDistanciaCam, mask))
        {
            float _ = Vector3.Distance(hitInfo.point, target.position);
            distanciaCam = _;
            isReturning = false;
            Debug.Log(hitInfo.collider.gameObject.name);
        }
        else
        {
            if (!isReturning && distanciaCam < startDistanciaCam)
            {
                isReturning = true;
            }
        }
        if (isReturning)
        {
            distanciaCam = Mathf.Lerp(distanciaCam, startDistanciaCam, Time.deltaTime * camVelocity);

            if (Mathf.Abs(distanciaCam - startDistanciaCam) < 0.01f)
            {
                distanciaCam = startDistanciaCam;
                isReturning = false;
            }
        }
        Vector3 finalPosition = target.position - rotacionTotal * new Vector3(0, 0, distanciaCam);
        transform.position = finalPosition;
        transform.rotation = rotacionTotal;
    }
}
