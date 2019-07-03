using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCameraPostProcess : MonoBehaviour
{
    public Shader shader;
    [Range(0, 20)]
    public int steps;
    [Range(0, 1)]
    public float threshold;

    public Material mat;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        mat.SetTexture("_MainTex", source);
        mat.SetInt("_Steps", steps);
        mat.SetFloat("_Threshold", threshold);
        Graphics.Blit(source, destination, mat);
    }
}
