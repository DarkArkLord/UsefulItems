vec2 getNormalCoords(vec2 cur) 
{
    float divider = iResolution.y < iResolution.x 
        ? iResolution.y 
        : iResolution.x;
    return (2.0 * cur - iResolution.xy) / divider;
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
    float loopSteps = 100.0;
    
    vec2 t = vec2(cur);
    float i;
    for(i = 0.0; i < loopSteps; i++) 
    {
        t = sqr(t) + cur;
        float l = len(t);
        if(l > 4.0) break;
    }
    return vec3(1.0 - i / loopSteps);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 cur = getNormalCoords(fragCoord) - vec2(0.45, 0);
    vec3 color = checkPoint(cur);
    // Output to screen
    fragColor = vec4(color,1.0);
}