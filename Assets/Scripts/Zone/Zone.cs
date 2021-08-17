using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    //Is the animation curved or straight
    [SerializeField]
    private bool m_curved = true;

    //Card capacity
    [SerializeField]
    protected int m_capacity = 10;

    //Zone content
    [SerializeField]
    protected Transform m_content;

    //Zone owner
    protected Character m_owner;

    [SerializeField]
    private float offsetZ;

    [SerializeField]
    private Vector3 m_offset;

    [SerializeField]
    protected bool m_isVisible = true;

    //Whether card is facing up or down
    [SerializeField]
    protected bool m_isFaceUp = true;


    //The location of the first child
    private float m_firstChildLocalPosition;



    #region delegates
    protected delegate void PostAddedDelegate(MyObject obj);

    protected PostAddedDelegate PostAdd;

    protected delegate void PreAddedDelegate(MyObject obj);

    protected PreAddedDelegate PreAdd;
    #endregion


    #region getter & setter
    /// <summary>
    /// Get all cards within the zone
    /// </summary>
    public List<Card> Cards
    {
        get
        {
            List<Card> cards = new List<Card>();
            foreach (Transform child in m_content)
            {
                cards.Add(child.GetComponent<Card>());
            }

            return cards;
        }
    }


    public Transform Content
    {
        get
        {
            return m_content;
        }

        set
        {
            m_content = value;
        }
    }
    #endregion


    protected virtual void Awake()
    {
        m_owner = GetComponentInParent<Character>();
    }


    /// <summary>
    /// Add card to the zone
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="timer"></param>
    public void Add(MyObject obj, float timer)
    {
        if (m_content.childCount < m_capacity)
        {
            StartCoroutine(StartAddCoroutine(obj, timer));
        }
    }



    /// <summary>
    /// Start the coroutine to add card to the zone
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="timer"></param>
    public IEnumerator StartAddCoroutine(MyObject obj, float timer)
    {

        PreAdd?.Invoke(obj);

        obj.Ui.SetActive(m_isVisible);

        yield return AddCoroutine(obj, timer);

        PostAdd?.Invoke(obj);

        Arrange();
    }


    /// <summary>
    /// Card animation when moving to zone
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="timer"></param>
    /// <returns></returns>
    protected virtual IEnumerator AddCoroutine(MyObject obj, float timer)
    {
        float time = 0;
        obj.Back.SetActive(!m_isFaceUp);
        obj.Front.SetActive(m_isFaceUp);

        obj.transform.SetParent(null);

        while (time < timer)
        {
            yield return new WaitForEndOfFrame();

            time += Time.deltaTime;


            Quaternion targetRotation;

            targetRotation = obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, transform.rotation, time / timer);



            obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, targetRotation, time / timer);

            Vector3 currentPos = Vector3.Lerp(obj.transform.position, transform.position, time / timer);
            if (m_curved)
            {
                currentPos.x += Vector3.up.x * Mathf.Sin(Mathf.Clamp01(time / timer) * Mathf.PI);
                currentPos.y += Vector3.up.y * Mathf.Sin(Mathf.Clamp01(time / timer) * Mathf.PI);
                currentPos.z += Vector3.up.z * Mathf.Sin(Mathf.Clamp01(time / timer) * Mathf.PI);
            }


            obj.transform.position = currentPos;

        }

        obj.CurrentZone = this;

        obj.transform.SetParent(transform);
        obj.transform.SetAsFirstSibling();

        obj.transform.rotation = transform.rotation;

        obj.transform.transform.position = transform.position;

        obj.Back.SetActive(!m_isFaceUp);
        obj.Front.SetActive(m_isFaceUp);

        obj.gameObject.SetActive(m_isVisible);
    }

    /// <summary>
    /// Shuffle cards in the zone
    /// </summary>
    public void Shuffle()
    {
        foreach (Transform child in m_content)
        {
            child.SetSiblingIndex(UnityEngine.Random.Range(0, transform.childCount));
        }
    }



    /// <summary>
    /// Arrange card inside the zone
    /// </summary>
    public void Arrange()
    {

        for (int i = 0; i < Content.childCount; i++)
        {
            Content.GetChild(i).transform.localPosition = new Vector3(Content.GetChild(i).transform.localPosition.x, m_offset.y * i, m_offset.z * i);
        }



        int num = 0;
        for (int i = 0; i < Content.childCount / 2; i++)
        {
            num++;
            Content.GetChild(i).transform.localPosition = new Vector3(Content.GetChild(i).transform.localPosition.x, Content.GetChild(i).transform.localPosition.y * num * 0.05f, Content.GetChild(i).transform.localPosition.z);
        }

        num = 0;

        for (int i = Content.childCount - 1; i > Content.childCount / 2; i--)
        {
            Content.GetChild(i).transform.localPosition = new Vector3(Content.GetChild(i).transform.localPosition.x, Content.GetChild(i).transform.localPosition.y * num * 0.05f, Content.GetChild(i).transform.localPosition.z);
        }
    }
}
