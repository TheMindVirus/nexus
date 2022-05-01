# nexus (mobile)

### Demo: https://themindvirus.github.io/nexus/mobile

![IMG_5521](/mobile/IMG_5521.jpeg)
![IMG_5523](/mobile/IMG_5523.jpeg)
```js
var leftStickTouchDrag = false;
var leftStickTouchStart =(event)=> { leftStickTouchDrag = true; console.log("Touch Start Left"); }
var leftStickTouchMove =(event)=>
{
    if (!leftStickTouchDrag) { return; }
    var rect = event.target.getBoundingClientRect();
    var x = event.touches[0].clientX - rect.left - (rect.width / 2.0);
    var y = event.touches[0].clientY - rect.top - (rect.height / 2.0); y *= -1.0;
    var s = 0.001;
    DirectionX = s*x; DirectionY = 0.0; DirectionZ = s*y;
    console.log("Touch Move Left " + x + " " + y);
}
var leftStickTouchEnd =(event)=> { leftStickTouchDrag = false; console.log("Touch End Left");
    DirectionX = 0.0; DirectionY = 0.0; DirectionZ = 0.0; }
var leftStickTouchCancel =(event)=> { leftStickTouchDrag = false; console.log("Touch Cancel Left");
    DirectionX = 0.0; DirectionY = 0.0; DirectionZ = 0.0; }

var rightStickTouchDrag = false;
var rightStickTouchStart =(event)=> { rightStickTouchDrag = true; console.log("Touch Start Right"); }
var rightStickTouchMove =(event)=>
{
    if (!rightStickTouchDrag) { return; }
    var rect = event.target.getBoundingClientRect();
    var x = event.touches[0].clientX - rect.left - (rect.width / 2.0);
    var y = event.touches[0].clientY - rect.top - (rect.height / 2.0); y *= -1.0;
    var s = 0.01;
    ViewX = -s*y; ViewY = s*x; ViewZ = 0.0;
    console.log("Touch Move Right " + x + " " + y);
}
var rightStickTouchEnd =(event)=> { rightStickTouchDrag = false; console.log("Touch End Right");
    ViewX = 0.0; ViewY = 0.0; ViewZ = 0.0; }
var rightStickTouchCancel =(event)=> { rightStickTouchDrag = false; console.log("Touch Cancel Right");
    ViewX = 0.0; ViewY = 0.0; ViewZ = 0.0; }

var leftStickMouseDrag = false;
var leftStickMouseDown =(event)=> { leftStickMouseDrag = true; console.log("Mouse Down Left"); }
var leftStickMouseMove =(event)=>
{
    if (!leftStickMouseDrag) { return; }
    var rect = event.target.getBoundingClientRect();
    var x = event.x - rect.left - (rect.width / 2.0);
    var y = event.y - rect.top - (rect.height / 2.0);
    var s = 0.001;
    DirectionX = s*x; DirectionY = 0.0; DirectionZ = -s*y;
    console.log("Mouse Move Left " + x + ", " + y);
}
var leftStickMouseUp =(event)=> { leftStickMouseDrag = false; console.log("Mouse Up Left");
    DirectionX = 0.0; DirectionY = 0.0; DirectionZ = 0.0; }
var leftStickMouseOut =(event)=> { leftStickMouseDrag = false; console.log("Mouse Out Left");
    DirectionX = 0.0; DirectionY = 0.0; DirectionZ = 0.0; }

var rightStickMouseDrag = false;
var rightStickMouseDown =(event)=> { rightStickMouseDrag = true; console.log("Mouse Down Right"); }
var rightStickMouseMove =(event)=>
{
    if (!rightStickMouseDrag) { return; }
    var rect = event.target.getBoundingClientRect();
    var x = event.x - rect.left - (rect.width / 2.0);
    var y = event.y - rect.top - (rect.height / 2.0);
    var s = 0.01;
    ViewX = s*y; ViewY = s*x; ViewZ = 0.0;
    console.log("Mouse Move Right " + x + ", " + y);
}
var rightStickMouseUp =(event)=> { rightStickMouseDrag = false; console.log("Mouse Up Right");
    ViewX = 0.0; ViewY = 0.0; ViewZ = 0.0; }
var rightStickMouseOut =(event)=> { rightStickMouseDrag = false; console.log("Mouse Out Right");
    ViewX = 0.0; ViewY = 0.0; ViewZ = 0.0; }
```
![IMG_5524](/mobile/IMG_5524.jpeg)