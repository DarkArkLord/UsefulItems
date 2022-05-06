vec2 getNormalCoords(vec2 cur) 
{
    return (cur / iResolution.xy - vec2(0.5)) * 2.0;
}

vec2 mul(vec2 a, vec2 b)
{
    float x = a.x * b.x - a.y * b.y;
    float y = a.x * b.y + a.y * b.x;
    return vec2(x, y);
}

vec2 sqr(vec2 cur)
{
    return mul(cur, cur);
}

float len(vec2 cur) 
{
    return cur.x * cur.x + cur.y * cur.y;
}

vec3 checkPoint(vec2 cur) 
{
    int loopSteps = 5000;
    
    vec3 white = vec3(1.0);
    vec3 black = vec3(0.0);
    
    vec2 t = vec2(cur);
    for(int i = 0; i < loopSteps; i++) 
    {
        t = sqr(t) + cur;
        if(len(t) > 4.0)
        {
            return white;
        }
    }
    return black;
}

void mainImage( out vec4 outColor, in vec2 inCoord)
{
    vec2 cur = getNormalCoords(inCoord) - vec2(0.45, 0);
    vec3 color = checkPoint(cur);
    outColor = vec4(color,1.0);
}