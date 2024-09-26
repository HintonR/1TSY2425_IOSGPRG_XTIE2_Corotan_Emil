using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    public Sprite _redArrow, _greenArrow, _yellowArrow, _redArrowN, _greenArrowN, _yellowArrowN;
    [SerializeField]
    public GameObject _arrow;

    void Awake()
    {
        Instance = this;
    }
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
