using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamelogic.Grids2;

public class Player : MonoBehaviour
{
    public Vector3 offset;
    public float speed;

    ZappyGrid zappyGrid;
    Vector3 pos;
    ZappyObjects inventory;
    bool coroutineRunning;
    CoroutineFramework coroutineFramework;

    void Start()
    {
        coroutineFramework = GetComponent<CoroutineFramework>();
        //TODO recup spawn pos from server
        zappyGrid = GameObject.FindGameObjectWithTag("Grid").GetComponent<ZappyGrid>();
        transform.position = zappyGrid.Grid[new GridPoint3(0, 0, 0)].transform.position + offset;

        StartCoroutine(MoveRight());
        StartCoroutine(MoveRight());
        StartCoroutine(MoveLeft());
        StartCoroutine(MoveDown());
        StartCoroutine(MoveDown());
        StartCoroutine(MoveUp());
        StartCoroutine(MoveUp());
        StartCoroutine(MoveUp());
    }

    private IEnumerator MoveUp()
    {
        while (coroutineFramework.IsTrackedCoroutineRunning())
            yield return new WaitForEndOfFrame();
        print("MoveUp");
        Vector3 dest = zappyGrid.GetMap.WorldToGrid(transform.position) + BlockPoint.North;
        yield return coroutineFramework.StartTrackedCoroutine(MoveOverSpeed(dest, speed));
        if (!zappyGrid.Grid.Contains(new GridPoint3((int)dest.x, 0, (int)dest.z)))
            transform.position = dest + BlockPoint.South * zappyGrid.gridDimensions.z;
    }

    private IEnumerator MoveDown()
    {
        while (coroutineFramework.IsTrackedCoroutineRunning())
            yield return new WaitForEndOfFrame();
        Vector3 dest = zappyGrid.GetMap.WorldToGrid(transform.position) + BlockPoint.South;
        yield return coroutineFramework.StartTrackedCoroutine(MoveOverSpeed(dest, speed));
        if (!zappyGrid.Grid.Contains(new GridPoint3((int)dest.x, 0, (int)dest.z)))
            transform.position = dest + BlockPoint.North * zappyGrid.gridDimensions.z;
    }

    private IEnumerator MoveLeft()
    {
        while (coroutineFramework.IsTrackedCoroutineRunning())
            yield return new WaitForEndOfFrame();
        Vector3 dest = zappyGrid.GetMap.WorldToGrid(transform.position) + BlockPoint.East;
        yield return coroutineFramework.StartTrackedCoroutine(MoveOverSpeed(dest, speed));
        if (!zappyGrid.Grid.Contains(new GridPoint3((int)dest.x, 0, (int)dest.z)))
            transform.position = dest + BlockPoint.West * zappyGrid.gridDimensions.x;
    }

    private IEnumerator MoveRight()
    {
        while (coroutineFramework.IsTrackedCoroutineRunning())
            yield return new WaitForEndOfFrame();
        Vector3 dest = zappyGrid.GetMap.WorldToGrid(transform.position) + BlockPoint.West;
        yield return coroutineFramework.StartTrackedCoroutine(MoveOverSpeed(dest, speed));
        if (!zappyGrid.Grid.Contains(new GridPoint3((int)dest.x, 0, (int)dest.z)))
            transform.position = dest + BlockPoint.East * zappyGrid.gridDimensions.x;
    }

    private IEnumerator MoveOverSpeed(Vector3 end, float speed)
    {
        print(transform.localPosition + " != " + end);
        while (transform.localPosition != end)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

}
