const float FOV = 1.0;
const int MAX_STEPS = 256;
const float MAX_DIST = 500.0;
const float EPSILON = 0.001;

vec2 map(vec3 p) {
    float sphereDist = length(p) - 1.0;
    float sphereId = 1.0;
    vec2 sphere = vec2(sphereDist, sphereId);

    vec2 res = sphere;
    return res;
}

vec2 rayMarch(vec3 ro, vec3 rd) {
    vec2 hit, object;
    for(int i = 0; i < MAX_STEPS; i++) {
        vec3 p = ro + object.x * rd;
        hit = map(p);
        object.x += hit.x;
        object.y = hit.y;
        if(abs(hit.x) < EPSILON || object.x > MAX_DIST) {
            break;
        }
    }
    return object;
}

void render(inout vec3 col, in vec2 uv) {
    vec3 ro = vec3(0.0, 0.0, -3.0);
    vec3 rd = normalize(vec3(uv, FOV));
    
    vec2 object = rayMarch(ro, rd);
    if(object.x < MAX_DIST) {
        col += 3.0 / object.x;
    }
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