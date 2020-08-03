#define min_f(a,b,c) (min(a,min(b,c)))
#define max_f(a,b,c) (max(a,max(b,c)))

float3 rgb2hsv(float src_r, float src_g, float src_b)
{
    float r = src_r / 255.0f;
    float g = src_g / 255.0f;
    float b = src_b / 255.0f;

    float h, s, v; // h:0-360.0, s:0.0-1.0, v:0.0-1.0

    float maxVal = max_f(r, g, b);
    float minVal = min_f(r, g, b);

    v = maxVal;

    if (maxVal == 0.0f) {
        s = 0;
        h = 0;
    }
    else if (maxVal - minVal == 0.0f) {
        s = 0;
        h = 0;
    }
    else {
        s = (maxVal - minVal) / maxVal;

        if (maxVal == r) {
            h = 60 * ((g - b) / (maxVal - minVal)) + 0;
        }
        else if (maxVal == g) {
            h = 60 * ((b - r) / (maxVal - minVal)) + 120;
        }
        else {
            h = 60 * ((r - g) / (maxVal - minVal)) + 240;
        }
    }

    if (h < 0) h += 360.0f;

    float dst_h = (h / 2);   // dst_h : 0-180
    float dst_s = (s * 255); // dst_s : 0-255
    float dst_v = (v * 255); // dst_v : 0-255
    return float3(dst_h, dst_s, dst_v);
}