
![Logo](https://images2.imgbox.com/c6/8b/yUbLlonJ_o.jpg)



# Exploring a Spinning Doughnut[![AGPL License](https://img.shields.io/badge/CSharp-Project-purple)](https://dotnet.microsoft.com/en-us/languages/csharp) 
    



## Donut Math
This report dives into the creation of a rotating/spinning torus or doughnut.

The implementation contains fundamental mathematical concepts and techniques to generate an animated display of a three-dimensional object projected onto a two-dimensional surface, Through the manipulation of angles and coordinates, coupled with graphical rendering within a Windows Forms application.







## Explanation Of Doughnut Math :

The spinning torus, also known as a "doughnut”, is a shape formed by rotating a circle in three-dimensional space.

Mathematically, it is defined by two radiuses, R1 and R2,
which determine the size and position of the torus.

![App Screenshot](https://images2.imgbox.com/be/e8/eNeYudOB_o.png)

The torus is then projected onto a two-dimensional plane located a certain distance away from the viewer, resulting in a flattened representation that mimics the perception of depth.

## Projecting a 3D Object onto a 2D Screen :

(x,y,z) is the object points in the 3D-Space, In order to project a 3D object onto a 2D screen,we imagine an imaginary flat surface positioned at
a specific distance (denoted as z') from the viewer.

![App Screenshot](https://images2.imgbox.com/fc/d0/6w31dxJq_o.png)

This surface (z') acts as a reference onto which the 3D points, represented as (x, y, z), are projected, through this projection we determine their corresponding positions  (x', y') on the 2D screen.

##### __Note :__ 
In the Since we’re looking from the side, we can only see the y and z axes, but the math works the same for the x axis.

We can notice that the origin, the y-axis, and the point (x, y, z) create a right triangle,
and a similar right triangle is also formed with the points (x', y', z').
So we can use the Triangle Similarity Theorem and find out that relative proportions are maintained.

![App Screenshot](https://images2.imgbox.com/10/db/94m54JO4_o.png)

##### For example :
if we want to see an object which is 10 units wide in our 3D space,
set back 5 units from the viewer, then *K1* should be chosen so that the projection of
the point x=10, z=5 is still on the screen with
*x’* < 50 ==> 10*K1*/5 < 50 ==> *K1* < 25.

- To make cost-effective computations, we use the inverse of the z-coordinate 
    denoted as  *z^(-1)=1/z* by precomputing *z^(-1)*, we can use it when computing the  actual (x', y') positions.

- Dividing once by z and then multiplying by z-1 twice is cheaper than dividing by z twice, this optimization helps in speeding up the rendering process.

## Drawing the Torus : 

Circle of radius __R1__ centered at point __(R2,0,0)__, θ — from 0 to 2π:

![App Screenshot](https://images2.imgbox.com/2f/eb/IoV8Lu63_o.png)

Now we take that circle and rotate it around the y-axis by another angle, to rotate a 3D point around an axe the standard technique is to multiply by a rotation matrix.

![App Screenshot](https://images2.imgbox.com/ea/f4/00u2IE9S_o.png)

We also want the whole donut to spin around on at least two more axes for the animation.

Rotation about the x-axis by __A__ and a rotation about the z-axis by __B__.

![App Screenshot](https://images2.imgbox.com/2a/63/xuMj4XOj_o.png)

Working through the above gets us an (x,y,z) point on the surface of our torus,
rotated around two axes.

To actually get screen coordinates, we need to move the torus somewhere in front of the viewer (the viewer is at the origin) — so we just add some constant to z to move it backward.

So we have another constant to pick, call it __K2__, for the distance of the donut from the viewer, and our projection now looks like:
 
![App Screenshot](https://images2.imgbox.com/06/99/cmpP0GMr_o.png)

- K1 and K2 can be tweaked together to change the field of view and flatten or exaggerate the depth of the object.

## Implementation :

- We use a bitmap to draw/represent the rotating donut points. 
- Mathematical calculations determine each point's position and color on the donut's surface, which is projected onto the 2D bitmap.
- Double buffering ensures smooth animation by drawing frames onto a hidden buffer before displaying them seamlessly, this process creates a visually engaging spinning donut animation, showcasing the intersection of mathematics and computer graphics.

## References :

Donut Math :
https://www.a1k0n.net/2011/07/20/donut-math.html	

Rotation Matrix :
https://en.wikipedia.org/wiki/Rotation_matrix	

 


## Credits

This project is maintained by @malikmaky.

For any inquiries or feedback, please contact malikmhmd@hotmail.com


## Demo

Check out the demo through this youtube video down bellow
https://www.youtube.com/watch?v=40IfGnu9R9Q
