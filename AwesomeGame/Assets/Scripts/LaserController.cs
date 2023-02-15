using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] Vector3 _placeToSpawnLaserBox;
    [SerializeField] GameObject _myLaserPrefab;
    [SerializeField] float ElevationOffset = 0f;
    [SerializeField] bool _isCircleMoving = false;
    private float _angle;
    private Vector3 _positionOffset;
    private GameObject _myGameObject;
    void Awake()
    {
        _myGameObject = GameObject.Instantiate(_myLaserPrefab, this.gameObject.transform, false);
    }

    private void LateUpdate()
    {
        if (_isCircleMoving)
        {
            _positionOffset.Set(
                        Mathf.Cos(_angle)/* * Vector3.Distance(_placeToSpawnLaserBox, transform.position)*/,
                        _placeToSpawnLaserBox.y,
                        Mathf.Sin(_angle)/* * Vector3.Distance(_placeToSpawnLaserBox, transform.position)*/
                    );
            _myGameObject.transform.position = transform.position + _positionOffset;
            _angle += Time.deltaTime * _moveSpeed;
        }
        
    }
}
