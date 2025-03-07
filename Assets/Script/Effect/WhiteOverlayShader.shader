Shader "Custom/WhiteOverlayShader" {
    Properties {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Overlay ("White Overlay", Range(0,1)) = 1.0
    }
    SubShader {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass {
            Cull Off

            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Overlay;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                // 알파 값이 일정 임계값(예: 0.1) 미만이면 픽셀을 버림
                clip(col.a - 0.1);
                // _Overlay가 1이면 완전 흰색, 0이면 원래 색상 (어둡게 하려면 lerp 대상색을 0으로 변경)
                col.rgb = lerp(col.rgb, 1.0, _Overlay);
                return col;
            }

            ENDCG
        }
    }
}
