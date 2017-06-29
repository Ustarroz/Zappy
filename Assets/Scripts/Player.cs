using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 offset;
    public float speed;
    public int level = 1;
    public GameObject currModel;
    public List<GameObject> models;
    public ZappyObjects inventory;
    public int id;
    public string team;
    public Vector3 gridPos;
    public GameObject expulse;
    public Orientation orientation;
    private CoroutineFramework coroutineManager;
    private WaitForEndOfFrame waitForEndOfFrame;
    private Map map;


    public enum Orientation
    {
        NORTH = 1,
        EAST = 2,
        SOUTH = 3,
        WEST = 4
    }

    private Map GetMap
    {
        get
        {
            if (map == null)
                map = GameObject.FindGameObjectWithTag("Grid").GetComponent<Map>();
            return map;
        }
    }

    private void Awake()
    {
        inventory = new ZappyObjects();
        waitForEndOfFrame = new WaitForEndOfFrame();
        coroutineManager = GetComponent<CoroutineFramework>();
        speed = 7 / UpdateManager.frequency;

        /*   StartCoroutine(MoveForward());
           StartCoroutine(Turn90Left());
           StartCoroutine(MoveForward());
           StartCoroutine(Turn90Right());
           StartCoroutine(MoveForward());
           StartCoroutine(Turn90Right());
           StartCoroutine(MoveForward());
          */
    }

    public IEnumerator MoveForward()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        print(transform.eulerAngles.y);
        if (transform.eulerAngles.y == 0)
            yield return StartCoroutine(MoveUp());
        else if (transform.eulerAngles.y == 90)
            yield return StartCoroutine(MoveRight());
        else if (transform.eulerAngles.y == 180)
            yield return StartCoroutine(MoveDown());
        else if (transform.eulerAngles.y == 270)
            yield return StartCoroutine(MoveLeft());
    }

    public IEnumerator Move(int x, int y)
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        if (x < gridPos.x)
            yield return StartCoroutine(MoveLeft());
        else if (x > gridPos.x)
            yield return StartCoroutine(MoveRight());
        else if (y < gridPos.y)
            yield return StartCoroutine(MoveDown());
        else if (y > gridPos.y)
            yield return StartCoroutine(MoveUp());
        gridPos.x = x;
        gridPos.y = y;
    }


    private IEnumerator MoveUp()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        //print("MoveUp : position before : " + transform.position);
        Vector3 dest = GetMap.WorldToGrid(transform.position + Map.North);
        yield return coroutineManager.StartTrackedCoroutine(MoveOverSeconds(transform.position + (Map.North * GetMap.ScaleFactor.z), speed));
        if (!GetMap.Contains(dest))
            transform.position = new Vector3(transform.position.x, transform.position.y, GetMap.GridToWorld(Map.South * (GetMap.dimension.y)).z + transform.position.z);
    }

    private IEnumerator MoveDown()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        // print("MoveDOwn : position before : " + transform.position);
        Vector3 dest = GetMap.WorldToGrid(transform.position + Map.South);
        yield return coroutineManager.StartTrackedCoroutine(MoveOverSeconds(transform.position + (Map.South * GetMap.ScaleFactor.z), speed));
        if (!GetMap.Contains(dest))
            transform.position = new Vector3(transform.position.x, transform.position.y, GetMap.GridToWorld(Map.North * (GetMap.dimension.y - 1)).z);
    }

    private IEnumerator MoveRight()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        Vector3 dest = GetMap.WorldToGrid(transform.position + Map.East);
        //print("MoveLeft : position before : " + transform.position + " dest : " + dest);
        yield return coroutineManager.StartTrackedCoroutine(MoveOverSeconds(transform.position + (Map.East * GetMap.ScaleFactor.x), speed));
        if (!GetMap.Contains(dest))
            transform.position = new Vector3(GetMap.GridToWorld(Map.West * (GetMap.dimension.x)).x + transform.position.x, transform.position.y, transform.position.z);
    }

    private IEnumerator MoveLeft()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        Vector3 dest = GetMap.WorldToGrid(transform.position + Map.West);
        // print("MoveRIght : position before : " + transform.position + " dest : " + dest);
        yield return coroutineManager.StartTrackedCoroutine(MoveOverSeconds(transform.position + (Map.West * GetMap.ScaleFactor.x), speed));
        if (!GetMap.Contains(dest))
            transform.position = new Vector3(GetMap.GridToWorld(Map.East * (GetMap.dimension.x - 1)).x, transform.position.y, transform.position.z);
    }

    private IEnumerator MoveOverSpeed(Vector3 end, float speed)
    {
        print(transform.localPosition + " != " + end);
        while (transform.localPosition != end)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, end, speed * Time.deltaTime);
            yield return waitForEndOfFrame;
        }
    }

    public IEnumerator MoveOverSeconds(Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startPos = transform.position;

        while (elapsedTime < seconds)
        {
            transform.position = Vector3.Lerp(startPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            print(elapsedTime);
            yield return waitForEndOfFrame;
        }
        transform.position = end;
    }

    public IEnumerator Turn(Orientation orient)
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        print("player orientation : " + orientation + " new oriantation :" + orient);
        if ((orientation == Orientation.NORTH && orient == Orientation.EAST) ||
            (orientation == Orientation.EAST && orient == Orientation.SOUTH) ||
            (orientation == Orientation.SOUTH && orient == Orientation.WEST) ||
            (orientation == Orientation.WEST && orient == Orientation.NORTH))
            yield return StartCoroutine(Turn90Right());
        else
            yield return StartCoroutine(Turn90Left());
        orientation = orient;
    }

    public IEnumerator Turn90Left()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        yield return coroutineManager.StartTrackedCoroutine(Rotate(-90, speed));
    }

    public IEnumerator Turn90Right()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        yield return coroutineManager.StartTrackedCoroutine(Rotate(90, speed));
    }

    private IEnumerator Rotate(float rotationAmount, float seconds)
    {
        float elapsedTime = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion finalRotation = Quaternion.Euler(0, rotationAmount, 0) * transform.rotation;
        while (elapsedTime < seconds)
        {
            transform.rotation = Quaternion.Lerp(startRotation, finalRotation, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return waitForEndOfFrame;
        }
        transform.rotation = finalRotation;
    }

    public void Spawn(Vector3 position)
    {
        if (currModel != null)
            Destroy(currModel);
        if (level - 1 < models.Count)
            currModel = Instantiate(models[level - 1], Vector3.zero, models[level - 1].transform.rotation, transform);
        transform.position = position + offset;
    }

    public bool IsPositionDifferent(int x, int y)
    {
        return gridPos.x != x || gridPos.y != y;
    }

    public bool IsOrientationDifferent(Orientation orient)
    {
        return orientation != orient;
    }

    public IEnumerator Expulse()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        expulse.SetActive(true);
        yield return new WaitForSeconds(1);
        expulse.SetActive(false);
    }
}
