Shader "Custom/Horizon Skybox"
{
    Properties
    {
        _SkyCol1("Sky Colour 1", Color) = (1, 1, 1, 1)
		_SkyCol2("Sky Colour 2", Color) = (1, 1, 1, 1)

		_Horizon("Horizon Height", Float) = 0
		_Transition("Transition Strength", Range(0, 10)) = 1

		[HDR]_SunCol1("Sun Colour 1", Color) = (1, 1, 1, 1)
		_SunSize("Sun Size", Range(0.8, 1)) = 0.96
		_Frequency("Frequency", Float) = 100
		_WaveOffset("Wave Offset", Range(-1, 1)) = 0
		_Decay("Decay", Float) = 10
		_Speed("Speed", Float) = 1
		_Curve("Curve", Float) = 0.15
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Background" }

        Pass
        {
			ZWrite Off
			Cull Off
			Fog { Mode Off }

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 texcoord : TEXCOORD0;
            };

            struct v2f
            {
				float4 vertex : SV_POSITION;
                float3 texcoord : TEXCOORD0;
            };

			float4 _SkyCol1, _SkyCol2, _SunCol1;
			float _SunSize, _Frequency, _WaveOffset, _Decay, _Speed, _Curve, _Horizon, _Transition, _Range;

			float map (float s, float a1, float a2, float b1, float b2) {
				return b1 + (s - a1)*(b2 - b1) / (a2 - a1);
			}

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				i.texcoord = normalize(i.texcoord);
				float TdotL = dot(i.texcoord, _WorldSpaceLightPos0.xyz);

				if (TdotL > _SunSize && (sin(i.texcoord.y * _Frequency + _Time[1] * _Speed) < _WaveOffset * i.texcoord.y * _Decay))
					return _SunCol1;

				float transition = _Transition;
				float horizon = _Horizon + pow((TdotL + 1), 2) * _Curve * _Transition;

				float TdotY = dot(float3(0, 1, 0), _WorldSpaceLightPos0.xyz);

				return lerp(
					_SkyCol1,
					_SkyCol2,
					clamp(
						(i.texcoord.y * transition) - horizon,
						0,
						1
					)
				);
            }

            ENDCG
        }
    }
}
