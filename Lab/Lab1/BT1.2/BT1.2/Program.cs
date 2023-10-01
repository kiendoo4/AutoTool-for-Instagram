using System;

namespace BT1._2
{
    internal class Fraction : IComparable<Fraction>
    {
        public int t, m;
        public int gcd(int a, int b)
        {
            if (a == 0) return 1;
            int c, d;
            if (a > 0) c = a; else c = -a;
            if (b > 0) d = b; else d = -b;
            while(c != d)
            {
                if (c > d) c -= d;
                else d -= c;
            }    
            return c;
        }
        public Fraction() { }
        public Fraction(int t)
        {
            this.t = t; 
            this.m = 1;
        }
        public Fraction(int t, int m)
        {
            while (m == 0)
            {
                throw new ArgumentException("Mau so khong the la 0!");
            }
            this.t = t / gcd(t, m);
            this.m = m / gcd(t, m);
        }
        public Fraction(Fraction a)
        {
            this.t = a.t / gcd(a.t, a.m);
            this.m = a.m / gcd(a.t, a.m);
        }
        public int Up
        {
            get
            {
                return this.t;
            }
            set
            { this.t = value; }
        }
        public int Down
        {
            get
            {
                return this.m;
            }
            set { this.m = value; }
        }
        public static Fraction operator +(Fraction a) => a;
        public static Fraction operator -(Fraction a) => new Fraction(-a.t, a.m);
        public static Fraction operator +(Fraction a, Fraction b) => new Fraction(a.t * b.m + b.t * a.m, a.m * b.m);
        public static Fraction operator -(Fraction a, Fraction b) => a + (-b);
        public static Fraction operator *(Fraction a, Fraction b) => new Fraction(a.t * b.t, a.m * b.m);
        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b.t == 0)
            {
                throw new ArgumentException("Phep chia khong thuc hien duoc!");
            }
            else
                return new Fraction(a.t * b.m, a.m * b.t);
        }
        public static bool operator == (Fraction a, Fraction b)
        {
            return ((a.t == b.t) && (a.m == b.m));
        }
        public static bool operator !=(Fraction a, Fraction b)
        {
            return ((a.t != b.t) || (a.m != b.m));
        }
        public static bool operator >(Fraction a, Fraction b)
        {
            if (a.m * b.m > 0)
                return (a.t * b.m > a.m * b.t);
            else
                return (a.t * b.m < a.m * b.t);
        }
        public static bool operator <(Fraction a, Fraction b)
        {
            if (a > b || a == b) return false;
            return true;
        }
        public void print()
        {
            if (t == 0) { Console.WriteLine(0); }
            else if(m != 1) Console.WriteLine(t + "/" + m);
            else Console.WriteLine(t);
        }
        public int CompareTo(Fraction c)
        {
            if (m * c.m > 0)
                return (t * c.m).CompareTo(m * c.t);
            else
                return (- t * c.m).CompareTo(-m * c.t);
        }
    }
    class Program
    {
        static void Main()
        {
            //Nhập 2 phân số
            Fraction u = new Fraction();
            Fraction v = new Fraction();
            Console.Write("Nhap tu cua phan so thu nhat: ");
            u.Up = Convert.ToInt32(Console.ReadLine());
            Console.Write("Nhap mau cua phan so thu nhat: ");
            u.Down = Convert.ToInt32(Console.ReadLine());
            Console.Write("Nhap tu cua phan so thu hai: ");
            v.Up = Convert.ToInt32(Console.ReadLine());
            Console.Write("Nhap mau cua phan so thu hai: ");
            v.Down = Convert.ToInt32(Console.ReadLine());
            //Tính toán cộng trừ nhân chia 2 phân số
            Console.Write("Cong hai phan so: "); (u + v).print();
            Console.Write("Tru hai phan so: "); (u - v).print();
            Console.Write("Nhan hai phan so: "); (u * v).print();
            Console.Write("Chia hai phan so: "); (u / v).print();
            //So sánh u và v
            if (u == v) Console.WriteLine("2 phan so bang nhau");
            else
            {
                if (u > v) Console.WriteLine("Phan so thu nhat > Phan so thu hai");
                else Console.WriteLine("Phan so thu nhat < Phan so thu hai");
            }
            //Chuyển đổi ngầm định số nguyên thành phân số đã tạo ở constructor Fraction(int t)
            int p;
            Console.Write("Nhap mot so nguyen: "); p = Convert.ToInt32(Console.ReadLine());
            //Chuyển
            Fraction fraction = new Fraction(p);
            Console.WriteLine("Sau khi da chuyen, kieu du lieu moi la: " + fraction.GetType());
            //Implement interface Icomparable, test với phương thức Array.Sort()
            Console.WriteLine("Implement interface Icomparable, voi day phan so: 1/2, -1/3, 1/4, -2, 3/8");
            Fraction[] listofFrac = new Fraction[]
            {
                new Fraction(1, 2),
                new Fraction(-1, 3),
                new Fraction(1, 4),
                new Fraction(-2),
                new Fraction(3, 8)
            };
            Array.Sort(listofFrac);
            Console.Write("After sorting: ");
            foreach(var frac in listofFrac)
            {
                frac.print(); Console.Write(" ");
            }
        }
    }
}