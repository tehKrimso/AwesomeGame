using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
    private GameObject _laserRayObjet;
    [SerializeField] private float laserXSize;
    [SerializeField] private float laserZSize;
    [SerializeField] private float laserOffset = 0.5f;
    private void Start()
    {
        _laserRayObjet = transform.GetChild(0).gameObject;
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, -Vector3.up);
        if (Physics.Raycast(downRay, out hit,999f))
        {
            _laserRayObjet.transform.localScale = new Vector3(laserXSize, hit.distance/2 + laserOffset, laserZSize);
            _laserRayObjet.transform.localPosition = new Vector3(0f, -hit.distance/2 + laserOffset, 0f);
            Debug.DrawLine(transform.position, hit.point, Color.white);

        }
    }
}
