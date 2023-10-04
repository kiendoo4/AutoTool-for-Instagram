using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai_1._4
{
    class ManHinh
    {
        private NhietKe MyNhietKe;

        public ManHinh(NhietKe init_nhietke)
        {
            this.MyNhietKe = init_nhietke;

            // Đăng ký lắng nghe sự kiện thay đổi nhiệt độ
            MyNhietKe.ThayDoiNhietDo += ThayDoiNhietDo;
        }

        private void ThayDoiNhietDo(object sender, EventArgs e)
        {
            // Hiển thị nhiệt độ mới
            Console.WriteLine("Nhiet do hien tai: {0}", MyNhietKe.get());
        }
    }
}
