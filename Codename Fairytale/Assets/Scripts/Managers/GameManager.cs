using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//written by Mindy Jun

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance;

    public static GameManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return m_instance;
        }
    }

    public int completedLevels { get; set; }

    void Awake()
    {
        m_instance = this;
    }

    void Start()
    {
        completedLevels = 0;
    }
}
