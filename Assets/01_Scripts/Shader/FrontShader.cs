using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FrontShader : MonoBehaviour
{
    [SerializeField] bool IsPlayerChar;
    [SerializeField] Shader frontMaskShader;
    [SerializeField] Shader baseMaskShader;
    [SerializeField] Shader frontShader;
    [SerializeField] Shader baseShader;
    public List<Material> mats = new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
        if (IsPlayerChar)
        {
            var Renderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach(Renderer renders in Renderers)
            {
                for (int i = 0; i < renders.materials.Length +1; i++)
                {
                    if (i == 1)
                    {
                        renders.material.shader = frontMaskShader;
                    }
                    else if (i == 2)
                    {
                        renders.material.shader = frontShader;
                    }
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
