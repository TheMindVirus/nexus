```html
<!DOCTYPE html>
<html lang="en-us">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <title>Unity WebGL Player | Nexus</title>
    <link rel="shortcut icon" href="TemplateData/favicon.ico">
    <link rel="stylesheet" href="TemplateData/style.css">
    <script src="./Build/Output.loader.js"></script>
    <script>
var instance = null;

var play = function(element)
{
    element.remove();
    var container = document.querySelector("#unity-container");
    var canvas = document.querySelector("#unity-canvas");
    var loadingBar = document.querySelector("#unity-loading-bar");
    var progressBarFull = document.querySelector("#unity-progress-bar-full");

    var buildUrl = "Build";
    var loaderUrl = buildUrl + "/Output.loader.js";
    var config =
    {
        dataUrl: buildUrl + "/Output.data.unityweb",
        frameworkUrl: buildUrl + "/Output.framework.js.unityweb",
        codeUrl: buildUrl + "/Output.wasm.unityweb",
        streamingAssetsUrl: "StreamingAssets",
        companyName: "TheMindVirus",
        productName: "Nexus",
        productVersion: "1.0",
    };

    canvas.style.width = "100vw";
    canvas.style.height = "100vh";
    canvas.ondragstart = function(event) { event.preventDefault(); }

    loadingBar.style.display = "block";

    createUnityInstance(canvas, config, (progress) =>
    {
        progressBarFull.style.width = 100 * progress + "%";
    }).then((unityInstance) =>
    {
        instance = unityInstance;
        loadingBar.style.display = "none";
        paint(); paint(); paint(); //Initial Load-In looked a bit blank
        setInterval(() => { paint(); }, 60000);
    }).catch((message) => { alert(message); });
}

var paint =()=>
{
    var url = "query.php?https://www.google.com/search?&tbm=isch&q=";
    var query = ["tabletop", "game", "cover", "page", parseInt(Math.random() * 9999999).toString()];
    for (var i = query.length; i > 0; --i)
    {
        url += query[query.length - i];
        if (i > 1) { url += "+"; }
    }

    var xmlhttp = new XMLHttpRequest();
    xmlhttp.open("GET", url, true);
    xmlhttp.onreadystatechange =()=>
    {
        if ((xmlhttp.readyState == 4) && (xmlhttp.status == 200))
        {
            process(xmlhttp.responseText);
        }
    }
    xmlhttp.send();
}

var process =(text)=>
{
    var count = 0;
    var term = "src=\"";
    var mark = "\"";
    var matches = text.match(new RegExp(term, 'g'));
    if (matches != null) { count = matches.length; }

    var start = 0;
    var end = 0;
    var links = [];
    for (var i = 0; i < count; ++i)
    {
        start = text.indexOf(term, start + 1);
        end = text.indexOf(mark, start + term.length);
        var link = text.substring(start + term.length, end);
        if (link.startsWith("https://"))
        {
            var amp = link.indexOf("&amp;");
            if (amp > -1) { link = link.substring(0, amp); }
            links.push(link);
        }
    }
    for (var i = 0; i < links.length; ++i) { download(links[i]); }
}

var download =(link)=>
{
    var request = new XMLHttpRequest();
    request.overrideMimeType("image/jpeg");
    request.open("GET", "jpeg.php?" + link, true);
    request.onreadystatechange =()=>
    {
        if ((request.readyState == 4) && (request.status == 200))
        {
            var data = request.response;
            var panel = Math.random() * 36;
            var json = '{"panel": ' + panel + ', "data": "' + data + '"}';
            instance.SendMessage("Nexus", "SetPanelData", json);
        }
    }
    request.send(0);
}
    </script>
    <style>
* { margin: auto; text-align: center; overflow: hidden; }
body { position: absolute; top: 0; left: 0; width: 100vw; height: 100vh; background-color: black; }
.webgl-content { position: absolute; top: 0 left: 0; width: 100%; height: 100%; text-shadow: 1px 1px 10px white; }
.unityContainer { position: absolute; top: 0; left: 0; width: 100%; height: 100%; }
.footer { position: absolute; top: 0; width: 100%; height: 30px; background-color: cyan; box-shadow: 1px 1px 10px black; opacity: 0.5; }
.webgl-logo { position: relative; top: 0; left: 0; background-color: white; z-index: 9999; }
.fullscreen { position: relative; top: 0; right: 0; }
.title { position: relative; bottom: 0; font-weight: bold; }
#play-button { position: fixed; top: 0; left: 0; width: 100%; height: 100%; text-align: center; color: blue; background: black; border: none; font-size: 100pt; user-select: none; z-index: 9999; }
    </style>
  </head>
  <body>
    <button id="play-button" onclick="play(this);">Play</button>
    <div id="unity-container" class="unity-desktop">
      <canvas id="unity-canvas" width="100%" height="100%" onclick="requestPointerLock();"></canvas>
      <div id="unity-loading-bar">
        <div id="unity-logo"></div>
        <div id="unity-progress-bar-empty">
          <div id="unity-progress-bar-full"></div>
        </div>
      </div>
    </div>
  </body>
</html>
```