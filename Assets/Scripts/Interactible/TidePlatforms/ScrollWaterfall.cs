using UnityEngine;
using System.Collections;

public class ScrollWaterfall : MonoBehaviour
{

    public float WF_speed = 0.75f;

    Renderer WF_renderer;

    void Start()
    {
        WF_renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float TextureOffset = Time.time * WF_speed;
        WF_renderer.material.SetTextureOffset("_MainTex", new Vector3(0, -TextureOffset,0));
    }

}
