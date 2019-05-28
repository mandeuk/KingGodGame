Shader "Unlit/UnlitColorCullOff"
{
	    Properties {
        _Color ("Main Color", COLOR) = (0,0,0,1)
    }
    SubShader {
		Tags { "RenderType"="TransperentCutout" }
        Pass {
            Material {
                Diffuse [_Color]
            }
            Lighting On
			Cull Off
        }
    }
}
