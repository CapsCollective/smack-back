Shader "Custom/WireframeGlow"
{
	Properties
	{
		[HDR] _Tint("Wireframe Colour", Color) = (1, 1, 1, 1)
		[HDR] _FaceTint("Face Colour", Color) = (1, 1, 1, 1)
		_Weight("Wireframe Weight", Float) = 0.05
		_Smoothing("Wireframe Smoothing", Range(0, 10)) = 1
		[Toggle] _Diag("Enable Diagonals", Float) = 0
	}

	SubShader
	{
		Tags { "Queue" = "Geometry" "RenderType" = "Opaque" }

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma geometry geom

			#pragma multi_compile __ _DIAG_ON

			#include "UnityCG.cginc"

			struct v2g {
				float4 worldPos : SV_POSITION;
				float3 normal : NORMAL;
			};

			struct g2f {
				float4 pos : SV_POSITION;
				float3 bary : TEXCOORD0;
				float3 normal : NORMAL;
			};

			float _Weight, _Smoothing;
			float4 _Tint, _FaceTint;

			v2g vert(appdata_base v) {
				v2g o;
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				return o;
			}

			[maxvertexcount(3)]
			void geom(triangle v2g i[3], inout TriangleStream<g2f> stream) {
				float3 p0 = i[0].worldPos.xyz;
				float3 p1 = i[1].worldPos.xyz;
				float3 p2 = i[2].worldPos.xyz;

				float3 triangleNormal = normalize(cross(p1 - p0, p2 - p0));

				i[0].normal = triangleNormal;
				i[1].normal = triangleNormal;
				i[2].normal = triangleNormal;

				float3 param = float3(0, 0, 0);
				
				#if !_DIAG_ON
				float a = length(i[0].worldPos - i[1].worldPos);
				float b = length(i[1].worldPos - i[2].worldPos);
				float c = length(i[2].worldPos - i[0].worldPos);

				if (a > b && a > c)
					param.y = 1;
				else if (b > c && b > a)
					param.x = 1;
				else
					param.z = 1;
				#endif
				
				g2f g0, g1, g2;
				
				g0.normal = i[0].normal;
				g1.normal = i[1].normal;
				g2.normal = i[2].normal;

				g0.pos = mul(UNITY_MATRIX_VP, i[0].worldPos);
				g1.pos = mul(UNITY_MATRIX_VP, i[1].worldPos);
				g2.pos = mul(UNITY_MATRIX_VP, i[2].worldPos);

				g0.bary = float3(1, 0, 0) + param;
				g1.bary = float3(0, 0, 1) + param;
				g2.bary = float3(0, 1, 0) + param;

				stream.Append(g0);
				stream.Append(g1);
				stream.Append(g2);
			}

			float4 frag(g2f i) : SV_Target{
				float3 deltas = fwidth(i.bary);
				float3 smoothing = deltas * _Smoothing;
				float3 weight = deltas * _Weight;
				i.bary = smoothstep(weight, weight + smoothing, i.bary);
				float minBary = min(i.bary.x, min(i.bary.y, i.bary.z));
				float NdotL = dot(_WorldSpaceLightPos0.xyz, i.normal);
				return lerp(_Tint, _FaceTint * NdotL, minBary);
			}

			ENDCG
		}
	}
}
