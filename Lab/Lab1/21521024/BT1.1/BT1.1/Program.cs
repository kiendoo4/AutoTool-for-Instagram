using System.Security.Cryptography.X509Certificates;

namespace BT1._1
{
    public abstract class Shape
    {
        private string name;
        public Shape(string name)
        {
            Name = name;
        }
        public string Name
        {
            get
            {
                return name; //getter
            }
            set
            {
                name = value; //setter
            }
        }
        public abstract double Area
        {
            get;
        }
    }
    public class Circle : Shape
    {
        private double r;
        public Circle(double r, string Name) : base(Name)
        {
            this.r = r;
            this.Name = "Circle";
        }
        public override double Area
        {
            get
            {
                return r * r * Math.PI;
            }
        }
    }
    public class Rectangle : Shape
    {
        private double x, y;
        public Rectangle(double x, double y, string Name) : base (Name)
        {
            this.x = x;
            this.y = y;
            this.Name = "Rectangle";
        }
        public override double Area
        {
            get { return x * y;}
        }
    }
    public class Square : Rectangle
    {
        private double x;
        public Square(double x, string Name) : base(x, x, Name)
        {
            this.x = x;
            this.Name = "Square";
        }
        public override double Area
        {
            get { return x * x; }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Quy ước: Hình 1 là hình tròn, hình 2 là hình chữ nhật
             * Hình 3 là hình vuông */
            int n;
            Console.WriteLine("Nhap n hinh can truy van: ");
            n = Convert.ToInt32(Console.ReadLine());
            Shape[] shapes = new Shape[n + 1];
            for(int i = 1; i <= n; i++)
            {
                Random rnd = new Random();
                int ci = rnd.Next(1, 4);
                if(ci == 1)
                {
                    Console.WriteLine("Hinh thu " + i + " duoc chon la hinh tron, hay nhap ban kinh: ");
                    double r = Convert.ToDouble(Console.ReadLine());
                    shapes[i] = new Circle(r, "Circle");
                }
                else if(ci == 2)
                {
                    double x, y;
                    Console.WriteLine("Hinh thu " + i + " duoc chon la hinh chu nhat");
                    Console.WriteLine("Chieu dai: "); x = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Chieu rong: "); y = Convert.ToDouble(Console.ReadLine());
                    shapes[i] = new Rectangle(x, y, "Rectangle");
                }
                else
                {
                    double x;
                    Console.WriteLine("Hinh thu " + i + " duoc chon la hinh vuong");
                    Console.WriteLine("Canh: "); x = Convert.ToDouble(Console.ReadLine());
                    shapes[i] = new Square(x, "Square");
                }
            }
            for(int i = 1; i <= n; i++)
            {
                Console.WriteLine("Hinh dang cua hinh la: " + shapes[i].Name);
                Console.WriteLine("Dien tich cua hinh thu " + i + " la: " + shapes[i].Area);
            }
        }
    }
}