import random, requests #Python 3.9.4

num = random.randint(1, 9999999)
qry = ["tabletop", "game", "cover", "page", str(num)]
url = "https://www.google.com/search?&tbm=isch&q=" + "+".join([i for i in qry])
req = requests.get(url)
txt = req.text

trm = "src=\""
cnt = txt.count(trm)
pos = 0
end = 0
mrk = "\""

chk = "&amp;"

lst = []
for i in range(0, cnt):
    pos = txt.find(trm, pos + 1)
    end = txt.find(mrk, pos + len(trm))
    lnk = txt[pos + len(trm):end]
    if lnk.startswith("https://"):
        amp = lnk.find(chk)
        if amp:
            lnk = lnk[:amp]
        lst.append(lnk)

[print(i) for i in lst]
