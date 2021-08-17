using System.Collections;
using UnityEngine;

public class Pile : Zone
{

    protected override IEnumerator AddCoroutine(MyObject obj, float timer)
    {
        obj.Back.SetActive(!m_isFaceUp);
        obj.Front.SetActive(m_isFaceUp);

        float time = 0;

        obj.transform.SetParent(null);

        obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, transform.position.z);
        obj.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);

        obj.Front.SetActive(false);

        while (time < timer)
        {
            yield return new WaitForEndOfFrame();

            time += Time.deltaTime;

            obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, transform.rotation, time / timer);

            Vector3 currentPos = Vector3.Lerp(obj.transform.position, transform.position, time / timer);

            currentPos.x += Vector3.up.x * Mathf.Sin(Mathf.Clamp01(time / timer) * Mathf.PI);
            currentPos.y += Vector3.up.y * Mathf.Sin(Mathf.Clamp01(time / timer) * Mathf.PI);
            currentPos.z += Vector3.up.z * Mathf.Sin(Mathf.Clamp01(time / timer) * Mathf.PI);

            obj.transform.position = currentPos;

        }

        obj.CurrentZone = this;

        obj.transform.SetParent(m_content);

        obj.transform.transform.position = transform.position;

        obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        obj.transform.localRotation = Quaternion.Euler(0, 0, 0);

        obj.Back.SetActive(!m_isFaceUp);
        obj.Front.SetActive(m_isFaceUp);

        obj.gameObject.SetActive(m_isVisible);
    }

    private void OnMouseDown()
    {
        ZoneView.Instance.Show(m_content);
    }

}
