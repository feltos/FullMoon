using UnityEngine;
using System.Collections;

public class ScrollUV : MonoBehaviour
{

    public float speed = 0.75f;

    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float TextureOffset = Time.time * speed;
        renderer.material.SetTextureOffset("_MainTex", new Vector3(0, -TextureOffset,0));
    }

}
