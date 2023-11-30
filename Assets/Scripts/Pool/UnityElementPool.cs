using UnityEngine;
using UnityEngine.Pool;

public class UnityElementPool : Pool
{
    [SerializeField] Factory _elementFactory;

    private IObjectPool<GameObject> _elements;

    // Start is called before the first frame update
    void Start()
    {
        _elements = new ObjectPool<GameObject>(CreateNewElement, OnGettingElement, OnReleasingElement, OnDestroyingElement);
    }

    private GameObject CreateNewElement()
    {
        return _elementFactory.Generate(Vector3.zero);
    }

    private void OnGettingElement(GameObject bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleasingElement(GameObject bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyingElement(GameObject bullet)
    {
        Destroy(bullet);
    }

    public override GameObject Get(Vector2 position)
    {
        GameObject go = _elements.Get();
        go.transform.position = position;
        go.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        return go;
    }

    public override void Release(GameObject element)
    {
        _elements.Release(element);
    }
}