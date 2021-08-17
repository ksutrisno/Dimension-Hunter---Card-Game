using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    [SerializeField]
    Transform origin;

    LineRenderer lineRenderer;

    private Character target;
    [SerializeField]
    public Character Target
    {
        get { return target; }
        set { target = value; }
    }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
    }

    private void Update()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(origin.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;



        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        Vector2 pos;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        pos = mousePos - (mousePos - new Vector2(origin.position.x, origin.position.y)).normalized * 0.35f;


        transform.position = pos;

        lineRenderer.SetPosition(1, pos);





    }

}
