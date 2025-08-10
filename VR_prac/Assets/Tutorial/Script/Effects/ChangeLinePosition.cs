using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLinePosition : MonoBehaviour
{
    public int index;

    private LineRenderer targert;

    private void Awake()
    {
        targert = GetComponent<LineRenderer>();
    }

    public void call(Vector3 worldPosition)
    {
        if (targert.useWorldSpace)
        {
            targert.SetPosition(index,worldPosition);   //맞은애가 월드포지션 안이면 그대로 사용
        }
        else
        {
            var localPosition = transform.InverseTransformPoint(worldPosition); //맞은애가 월드포지션 밖이면 위치 가져옴
            targert.SetPosition(index, localPosition);
        }
    }
}
