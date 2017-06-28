using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 offset;
    public float speed;
    public int level = 0;
    public GameObject currModel;
    public List<GameObject> models;
    public ZappyObjects inventory;
    public int id;
    public string team;
    public Vector3 gridPos;
    public GameObject expulse;
    private CoroutineFramework coroutineManager;
    private WaitForEndOfFrame waitForEndOfFrame;
    private Map map;

    private Map GetMap
    {
        get
        {
            if (map == null)
                map = GameObject.FindGameObjectWithTag("Grid").GetComponent<Map>();
            return map;
        }
    }


    void Awake()
    {
        waitForEndOfFrame = new WaitForEndOfFrame();
        coroutineManager = GetComponent<CoroutineFramework>();

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
        yield return coroutineManager.StartTrackedCoroutine(MoveOverSpeed(transform.position + (Map.North * GetMap.ScaleFactor.z), speed));
        if (!GetMap.Contains(dest))
            transform.position = new Vector3(transform.position.x, transform.position.y, GetMap.GridToWorld(Map.South * (GetMap.dimension.y)).z + transform.position.z);
    }

    private IEnumerator MoveDown()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        // print("MoveDOwn : position before : " + transform.position);
        Vector3 dest = GetMap.WorldToGrid(transform.position + Map.South);
        yield return coroutineManager.StartTrackedCoroutine(MoveOverSpeed(transform.position + (Map.South * GetMap.ScaleFactor.z), speed));
        if (!GetMap.Contains(dest))
            transform.position = new Vector3(transform.position.x, transform.position.y, GetMap.GridToWorld(Map.North * (GetMap.dimension.y - 1)).z);
    }

    private IEnumerator MoveRight()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        Vector3 dest = GetMap.WorldToGrid(transform.position + Map.East);
        //print("MoveLeft : position before : " + transform.position + " dest : " + dest);
        yield return coroutineManager.StartTrackedCoroutine(MoveOverSpeed(transform.position + (Map.East * GetMap.ScaleFactor.x), speed));
        if (!GetMap.Contains(dest))
            transform.position = new Vector3(GetMap.GridToWorld(Map.West * (GetMap.dimension.x)).x + transform.position.x, transform.position.y, transform.position.z);
    }

    private IEnumerator MoveLeft()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        Vector3 dest = GetMap.WorldToGrid(transform.position + Map.West);
        // print("MoveRIght : position before : " + transform.position + " dest : " + dest);
        yield return coroutineManager.StartTrackedCoroutine(MoveOverSpeed(transform.position + (Map.West * GetMap.ScaleFactor.x), speed));
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

    public IEnumerator Turn(Vector3 orientation)
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        float angle = Vector3.Angle(transform.rotation.eulerAngles, orientation);

        if (angle < 0)
            yield return StartCoroutine(Turn90Left());
        else
            yield return StartCoroutine(Turn90Right());
    }

    public IEnumerator Turn90Left()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        yield return coroutineManager.StartTrackedCoroutine(Rotate(-90));
    }

    public IEnumerator Turn90Right()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        yield return coroutineManager.StartTrackedCoroutine(Rotate(90));
    }

    public float AngleBetween(Vector3 vec1, Vector3 vec2)
    {
        Vector3 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector3.Angle(Vector3.right, diference) * sign;
    }

    private IEnumerator Rotate(float rotationAmount)
    {
        Quaternion finalRotation = Quaternion.Euler(0, rotationAmount, 0) * transform.rotation;

        while (transform.rotation != finalRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, Time.deltaTime * 2);
            yield return waitForEndOfFrame;
        }
        transform.rotation = finalRotation;
    }

    public void Spawn(Vector3 position)
    {
        if (currModel != null)
            Destroy(currModel);
        if (level < models.Count)
            currModel = Instantiate(models[level], Vector3.zero, models[level].transform.rotation, transform);
        transform.position = position + offset;
    }

    public bool IsPositionDifferent(int x, int y)
    {
        return gridPos.x != x || gridPos.y != y;
    }

    public bool IsOrientationDifferent(Vector3 orientation)
    {
        return transform.rotation.eulerAngles != orientation;
    }

    public void Expulse()
    {
        expulse.SetActive(!expulse.activeSelf);
    }
}
