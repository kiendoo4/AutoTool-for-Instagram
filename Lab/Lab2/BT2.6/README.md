# Lưu ý trước khi chạy BT2.6

1. Nên chạy file .sql để có csdl cho việc chạy file.
2. Đổi thông tin Data Source trong file `Database.cs` cho phù hợp với server của máy.  
`private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=QLSV;Integrated Security=True";`
