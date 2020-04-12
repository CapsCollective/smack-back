// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "My Shaders/CRT"
{
	Properties
	{
		_Bars("Bars", Float) = 1000
		_Texture("Texture", 2D) = "white" {}
		_BarSize("Bar Size", Int) = 1
		_TimeScale("Time Scale", Float) = 1
		_Float0("Bar Transparency", Range( 0 , 1)) = 0.75
		[HDR]_Color0("Color 0", Color) = (1,1,1,0)
		_TextureSample1("Pixel Shader", 2D) = "white" {}
		_Pixels("Pixels", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float eyeDepth;
		};

		uniform float4 _Color0;
		uniform sampler2D _Texture;
		uniform float4 _Texture_ST;
		uniform float2 _Pixels;
		uniform sampler2D _TextureSample1;
		uniform float _TimeScale;
		uniform float _Bars;
		uniform int _BarSize;
		uniform float _Float0;


		float4 MyCustomExpression4( float y , float mult , float barSize )
		{
			return 1 - saturate(round(abs(frac(y * mult) * barSize)));
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.eyeDepth = -UnityObjectToViewPos( v.vertex.xyz ).z;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv0_Texture = i.uv_texcoord * _Texture_ST.xy + _Texture_ST.zw;
			float pixelWidth136 =  1.0f / _Pixels.x;
			float pixelHeight136 = 1.0f / _Pixels.y;
			half2 pixelateduv136 = half2((int)(uv0_Texture.x / pixelWidth136) * pixelWidth136, (int)(uv0_Texture.y / pixelHeight136) * pixelHeight136);
			float cameraDepthFade147 = (( i.eyeDepth -_ProjectionParams.y - 0.25 ) / 1.0);
			float temp_output_153_0 = saturate( cameraDepthFade147 );
			float4 lerpResult149 = lerp( tex2D( _TextureSample1, ( ( uv0_Texture * _Pixels ) / float2( 3,3 ) ) ) , float4( 1,1,1,0 ) , temp_output_153_0);
			float2 temp_cast_1 = (( _Time.x * _TimeScale )).xx;
			float2 uv_TexCoord10 = i.uv_texcoord + temp_cast_1;
			float y4 = uv_TexCoord10.y;
			float mult4 = _Bars;
			float barSize4 = (float)_BarSize;
			float4 localMyCustomExpression4 = MyCustomExpression4( y4 , mult4 , barSize4 );
			float4 lerpResult152 = lerp( lerpResult149 , localMyCustomExpression4 , ( _Float0 * temp_output_153_0 ));
			o.Emission = ( ( _Color0 * tex2D( _Texture, pixelateduv136 ) ) * lerpResult152 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16900
344;241;1199;1422;1564.036;356.2575;1;True;False
Node;AmplifyShaderEditor.TexturePropertyNode;2;-2170.564,-587.6356;Float;True;Property;_Texture;Texture;2;0;Create;True;0;0;False;0;None;dffef66376be4fa480fb02b19edbe903;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;137;-1966.245,-381.0933;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;140;-1824.245,-506.0934;Float;False;Property;_Pixels;Pixels;8;0;Create;True;0;0;False;0;0,0;512,512;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;139;-1569.101,-260.1072;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CameraDepthFade;147;-1160.32,-38.4996;Float;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0.25;False;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;91;-1746.653,64.66962;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-1630.962,205.8491;Float;False;Property;_TimeScale;Time Scale;4;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;141;-1404.001,-225.6073;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT2;3,3;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SaturateNode;153;-866.0364,-114.2575;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;-1324.401,120.5326;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;155;-628.0364,-58.25751;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-991.2001,250.5296;Float;False;Property;_Bars;Bars;1;0;Create;True;0;0;False;0;1000;250;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;10;-1124.617,113.9972;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;25;-874.5125,-14.36932;Float;False;Property;_Float0;Float 0;5;0;Create;True;0;0;False;0;0.75;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCPixelate;136;-1417.201,-403.2073;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT;128;False;2;FLOAT;128;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;138;-1211.628,-228.2107;Float;True;Property;_TextureSample1;Texture Sample 1;7;0;Create;True;0;0;False;0;None;b79606207e03e9d4cabd954a21b13d05;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.IntNode;6;-951.1982,356.9405;Float;False;Property;_BarSize;Bar Size;3;0;Create;True;0;0;False;0;1;1;0;1;INT;0
Node;AmplifyShaderEditor.ColorNode;92;-1103.034,-833.2859;Float;False;Property;_Color0;Color 0;6;1;[HDR];Create;True;0;0;False;0;1,1,1,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;149;-657.5184,-238.2107;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;1,1,1,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-1232.125,-604.6438;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CustomExpressionNode;4;-737.3419,112.2024;Float;False;return 1 - saturate(round(abs(frac(y * mult) * barSize)))@;4;False;3;True;y;FLOAT;0;In;;Float;False;True;mult;FLOAT;1000;In;;Float;False;True;barSize;FLOAT;0;In;;Float;False;My Custom Expression;True;False;0;3;0;FLOAT;0;False;1;FLOAT;1000;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;154;-590.0364,-88.25751;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;93;-814.6647,-502.2126;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;152;-401.8408,-116.9079;Float;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;142;-106.8104,-174.3194;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;150.3981,-123.2447;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;My Shaders/Billboard;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;137;2;2;0
WireConnection;139;0;137;0
WireConnection;139;1;140;0
WireConnection;141;0;139;0
WireConnection;153;0;147;0
WireConnection;69;0;91;1
WireConnection;69;1;8;0
WireConnection;155;0;153;0
WireConnection;10;1;69;0
WireConnection;136;0;137;0
WireConnection;136;1;140;1
WireConnection;136;2;140;2
WireConnection;138;1;141;0
WireConnection;149;0;138;0
WireConnection;149;2;153;0
WireConnection;1;0;2;0
WireConnection;1;1;136;0
WireConnection;4;0;10;2
WireConnection;4;1;5;0
WireConnection;4;2;6;0
WireConnection;154;0;25;0
WireConnection;154;1;155;0
WireConnection;93;0;92;0
WireConnection;93;1;1;0
WireConnection;152;0;149;0
WireConnection;152;1;4;0
WireConnection;152;2;154;0
WireConnection;142;0;93;0
WireConnection;142;1;152;0
WireConnection;0;2;142;0
ASEEND*/
//CHKSM=4F202376A13BE53195EE8171621771233BD35B62