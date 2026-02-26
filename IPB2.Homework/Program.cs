using Microsoft.Data.SqlClient;
using System.ComponentModel.Design;
using System.Data;

SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
connectionString.DataSource = ".";
connectionString.InitialCatalog = "IPB2";
connectionString.UserID = "sa";
connectionString.Password = "sasa@123";
connectionString.TrustServerCertificate = true;

SqlConnection connection = new SqlConnection(connectionString.ConnectionString);
connection.Open();
SqlCommand command = null;
while (true)
{

startMain:
    Console.WriteLine("\n*** Homework ***");
    Console.WriteLine("1) Student (CRUD)");
    Console.WriteLine("2) Batch (CRUD)");
    Console.WriteLine("3) Exit");

    Console.Write("Please choose: ");
    var choose = Console.ReadLine();

    bool isChoose = int.TryParse(choose, out int option);
    if (option == 1) StudentCRUD();
    else if (option == 2) BatchCRUD();
    else if (option == 3) Environment.Exit(0);
    else { Console.WriteLine("Invalid option.Please try again."); goto startMain; }



}
void StudentCRUD()
{
    bool isContinue = true;
    while (isContinue)
    {
    start:
        Console.WriteLine("\n*** Student (CRUD) Exercise ***");
        Console.WriteLine("1) Create student");
        Console.WriteLine("2) Get student");
        Console.WriteLine("3) Update student");
        Console.WriteLine("4) Delete student");
        Console.WriteLine("5) Exit");
        Console.Write("Please choose: ");
        var choose = Console.ReadLine();
        bool isChoose = int.TryParse(choose, out int option);
        if (option == 1) CreateStudent();
        else if (option == 2) GetStudent();
        else if (option == 3) UpdateStudent();
        else if (option == 4) DeleteStudent();
        else if (option == 5) isContinue = false;
        else { Console.WriteLine("Invalid option.Please try again."); goto start; }
    }
}
void BatchCRUD()
{

    bool isContinue = true;
    while (isContinue)
    {
    start:
        Console.WriteLine("\n*** Batch (CRUD) Exercise ***");
        Console.WriteLine("1) Create batch");
        Console.WriteLine("2) Get batch");
        Console.WriteLine("3) Update batch");
        Console.WriteLine("4) Delete batch");
        Console.WriteLine("5) Exit");
        Console.Write("Please choose: ");
        var choose = Console.ReadLine();
        bool isChoose = int.TryParse(choose, out int option);
        if (option == 1) CreateBatch();
        else if (option == 2) GetBatch();
        else if (option == 3) UpdateBatch();
        else if (option == 4) DeleteBatch();
        else if (option == 5) isContinue = false;
        else { Console.WriteLine("Invalid option.Please try again."); goto start; }
    }
}
// Student (CRUD)
void GetStudent()
{
    string sql = @"SELECT [StudentId]
      ,[StudentName]
      ,[ClassNo]
      ,[Age]
      ,[Address]
      ,[ParentName]
        FROM [dbo].[Tbl_Student] where isDelete = 0";
    command = new SqlCommand(sql, connection);
    SqlDataAdapter adapter = new SqlDataAdapter(command);
    DataTable dt = new DataTable();
    adapter.Fill(dt);

    //Console.WriteLine("StudentID\t StudentName\t ClassNo\t Age\t Address\t ParentName");
    Console.WriteLine("\n*** Get Student ***");
    foreach (DataRow dr in dt.Rows)
    {
        Console.Write(dr["StudentId"].ToString() + "\t");
        Console.Write(dr["StudentName"].ToString() + "\t");
        Console.Write(dr["ClassNo"].ToString() + "\t");
        Console.Write(dr["Age"].ToString() + "\t");
        Console.Write(dr["Address"].ToString() + "\t");
        Console.WriteLine(dr["ParentName"].ToString());

    }

}
void CreateStudent()
{

    Console.WriteLine("\n*** Create Student ***");
    Console.Write("Enter name: ");
    string name = Console.ReadLine()!;

    Console.Write("Enter Class: ");
    string classno = Console.ReadLine()!;

ageAgain:
    Console.Write("Enter age: ");
    string age = Console.ReadLine()!;
    if (!int.TryParse(age, out int result))
    {
        Console.WriteLine("Invalid input.Please enter again.");
        goto ageAgain;
    }

    Console.Write("Enter address: ");
    string address = Console.ReadLine()!;

    Console.Write("Enter parent name: ");
    string parentName = Console.ReadLine()!;

    string sql = $@"INSERT INTO [dbo].[Tbl_Student]
           ([StudentId]
            ,[StudentName]
           ,[ClassNo]
           ,[Age]
           ,[Address]
           ,[ParentName]
           ,[isDelete])
     VALUES
           ('{Guid.NewGuid().ToString()}'
           ,'{name}'
           ,'{classno}'
           ,'{age}'
           ,'{address}'
           ,'{parentName}'
           ,0)";
    command = new SqlCommand(sql, connection);
    int rowAffected = command.ExecuteNonQuery();
    Console.WriteLine(rowAffected > 0 ? "Created student successfully." : "Failed to create student.");
}
void UpdateStudent()
{
    Console.WriteLine("\n*** Update Student ***");
    Console.Write("Enter student id: ");
    string stuId = Console.ReadLine()!;

    Console.Write("Enter name: ");
    string name = Console.ReadLine()!;

    Console.Write("Enter Class: ");
    string classno = Console.ReadLine()!;

    Console.Write("Enter age: ");
    string age = Console.ReadLine()!;

    Console.Write("Enter address: ");
    string address = Console.ReadLine()!;

    Console.Write("Enter parent name: ");
    string parentName = Console.ReadLine()!;

    string sql = $@"UPDATE [dbo].[Tbl_Student]
                   SET 
                      [StudentName] = '{name}'
                      ,[ClassNo] = '{classno}'
                      ,[Age] = '{age}'
                      ,[Address] = '{address}'
                      ,[ParentName] = '{parentName}'
                 WHERE studentId = '{stuId}'";
    command = new SqlCommand(sql, connection);
    int rowAffected = command.ExecuteNonQuery();
    Console.WriteLine(rowAffected > 0 ? "Updated student successfully." : "Failed to update student.");
}
void DeleteStudent()
{
    Console.WriteLine("\n*** Delete Student ***");
    Console.Write("Enter student id: ");
    string stuId = Console.ReadLine()!;

    string sql = $@"UPDATE [dbo].[Tbl_Student]
                   SET [isDelete] = 1 WHERE studentId = '{stuId}'";
    command = new SqlCommand(sql, connection);
    int rowAffected = command.ExecuteNonQuery();
    Console.WriteLine(rowAffected > 0 ? "Deleted student successfully." : "Failed to delete student.");

}

