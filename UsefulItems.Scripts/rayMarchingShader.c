const float FOV = 1.0;
const int MAX_STEPS = 256;
const float MAX_DIST = 500.0;
const float EPSILON = 0.001;

void render(inout vec3 col, in vec2 uv) {
    col.rg += uv;
}

void mainImage(out vec4 fragColor, in vec2 fragCoord)
{
    // Normalized pixel coordinates (from -1 to 1)
    vec2 uv = (2.0 * fragCoord - iResolution.xy) / iResolution.xy;

    vec3 col;
    render(col, uv);
    
    // Output to screen
    fragColor = vec4(col,1.0);
}