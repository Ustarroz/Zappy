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

    Map map;
    Vector3 pos;
    bool coroutineRunning;
    CoroutineFramework coroutineManager;

    void Start()
    {
        coroutineManager = GetComponent<CoroutineFramework>();
        //TODO recup spawn pos from server
        map = GameObject.FindGameObjectWithTag("Grid").GetComponent<Map>();
        Spawn(map.cells[2, 0].transform.position);

        /*   StartCoroutine(MoveDown());
           StartCoroutine(MoveDown());
           StartCoroutine(MoveUp());
           StartCoroutine(MoveUp());
           StartCoroutine(MoveUp());
           StartCoroutine(MoveRight());
           StartCoroutine(MoveRight());
           StartCoroutine(MoveRight());
           StartCoroutine(MoveUp());
           StartCoroutine(MoveUp());
           StartCoroutine(MoveUp());
           StartCoroutine(MoveUp());
           StartCoroutine(MoveUp());
           StartCoroutine(MoveUp());
           StartCoroutine(MoveLeft());
           StartCoroutine(MoveLeft());
           StartCoroutine(MoveLeft());
           StartCoroutine(MoveLeft());
           StartCoroutine(MoveLeft());*/
    }

    private IEnumerator MoveUp()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return new WaitForEndOfFrame();
        //print("MoveUp : position before : " + transform.position);
        Vector3 dest = map.WorldToGrid(transform.position + Map.North);
        yield return coroutineManager.StartTrackedCoroutine(MoveOverSpeed(transform.position + (Map.North * map.ScaleFactor.z), speed));
        if (!map.Contains(dest))
            transform.position = new Vector3(transform.position.x, transform.position.y, map.GridToWorld(Map.South * (map.dimension.y)).z + transform.position.z);
    }

    private IEnumerator MoveDown()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return new WaitForEndOfFrame();
        // print("MoveDOwn : position before : " + transform.position);
        Vector3 dest = map.WorldToGrid(transform.position + Map.South);
        yield return coroutineManager.StartTrackedCoroutine(MoveOverSpeed(transform.position + (Map.South * map.ScaleFactor.z), speed));
        if (!map.Contains(dest))
            transform.position = new Vector3(transform.position.x, transform.position.y, map.GridToWorld(Map.North * (map.dimension.y - 1)).z);
    }

    private IEnumerator MoveLeft()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return new WaitForEndOfFrame();
        Vector3 dest = map.WorldToGrid(transform.position + Map.East);
        //print("MoveLeft : position before : " + transform.position + " dest : " + dest);
        yield return coroutineManager.StartTrackedCoroutine(MoveOverSpeed(transform.position + (Map.East * map.ScaleFactor.x), speed));
        if (!map.Contains(dest))
            transform.position = new Vector3(map.GridToWorld(Map.West * (map.dimension.x)).x + transform.position.x, transform.position.y, transform.position.z);
    }

    private IEnumerator MoveRight()
    {
        while (coroutineManager.IsTrackedCoroutineRunning())
            yield return new WaitForEndOfFrame();
        Vector3 dest = map.WorldToGrid(transform.position + Map.West);
        //print("MoveRIght : position before : " + transform.position + " dest : " + dest);
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
            yield return new WaitForEndOfFrame();
        }
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
