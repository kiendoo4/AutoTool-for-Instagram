using System;

namespace Bai_1._4
{
    class Program
    {
        static void Main(string[] args)
        {
            NhietKe MyNhietKe = new NhietKe(37);
            ManHinh MyManHinh = new ManHinh(MyNhietKe);
            for (int i = 0; i < 10; i++)
            {
                Random NewNhietDo = new Random();
                MyNhietKe.set(NewNhietDo.Next(35, 40));
            }
        }
    }
}
