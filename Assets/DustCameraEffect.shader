Shader "Hidden/DustCameraEffect"
{
    Properties
    {
        _MainTex ("Screen Texture", 2D) = "white" {}
        _FrontTex ("Front Texture", 2D) = "white" {}
		_FrontCol ("Front Colour",Color) = (1,1,1,1)
        _BackTex ("Back Texture", 2D) = "white" {}
		_BackCol ("Back Colour",Color)= (1,1,1,1)

		
		_Threshold ("Threshold",range(0,1))=0.5
    }
    SubShader
    {
        // No culling or depth
		//Blend SrcAlpha OneMinusSrcAlpha
        Cull Off ZWrite On ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

			
            sampler2D _MainTex;
            sampler2D _FrontTex;
			float4 _FrontTex_ST;
			float4 _FrontCol;
            sampler2D _BackTex;
            float4 _BackTex_ST;
			float4 _BackCol;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

			//int _Steps;
			fixed _Threshold;
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 output;
                fixed4 front = tex2D(_FrontTex, TRANSFORM_TEX(i.uv,_FrontTex))*_FrontCol;
                fixed4 back = tex2D(_BackTex, TRANSFORM_TEX(i.uv,_BackTex))*_BackCol;
                if(col.a>_Threshold)
				{
					output.rgb = front;
				}
				else if (col.a>_Threshold-0.1)
				{
					output.rgb = front*0.8;
				}
				else
				{
				output.rgb = lerp(back,front*col.a/_Threshold,col.a/_Threshold);//lerp(back,front, ceil(col.a*(_Steps/_Threshold))/(_Steps/_Threshold));
				
				}
				

                return output;
            }
            ENDCG
        }
    }
}
