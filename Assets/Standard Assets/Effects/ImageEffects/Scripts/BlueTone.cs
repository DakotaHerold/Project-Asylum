using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    public class BlueTone : ImageEffectBase
	{
        // Called by camera to apply image effect
        void OnRenderImage (RenderTexture source, RenderTexture destination)
		{
            Graphics.Blit (source, destination, material);
        }
    }
}
