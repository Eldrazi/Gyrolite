float4 start = float4(0, 0.623, 0.984, 1);
float4 end = float4(0.753, 0.753, 0.682, 1);

float4 ApplyGradient(float2 pos : TEXCOORD0) : COLOR0
{ 
	float4 colour = lerp(start, end, distance(0, pos.y));
	return colour;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 ApplyGradient();
    }
}
