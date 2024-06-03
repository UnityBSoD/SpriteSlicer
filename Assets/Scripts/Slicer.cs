using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Slicer : MonoBehaviour
{
    SpriteRenderer sr;
    TrailRenderer tr;
    Collider2D collider;

    Dictionary<Transform, (Vector2, Vector2)> records = new Dictionary<Transform, (Vector2, Vector2)>();

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();
        collider = GetComponent<Collider2D>();

        Enable(false);
    }

    void Enable(bool _bool)
    {
        sr.enabled = _bool;
        tr.enabled = _bool;
        collider.enabled = _bool;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Enable(true);
        }
        if (Input.GetMouseButton(0))
        {
            transform.position = GetMousePosition();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Enable(false);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Break();
        }
    }

    Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jelly"))
        {
            var startPos = GetMousePosition();
            RecordTargetStart(collision.transform, startPos);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Jelly"))
        {
            if(collision.GetComponent<Jelly>().maxSlicesReached)
            {
                return;
            }
            
            var endPos = GetMousePosition();
            RecordTargetEnd(collision.transform, endPos);
            InformSliceManager(collision.transform);
        }
    }

    void RecordTargetStart(Transform _transform, Vector2 _start)
    {
        if(!records.ContainsKey(_transform))
        {
            records.Add(_transform, (_start, Vector2.zero));
        }
    }

    void RecordTargetEnd(Transform _transform, Vector2 _end)
    {
        if (records.ContainsKey(_transform))
        {
            var _start = records[_transform].Item1;
            records[_transform] = (_start, _end);
        }
    }

    void InformSliceManager(Transform _transform)
    {
        var pos = records[_transform];
        records.Remove(_transform);

        SliceManager.Instance.Slice(_transform, pos.Item1, pos.Item2);
    }

}
