from PIL import Image
import random

w = 1920
h = 1080

img = Image.new("RGBA", (w, h))

for y in range(0, h):
    for x in range(0, w):
        rng = random.randint(0, 5)
        r = rng
        g = rng
        b = rng
        a = 255
        img.putpixel((x, y), (r, g, b, a))

n = 25
yx = [0] * n
ddd = [-1, 0, 1]
c = 99
cc = 100
dd = 0
for y in range(0, n):
    yx[y] = random.randint(0, h)
    for x in range(0, w):
        d = random.randint(0, cc)
        dd = ddd[random.randint(0, len(ddd) - 1)] if d > c else dd
        yx[y] += dd
        yx[y] = 0 if yx[y] < 0 else h - 1 if yx[y] >= h else yx[y]
        r = 255
        g = 0
        b = 0
        a = 255
        img.putpixel((x, yx[y]), (r, g, b, a))

img.save("tex.png")
print("Done!")
