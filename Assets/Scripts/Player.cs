using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 offset;
    public float speed;

    Map map;
    Vector3 pos;
    ZappyObjects inventory;
    bool coroutineRunning;
    CoroutineFramework coroutineFramework;

    void Start()
    {
        coroutineFramework = GetComponent<CoroutineFramework>();
        //TODO recup spawn pos from server
        map = GameObject.FindGameObjectWithTag("Grid").GetComponent<Map>();
        transform.position = map.cells[0, 0].transform.position + offset;

       // StartCoroutine(MoveDown());
        //StartCoroutine(MoveUp());
       // StartCoroutine(MoveUp());

        StartCoroutine(MoveUp());
        StartCoroutine(MoveUp());

        //  StartCoroutine(MoveUp());

        //  StartCoroutine(MoveRight());
        /*   StartCoroutine(MoveRight());
         /*   StartCoroutine(MoveLeft());
            StartCoroutine(MoveDown());
            StartCoroutine(MoveDown());
            StartCoroutine(MoveUp());
            StartCoroutine(MoveUp());
            StartCoroutine(MoveUp());*/
    }

    private IEnumerator MoveUp()
    {
        while (coroutineFramework.IsTrackedCoroutineRunning())
            yield return new WaitForEndOfFrame();
        print("MoveUp : position before : " + transform.position);
        Vector3 dest = map.WorldToGrid(transform.position + Map.North);
        print("BLABLA " + (transform.position + Map.North) + " dest " + dest + " allo " + map.cells[(int)dest.x, (int)dest.z].transform.position);
        if (map.Contains(dest))
            yield return coroutineFramework.StartTrackedCoroutine(MoveOverSpeed(map.cells[(int)dest.x, (int)dest.z].transform.position + offset, speed));
        else
        {
            transform.position = map.GridToWorld(Map.South * (map.dimension.y));
            print(transform.position);
        }
    }

   /* private IEnumerator MoveDown()
    {
        while (coroutineFramework.IsTrackedCoroutineRunning())
            yield return new WaitForEndOfFrame();
        Vector3 dest = zappyGrid.GetMap.GridToWorld(transform.position + BlockPoint.South + BlockPoint.Down) + offset;
        yield return coroutineFramework.StartTrackedCoroutine(MoveOverSpeed(dest, speed));
        if (!zappyGrid.Grid.Contains(new GridPoint3((int)dest.x, 0, (int)dest.z)))
            transform.position = zappyGrid.GetMap.GridToWorld(BlockPoint.North * (zappyGrid.gridDimensions.z)) + dest;
    }

    private IEnumerator MoveLeft()
    {
        while (coroutineFramework.IsTrackedCoroutineRunning())
            yield return new WaitForEndOfFrame();
        Vector3 dest = zappyGrid.GetMap.GridToWorld(transform.position + BlockPoint.East + BlockPoint.Down) + offset;
        yield return coroutineFramework.StartTrackedCoroutine(MoveOverSpeed(dest, speed));
        if (!zappyGrid.Grid.Contains(new GridPoint3((int)dest.x, 0, (int)dest.z)))
            transform.position = dest + BlockPoint.West * zappyGrid.gridDimensions.x;
    }
    */
    private IEnumerator MoveRight()
    {
        while (coroutineFramework.IsTrackedCoroutineRunning())
            yield return new WaitForEndOfFrame();
        Vector3 dest = map.GridToWorld(transform.position + Map.West) + offset;
        print("dest :" + dest);
        yield return coroutineFramework.StartTrackedCoroutine(MoveOverSpeed(dest, speed));
        if (!map.Contains(dest))
        {
            transform.position = map.GridToWorld(Map.East * (map.dimension.x)) + dest;
            print("toto");
        }
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
