using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenToggle : MonoBehaviour
{
    public Material mat1, mat2;

    private MeshRenderer _meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(Toggle());
    }

    private IEnumerator Toggle()
    {
        while (true)
        {
            _meshRenderer.material = mat1;
            yield return new WaitForSeconds(3);
            _meshRenderer.material = mat2;
            yield return new WaitForSeconds(3);
        }
    } 
}
