Shader "Custom/Unlit" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
    }
    
    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100
        
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
    
        Pass {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma target 2.0
    
                #include "UnityCG.cginc"
    
                struct appdata_t {
                    float4 vertex : POSITION;
                };
    
                struct v2f {
                    float4 vertex : SV_POSITION;
                    float4 cameraPos : TEXCOORD0;
                };
    
                fixed4 _Color;
    
                v2f vert (appdata_t v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.cameraPos = ComputeScreenPos(o.vertex);
                    return o;
                }
    
                fixed4 frag (v2f i) : COLOR {
                    fixed4 col = _Color;
                    col.rgb = col.rgb + i.cameraPos.y;
                    return col;
                }
            ENDCG
        }
    }
}