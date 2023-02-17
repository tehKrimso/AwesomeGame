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
    [SerializeField] bool _isRotatable = false;
    [SerializeField] bool _isHorizontal = false;
    [SerializeField] int _timeTillDestroy = 15;
    private float _angle;
    private Vector3 _positionOffset;
    private GameObject _myGameObject;
    void Awake()
    {
        _myGameObject = GameObject.Instantiate(_myLaserPrefab, this.gameObject.transform, false);
        if( _isHorizontal)
        {
            _myGameObject.transform.Rotate(new Vector3(0,0,90));
        }
        if (_timeTillDestroy > 0)
        {
            Destroy(this.gameObject, _timeTillDestroy);
        }
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
            _myGameObject.transform.localPosition = transform.localPosition + _positionOffset;
            _angle += Time.deltaTime * _moveSpeed;
        }
        else
        {
            float moveOffset = Time.deltaTime * _moveSpeed;
            _myGameObject.transform.localPosition += new Vector3(0, 0, moveOffset);
        }

        
    }
}
