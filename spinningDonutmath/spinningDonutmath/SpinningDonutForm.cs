using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace spinningDonutmath
{
    public partial class SpinningDonutForm : Form
    {
        const double thetaAngleSpacing = 0.07;
        const double phiAngleSpacing = 0.02;

        const double R1 = 1;            //R1 : Torus of radius
        const double R2 = 2;            //R2 : Torus centered at point (R2,0,0)
        const double K2 = 25;           //K2 : Distance of the donut from the viewer
        static double K1;               //K1 => z' : Imaginary flat surface located at z’ units away from the viewer
        static int screenWidth = 500;   
        static int screenHeight = 500;
        static double A = 0;            //A : Rotation around the x-axis
        static double B = 0;            //B : Rotation around the z-axis
        static Bitmap donutBitMap;
        static Graphics gfx;

        public SpinningDonutForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true); // Enables Double-Buffering for smooth rendering
            this.ClientSize = new Size(screenWidth, screenHeight);
            this.Text = "Spinning Donut";
            this.Paint += FormPaint;
            this.Resize += FormResize;

            K1 = Math.Min(screenWidth, screenHeight) * K2 * 3.5 / (8 * (R1 + R2));  // K1 is calculated based on screen size
            donutBitMap = new Bitmap(screenWidth, screenHeight);                    
            gfx = Graphics.FromImage(donutBitMap);


            Timer timer = new Timer();
            timer.Interval = 50;                                                    // Adjust the interval for desired speed
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        static void FormPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(donutBitMap, Point.Empty);                         // Draw the bitmap onto the form
        }

        static void FormResize(object sender, EventArgs e)
        {
            // Reinitialize form and bitmap when resized
            screenWidth = ((Form)sender).ClientSize.Width;
            screenHeight = ((Form)sender).ClientSize.Height;
            K1 = Math.Min(screenWidth, screenHeight) * K2 * 3.5 / (8 * (R1 + R2));

            try
            {
                donutBitMap = new Bitmap(screenWidth, screenHeight);
            }catch(Exception) { }
            gfx = Graphics.FromImage(donutBitMap);
        }

        static void Timer_Tick(object sender, EventArgs e)
        {
            gfx.Clear(Color.Black);             // Clear the bitmap


            frameRender(A, B);                  // Render frame

            A += 0.02f;                         // Increment angles for the next frame
            B += 0.07f;

            try
            {
                Form.ActiveForm.Invalidate();   // Redraw the form
            }
            catch (Exception)
            {

            }
        }

        static void frameRender(double A, double B)
        {
            double cosA = Math.Cos(A);
            double sinA = Math.Sin(A);               // Precalculating cos and sin for A and B
            double cosB = Math.Cos(B);
            double sinB = Math.Sin(B);

            for (double theta = 0; theta < 2 * Math.PI; theta += thetaAngleSpacing)
            {
                double cosTheta = Math.Cos(theta);  // Precalculating cos and sin for theta (θ)
                double sinTheta = Math.Sin(theta);

                for (double phi = 0; phi < 2 * Math.PI; phi += phiAngleSpacing)
                {
                    double cosPhi = Math.Cos(phi);  // Precalculating cos and sin for phi (Φ)
                    double sinPhi = Math.Sin(phi);

                    double torusX = R2 + R1 * cosTheta; // x
                    double torusY = R1 * sinTheta;

                    double x = torusX * (cosB * cosPhi + sinA * sinB * sinPhi) - torusY * cosA * sinB; // Rotation around 2 axes using rotation matrix
                    double y = torusX * (sinB * cosPhi - sinA * cosB * sinPhi) + torusY * cosA * cosB;
                    double z = K2 + cosA * torusX * sinPhi + torusY * sinA;
                    double zInverse = 1 / z;

                    int xPoint = (int)(screenWidth / 2 + K1 * zInverse * x);    // x' : New 2D x  point location
                    int yPoint = (int)(screenHeight / 2 - K1 * zInverse * y);   // y' : New 2D y  point location

                    if (xPoint >= 0 && xPoint < donutBitMap.Width && yPoint >= 0 && yPoint < donutBitMap.Height)
                    {
                        donutBitMap.SetPixel(xPoint, yPoint, Color.White);      // Adding the new point to the bitmap
                    }
                }
             }
        }
    }
}
