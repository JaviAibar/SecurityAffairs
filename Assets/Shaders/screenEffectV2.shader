// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:False,rpth:0,vtps:0,hqsc:False,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:6,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:1,qpre:4,rntp:5,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32813,y:33253,varname:node_2865,prsc:2|emission-525-OUT;n:type:ShaderForge.SFN_TexCoord,id:4219,x:31938,y:33237,cmnt:Default coordinates,varname:node_4219,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Relay,id:8397,x:32163,y:33237,cmnt:Refract here,varname:node_8397,prsc:2|IN-4219-UVOUT;n:type:ShaderForge.SFN_Relay,id:4676,x:32470,y:33354,cmnt:Modify color here,varname:node_4676,prsc:2|IN-7542-RGB;n:type:ShaderForge.SFN_Tex2dAsset,id:4430,x:31938,y:33424,ptovrint:False,ptlb:MainTex,ptin:_MainTex,cmnt:MainTex contains the color of the scene,varname:node_9933,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7542,x:32293,y:33354,varname:node_1672,prsc:2,ntxv:0,isnm:False|UVIN-8397-OUT,TEX-4430-TEX;n:type:ShaderForge.SFN_Noise,id:1983,x:32440,y:33522,varname:node_1983,prsc:2;n:type:ShaderForge.SFN_Multiply,id:525,x:32601,y:33354,varname:node_525,prsc:2|A-4676-OUT,B-1983-OUT;n:type:ShaderForge.SFN_Time,id:7618,x:32184,y:33506,varname:node_7618,prsc:2;proporder:4430;pass:END;sub:END;*/

Shader "Shader Forge/screenEffectV2" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Overlay+1"
            "RenderType"="Overlay"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZTest Always
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float2 node_8397 = i.uv0; // Refract here
                float4 node_1672 = tex2D(_MainTex,TRANSFORM_TEX(node_8397, _MainTex));
                float2 node_1983_skew = i.uv0 + 0.2127+i.uv0.x*0.3713*i.uv0.y;
                float2 node_1983_rnd = 4.789*sin(489.123*(node_1983_skew));
                float node_1983 = frac(node_1983_rnd.x*node_1983_rnd.y*(1+node_1983_skew.x));
                float3 emissive = (node_1672.rgb*node_1983);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
