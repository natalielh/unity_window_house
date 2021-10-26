Shader "Unlit/Transparent Colored Add Double Sided" {
    Properties {
		[HDR]_Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    }

    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        
		Cull Off
        ZWrite Off
        Lighting Off
        Fog { Mode Off }

        //soft-add blend effect
        Blend One One

        Pass {
            Color [_Color]
            SetTexture [_MainTex] { combine texture * primary } 
        }
    }
}