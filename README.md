#### 👋 Testing out some unity procedural stuff

Testing out procedural generation.

1. Make simple UI to use perlin noise, show it on an image as perlin noise scale and offset is adjusted
2. After clicking generate mesh we will create a mesh that will triangulate all the points. (current version is just a simple array of plains with different colors).

Unsure if we should put x,y from the perlin noise as x and z since we want the height to be the walls of our dungeon, but for now lets keep x,y as x,y. 
Seems like unity "flipps" the images, so an easy fix is to do tiling -1 on x and y axis. For the image where we show the perlin noise.
