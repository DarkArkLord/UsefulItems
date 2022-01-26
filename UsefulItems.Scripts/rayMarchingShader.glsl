const float FOV = 1.0;
const int MAX_STEPS = 256;
const float MAX_DIST = 500.0;
const float EPSILON = 0.001;

/***************
 * hg_sdf.glsl *
 ***************
 *    START    *
 ***************/

float fSphere(vec3 p, float r) {
    return length(p) - r;
}

float fPlane(vec3 p, vec3 n, float distanceFromOrigin) {
	return dot(p, n) + distanceFromOrigin;
}

vec3 pMod3(inout vec3 p, vec3 size) {
	vec3 c = floor((p + size*0.5)/size);
	p = mod(p + size*0.5, size) - size*0.5;
	return c;
}

/***************
 *     END     *
 ***************/

vec2 fOpUniouId(vec2 a, vec2 b) {
    return a.x < b.x ? a : b;
}

vec2 map(vec3 p) {
    // plane
    float planeDist = fPlane(p, vec3(0, 1, 0), 1.0);
    float planeId = 2.0;
    vec2 plane = vec2(planeDist, planeId);
    // sphere
    float sphereDist = fSphere(p, 1.0);
    float sphereId = 1.0;
    vec2 sphere = vec2(sphereDist, sphereId);
    // result
    vec2 res = fOpUniouId(sphere, plane);
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

vec3 getNormal(vec3 p) {
     vec2 e = vec2(EPSILON, 0.0);
    vec3 n = vec3(map(p).x) - vec3(map(p - e.xyy).x, map(p - e.yxy).x, map(p - e.yyx).x);
    return normalize(n);
}

vec3 getLight(vec3 p, vec3 rd, vec3 color) {
    vec3 lightPos = vec3(20.0, 40.0, -30.0);
    vec3 L = normalize(lightPos - p);
    vec3 N = getNormal(p);

    vec3 diffuse = color * clamp(dot(L, N), 0.0, 1.0);

    // shadows
    float d = rayMarch(p + N * 0.02, normalize(lightPos)).x;
    if (d < length(lightPos - p)) return vec3(0);

    return diffuse;
}

vec3 getMaterial(vec3 p, float id) {
    vec3 m;
    switch (int(id)) {
        case 1:
        m = vec3(0.9, 0.0, 0.0); 
        break;
        case 2:
        m = vec3(0.2 + 0.4 * mod(floor(p.x) + floor(p.z), 2.0)); 
        break;
    }
    return m;
}

void render(inout vec3 col, in vec2 uv) {
    vec3 ro = vec3(0.0, 0.0, -3.0);
    vec3 rd = normalize(vec3(uv, FOV));
    
    vec2 object = rayMarch(ro, rd);
    vec3 background = vec3(0.5, 0.8, 0.9);
    if(object.x < MAX_DIST) {
        vec3 p = ro + object.x * rd;
        vec3 material = getMaterial(p, object.y);
        col += getLight(p, rd, material);
    } else {
        col += background;
    }
}

void mainImage(out vec4 fragColor, in vec2 fragCoord)
{
    // Normalized pixel coordinates (from -1 to 1)
    vec2 uv = (2.0 * fragCoord - iResolution.xy) / iResolution.xy;

    vec3 col;
    render(col, uv);

    // gamma correction
    col = pow(col, vec3(0.4545));
    // Output to screen
    fragColor = vec4(col,1.0);
}