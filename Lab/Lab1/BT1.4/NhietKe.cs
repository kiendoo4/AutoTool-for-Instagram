using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai_1._4
{
    class NhietKe
    {
        private int NhietDo;
        public NhietKe(int init_nhietdo)
        {
            NhietDo = init_nhietdo;
        }

        public int get()
        {
            return NhietDo;
        }
        public event EventHandler ThayDoiNhietDo;
        public void set(int NhietDo)
        {
            this.NhietDo = NhietDo;

            // Goi su kien thay doi nhiet do
            ThayDoiNhietDo.Invoke(this, EventArgs.Empty);
        }
    }
}
