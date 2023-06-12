Shader "ImageEffect/fae"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
	_red("red",Float) = 0.29
	_blue( "blue",Float ) = 0.12
	_green( "green",Float ) = 0.59
		_alpha("alpha",Float) = 0.59
		 _shadowThreshold("shaddow threshold",Float)=0.5
		_shadowColor("shadowColor",color)=(0,0,0,0)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
			float _red;
			float _green;
			float _blue;
			float _alpha;
			half4 _shadowColor;
			float _shadowThreshold;
            sampler2D _MainTex;

			fixed4 frag(v2f i) : SV_Target
			{
				float red = _red;
			float green = _green;
			float blue = _blue;
		
                fixed4 col = tex2D(_MainTex, i.uv);
				half4 tex = col;
				half4 t = (_red ,_green, _blue, _alpha);
			float luminance = (col.r * 0.29 + col.g * 0.59 + col.b * 0.12);
			if (luminance > _shadowThreshold) {
				col.rgb = 1;
			}
			else {
				col.rgb = 0;
			}
			
                // just invert the colors
                col.rgb =lerp (_shadowColor.rgb,t,col.r);
                return col;
            }
            ENDCG
        }
    }
}
