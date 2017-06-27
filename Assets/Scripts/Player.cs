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

    Map map;
    Vector3 pos;
    bool coroutineRunning;
    CoroutineFramework coroutineManager;

    WaitForEndOfFrame waitForEndOfFrame;

    void Start()
    {
        waitForEndOfFrame = new WaitForEndOfFrame();
        coroutineManager = GetComponent<CoroutineFramework>();
        //TODO recup spawn pos from server
        map = GameObject.FindGameObjectWithTag("Grid").GetComponent<Map>();
        /*   Spawn(map.cells[2, 0].transform.position);
        */

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

    private IEnumerator MoveUp()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        //print("MoveUp : position before : " + transform.position);
        Vector3 dest = map.WorldToGrid(transform.position + Map.North);
        yield return coroutineManager.StartTrackedCoroutine(MoveOverSpeed(transform.position + (Map.North * map.ScaleFactor.z), speed));
        if (!map.Contains(dest))
            transform.position = new Vector3(transform.position.x, transform.position.y, map.GridToWorld(Map.South * (map.dimension.y)).z + transform.position.z);
    }

    private IEnumerator MoveDown()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        // print("MoveDOwn : position before : " + transform.position);
        Vector3 dest = map.WorldToGrid(transform.position + Map.South);
        yield return coroutineManager.StartTrackedCoroutine(MoveOverSpeed(transform.position + (Map.South * map.ScaleFactor.z), speed));
        if (!map.Contains(dest))
            transform.position = new Vector3(transform.position.x, transform.position.y, map.GridToWorld(Map.North * (map.dimension.y - 1)).z);
    }

    private IEnumerator MoveRight()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        Vector3 dest = map.WorldToGrid(transform.position + Map.East);
        print("MoveLeft : position before : " + transform.position + " dest : " + dest);
        yield return coroutineManager.StartTrackedCoroutine(MoveOverSpeed(transform.position + (Map.East * map.ScaleFactor.x), speed));
        if (!map.Contains(dest))
            transform.position = new Vector3(map.GridToWorld(Map.West * (map.dimension.x)).x + transform.position.x, transform.position.y, transform.position.z);
    }

    private IEnumerator MoveLeft()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return waitForEndOfFrame;
        Vector3 dest = map.WorldToGrid(transform.position + Map.West);
        print("MoveRIght : position before : " + transform.position + " dest : " + dest);
        yield return coroutineManager.StartTrackedCoroutine(MoveOverSpeed(transform.position + (Map.West * map.ScaleFactor.x), speed));
        if (!map.Contains(dest))
            transform.position = new Vector3(map.GridToWorld(Map.East * (map.dimension.x - 1)).x, transform.position.y, transform.position.z);
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
}
