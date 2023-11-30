using UnityEngine;

public class ElementFactory : Factory
{
    [SerializeField] private GameObject _elementPrefab;

    public override GameObject Generate(Vector3 position)
    {
        GameObject go = Instantiate(_elementPrefab);
        go.transform.position = position;
        return go;
    }
}