// Batch (CRUD)
void GetBatch()
{
    string sql = @"SELECT [BatchId]
      ,[BatchName]
      ,[Duration]
      ,[FromDate]
      ,[ToDate]
      ,[InstructorName]
      ,[isDelete]
        FROM [dbo].[Tbl_Batch] where isDelete = 0";
    command = new SqlCommand(sql, connection);
    SqlDataAdapter adapter = new SqlDataAdapter(command);
    DataTable dt = new DataTable();
    adapter.Fill(dt);

    //Console.WriteLine("StudentID\t StudentName\t ClassNo\t Age\t Address\t ParentName");
    Console.WriteLine("\n*** Get Batch ***");
    foreach (DataRow dr in dt.Rows)
    {
        Console.Write(dr["BatchId"].ToString() + "\t");
        Console.Write(dr["BatchName"].ToString() + "\t");
        Console.Write(dr["Duration"].ToString() + "\t");
        Console.Write(dr["FromDate"].ToString() + "\t");
        Console.Write(dr["ToDate"].ToString() + "\t");
        Console.WriteLine(dr["InstructorName"].ToString());

    }

}
void CreateBatch()
{

    Console.WriteLine("\n*** Create Batch ***");
    Console.Write("Enter batch name: ");
    string name = Console.ReadLine()!;

duratinAgain:
    Console.Write("Enter duration: ");
    string duration = Console.ReadLine()!;

    if (!int.TryParse(duration, out int result))
    {
        Console.WriteLine("Invalid input.Please enter again.");
        goto duratinAgain;
    }

    Console.Write("Enter from date: ");
    string fromDate = Console.ReadLine()!;

    Console.Write("Enter to date: ");
    string toDate = Console.ReadLine()!;

    Console.Write("Enter instructor name: ");
    string instructorName = Console.ReadLine()!;

    string sql = $@"INSERT INTO [dbo].[Tbl_Batch]
           ([BatchId]
           ,[BatchName]
           ,[Duration]
           ,[FromDate]
           ,[ToDate]
           ,[InstructorName]
           ,[isDelete])
     VALUES
           ('{Guid.NewGuid().ToString()}'
           ,'{name}'
           ,'{duration}'
           ,'{fromDate}'
           ,'{toDate}'
           ,'{instructorName}'
           ,0)";
    command = new SqlCommand(sql, connection);
    int rowAffected = command.ExecuteNonQuery();
    Console.WriteLine(rowAffected > 0 ? "Created batch successfully." : "Failed to create batch.");
}
void UpdateBatch()
{
    Console.WriteLine("\n*** Create Batch ***");

    Console.Write("Enter batch id: ");
    string batchId = Console.ReadLine()!;

    Console.Write("Enter batch name: ");
    string name = Console.ReadLine()!;

    Console.Write("Enter duration: ");
    string duration = Console.ReadLine()!;

    Console.Write("Enter from date: ");
    string fromDate = Console.ReadLine()!;

    Console.Write("Enter to date: ");
    string toDate = Console.ReadLine()!;

    Console.Write("Enter instructor name: ");
    string instructorName = Console.ReadLine()!;

    string sql = $@"UPDATE [dbo].[Tbl_Batch]
                   SET [BatchName] = '{name}'
                      ,[Duration] = '{duration}'
                      ,[FromDate] = '{fromDate}'
                      ,[ToDate] = '{toDate}'
                      ,[InstructorName] = '{instructorName}'
                      ,[isDelete] = 0
                 WHERE BatchId = '{batchId}'";
    command = new SqlCommand(sql, connection);
    int rowAffected = command.ExecuteNonQuery();
    Console.WriteLine(rowAffected > 0 ? "Updated batch successfully." : "Failed to update batch.");
}
void DeleteBatch()
{
    Console.WriteLine("\n*** Delete Batch ***");
    Console.Write("Enter batch id: ");
    string batchId = Console.ReadLine()!;

    string sql = $@"UPDATE [dbo].[Tbl_Batch]
                   SET [isDelete] = 1 WHERE batchId = '{batchId}'";
    command = new SqlCommand(sql, connection);
    int rowAffected = command.ExecuteNonQuery();
    Console.WriteLine(rowAffected > 0 ? "Deleted batch successfully." : "Failed to delete batch.");

}

connection.Close();