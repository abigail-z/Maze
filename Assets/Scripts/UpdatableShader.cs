using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatableShader : MonoBehaviour
{
    private Renderer[] renderers;
	
	void Start ()
    {
        ShaderManager.Instance.StateChange += UpdateShaders;
	}

    void UpdateShaders()
    {
        if (renderers == null)
        {
            renderers = GetComponentsInChildren<Renderer>(true);
        }

        foreach (Renderer r in renderers)
        {
            r.material.shader = ShaderManager.Instance.CurrentShader;
        }
    }

    void OnDestroy()
    {
        ShaderManager.Instance.StateChange -= UpdateShaders;
    }
}
