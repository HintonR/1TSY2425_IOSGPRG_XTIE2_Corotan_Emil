using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ArrowColor
{
    Red,
    Green,
    Yellow
}

public class Enemy : MonoBehaviour
{
    GameManager _gM;
    SwipeManager _sW;
    private ArrowColor _color;
    private Direction _direction;
    private GameObject _arrow;
    private GameObject _arrowInstance;
    public float arrowOffsetY = 1f;
    bool _isSpinning = false;
    void Start()
    {
        _gM = GameManager.Instance;
        _sW = SwipeManager.Instance;

        _arrow = _gM._arrow;
        _color = (ArrowColor)Random.Range(0,2); //No Yellow
        if (_color != ArrowColor.Yellow)
            _direction = (Direction)Random.Range(0,4);
        else
            _isSpinning = true;
        
        //Debug.Log(_direction);
        SpawnArrowAbove();
    }

    void Update()
    {
        transform.Translate(Vector3.right * 0.5f * Time.deltaTime); 

        if (_arrowInstance != null)
        {
            Vector3 arrowPosition = transform.position + new Vector3(-arrowOffsetY, 0 , 0);
            _arrowInstance.transform.position = arrowPosition;
        }

        if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y)
        {
            Destroy(_arrowInstance);
            Destroy(gameObject);
        }
    }

     void OnTriggerEnter2D(Collider2D other)
    {   

        if (other.CompareTag("Attack"))
        {
            if (_color == ArrowColor.Green) _arrowInstance.GetComponent<SpriteRenderer>().sprite = _gM._greenArrow;
            else if (_color == ArrowColor.Red) _arrowInstance.GetComponent<SpriteRenderer>().sprite = _gM._redArrow;
            else if (_color == ArrowColor.Yellow) _arrowInstance.GetComponent<SpriteRenderer>().sprite = _gM._yellowArrow;
        }
        else if (other.CompareTag("Sight"))
        {
            Debug.Log("Enemy in Sight");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            if (_color == ArrowColor.Green || _color == ArrowColor.Yellow) 
            {            
                if(_sW._direction == _direction) {
                    Destroy(_arrowInstance);
                    Destroy(gameObject);
                }
            }
            if (_color == ArrowColor.Red)
            {
                if(_direction == Direction.Up && _sW._direction == Direction.Down || 
                   _direction == Direction.Down && _sW._direction == Direction.Up ||
                   _direction == Direction.Left && _sW._direction == Direction.Right ||
                   _direction == Direction.Right && _sW._direction == Direction.Left)
                {
                    Destroy(_arrowInstance);
                    Destroy(gameObject);
                }
            }
        }
    }


    void SpawnArrowAbove()
    {
        if (_arrow != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(-arrowOffsetY, 0, 0);
            _arrowInstance = Instantiate(_arrow, spawnPosition, Quaternion.identity);
            if (_color == ArrowColor.Red)
            _arrowInstance.GetComponent<SpriteRenderer>().sprite = _gM._redArrowN;
            else if (_color == ArrowColor.Green)
            _arrowInstance.GetComponent<SpriteRenderer>().sprite = _gM._greenArrowN;
            else if (_color == ArrowColor.Yellow)
            _arrowInstance.GetComponent<SpriteRenderer>().sprite = _gM._yellowArrowN;
            SetArrowRotation();
        }
    }
    void SetArrowRotation()
    {
        if (_direction == Direction.Up)
        {
            _arrowInstance.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (_direction == Direction.Down)
        {
            _arrowInstance.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (_direction == Direction.Left)
        {
            _arrowInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_direction == Direction.Right)
        {
            _arrowInstance.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
    }
}
