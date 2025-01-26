using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealAim : MonoBehaviour
{

    public RectTransform realAimObject;
    public LayerMask layerMask;
    Vector3 PosInicial;
    void Start()
    {
        PosInicial = realAimObject.position;
    }
 
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 100000, layerMask))
        {
            Vector3 posicionPantalla = Camera.main.WorldToScreenPoint(hitInfo.point);
            realAimObject.position = posicionPantalla;
        }
        else
        {
            realAimObject.position = PosInicial;
        }
    }
}
