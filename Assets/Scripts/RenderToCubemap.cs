using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderToCubemap : MonoBehaviour
{
    public RenderTexture cubemap;
private void Start()
{
}

    void Update()
    {
        GetComponent<Camera>().RenderToCubemap(cubemap);
    }
}
