using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyObject : MonoBehaviour {

    protected Zone m_currentZone;

    [SerializeField]
    private bool m_isFaceUp;

    [SerializeField]
    protected Character m_owner;

    [SerializeField]
    protected GameObject ui;
    [SerializeField]
    protected GameObject m_front;
    [SerializeField]
    protected GameObject m_back;

    public Zone CurrentZone
    {
        get
        {
            return m_currentZone;
        }

        set
        {
            m_currentZone = value;
        }
    }

    public Character Owner
    {
        get
        {
            return m_owner;
        }

        set
        {
            m_owner = value;
        }
    }
    public bool IsFaceUp
    {
        get
        {
            return m_isFaceUp;
        }

        set
        {
            m_isFaceUp = value;
        }
    }

    public GameObject Front
    {
        get
        {
            return m_front;
        }

        set
        {
            m_front = value;
        }
    }

    public GameObject Back
    {
        get
        {
            return m_back;
        }

        set
        {
            m_back = value;
        }
    }

    public GameObject Ui
    {
        get
        {
            return ui;
        }

        set
        {
            ui = value;
        }
    }

    public IEnumerator Flip(float timer)
    {

        float time = 0;


        while (time < timer)
        {
            yield return new WaitForEndOfFrame();

            time += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0, -180, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, time / timer);
        }
    }
}
