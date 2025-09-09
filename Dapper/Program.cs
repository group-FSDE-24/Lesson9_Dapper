using Dapper;
using Lesson9_Dapper.Models;
using System.Data;
using System.Data.SqlClient;
using Z.Dapper.Plus;

namespace Lesson9_Dapper;


class Program
{
	static async Task Main(string[] args)
	{
		var cs = "Server=(localdb)\\MSSQLLocalDB;Database=Library;Integrated Security=true;";
		using var con = new SqlConnection(cs);
		var sql = string.Empty;


		///////////////////////////////////////////////////////////////////////////////////////////
		// // Execute with Simple

		// sql = "INSERT INTO Categories VALUES(15, 'C#')";
		// sql = "UPDATE Categories SET Name='C++' WHERE Id=15";
		// sql = "DELETE FROM Categories WHERE Id=15";

		// con.Execute(sql);
		// Console.WriteLine("Successfully Execute Command");


		///////////////////////////////////////////////////////////////////////////////////////////
		/// Execute with Parametr


		// // with parameters 1
		// sql = "INSERT INTO Categories VALUES(15, @name)";
		// var name = "C#";
		// con.Execute(sql, new { NAME = name });
		// 
		// Console.WriteLine("Successfully Execute Command with Paramatr");


		// // with parameters 2
		// sql = "INSERT INTO Categories VALUES(@id, @name)";
		// 
		// var id = 16;
		// var name = "C#";
		// 
		// con.Execute(sql, new { id, name });
		// Console.WriteLine("Successfully Execute Command with Paramatr");


		// // with parameters 3 (multiple value insert)
		// sql = "INSERT INTO Categories VALUES(@id, @name)";
		// 
		// 
		// con.Execute(sql, new object[] {
		// 	new{ id = 19, name= "HTML" },
		// 	new{ id = 20, name= "CSS" },
		// 	new{ id = 21, name= "JS" },
		// });
		// 
		// Console.WriteLine("Successfully Execute Command with Paramatres");


		// // with parametres 3 ( mulltiple value update )

		// sql = "Update Categories Set Name='C++' Where Id=@id";
		// 
		// con.Execute(sql, new object[] {
		//  	new{ id = 19 },
		//  	new{ id = 20 },
		//  	new{ id = 21 }
		//  });
		// 
		// Console.WriteLine("Successfully Execute Command with Paramatres");


		// // with parameters (multiple value delete)
		// sql = "DELETE FROM Categories WHERE Id=@id";
		// 
		// 
		// con.Execute(sql, new object[] {
		// 	new{ id = 19 },
		// 	new{ id = 20 },
		// 	new{ id = 21 }
		// });
		// 
		// Console.WriteLine("Successfully Execute Command with Paramatres");


		///////////////////////////////////////////////////////////////////////////////////////////
		//// Stored Procedure


		// // Example One
		// //// usp_UpdateBooks
		// 
		// sql = "usp_UpdateBooks";
		// con.Execute(sql, new { pId = 1, pPages = 100 },
		//     commandType: CommandType.StoredProcedure);
		// 
		// Console.WriteLine("Successfully Completed usp_UpdateBooks Procedure");



		// /////////////////////////////
		// // Example With Output Parametr
		// //// usp_getBooksNumber
		// 
		// 
		// 
		// DynamicParameters parameters = new DynamicParameters();
		// parameters.Add("AuthorId", 1, DbType.Int32);
		// parameters.Add("BookCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
		// 
		// 
		// sql = "usp_getBooksNumber";
		// 
		// 
		// con.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
		// 
		// 
		// var outputValue = parameters.Get<int>("BookCount");
		// Console.WriteLine(outputValue);

		///////////////////////////////////////////////////////////////////////////////////////////
		// Select

		// Execute Reader
		// sql = "SELECT * FROM Categories";
		// var reader = con.ExecuteReader(sql);
		// 
		// var table = new DataTable();
		// table.Load(reader);
		// 
		// Console.WriteLine(table.Rows.Count);


		// Execute Scalar 
		// sql = "SELECT * FROM Categories";
		// var obj = con.ExecuteScalar(sql);
		// var obj = con.ExecuteScalar<int>(sql); // Boxing / UnBoxing 
		// var obj = await con.ExecuteScalarAsync<int>(sql);
		// Console.WriteLine(obj.ToString());

		//////////////////////////////////////////
		/// Query

		// // Query with Dynamic
		// sql = "SELECT * FROM Categories";
		// var collection = con.Query(sql);
		// foreach (var item in collection)
		// {
		//     Console.WriteLine($"{item.Id} - {item.Name}");
		// }

		// // Query with Base type
		//sql = "SELECT Id FROM Categories";
		//var collection = con.Query<int>(sql);
		//foreach (var item in collection)
		//{
		//    Console.WriteLine(item);
		//}

		// // Query with Custom Type
		// sql = "SELECT * FROM Categories";
		// var collection = con.Query<Category>(sql);
		// //var collection = await con.QueryAsync<Category>(sql);
		// foreach (var item in collection)
		// {
		// 	Console.WriteLine($"{item.Id} - {item.Name}");
		// }


		// // QueryFirstOrDefaultAsync
		// sql = "SELECT * FROM Categories WHERE Id > 5";
		// var category = await con.QueryFirstOrDefaultAsync<Category>(sql);
		// Console.WriteLine(category.Id + " - " + category.Name);

		///////////////////////////////////////////////////////////////////////////////////////////
		// Transaction

		//await con.OpenAsync();
		//var tran = await con.BeginTransactionAsync();

		// do something

		//await tran.CommitAsync(); // or tran.RollbackAsync();
		//await con.CloseAsync();

		///////////////////////////////////////////////////////////////////////////////////////////

		// // QueryMultipleAsync

		// sql = @"
		// SELECT * FROM Categories;
		// SELECT * FROM Authors;
		// ";
		// 
		// 
		// using var multi = await con.QueryMultipleAsync(sql);
		// 
		// var category = await multi.ReadFirstAsync<Category>();
		// var authors = await multi.ReadAsync<Author>();
		// 
		// 
		// Console.WriteLine(category.Name);
		// Console.WriteLine(authors.Count());

		///////////////////////////////////////////////////////////////////////////////////////////
		// // Query with Join
		// 
		// sql = "SELECT * FROM Books INNER JOIN Categories ON Id_Category = Categories.Id";
		// 
		// var list = con.Query<Book, Category, JoinResult>(sql, (book, category) =>
		// 	new JoinResult()
		// 	{
		// 		Id = book.Id,
		// 		BookName = book.Name,
		// 		CategoryName = category.Name,
		// 		Pages = book.Pages
		// 	}
		// );
		// 
		// 
		// foreach (var item in list)
		// {
		// 	Console.WriteLine($"{item.Id} - {item.BookName} - {item.Pages} - {item.CategoryName}");
		// }

		///////////////////////////////////////////////////////////////////////////////////////////
		// Bulk Insert -> Z.Dapper.Plus

		// Entity ile hara yukleyecek datalari onu gostermeliyem
		DapperPlusManager.Entity<Category>().Table("Categories");
		DapperPlusManager.Entity<Author>().Table("Authors");


		var categories = new List<Category>()
		{
			new(){ Id = 19, Name = "CSS" },
			new(){ Id = 20, Name = "HTML" },
			new(){ Id = 21, Name = "JS" },
		};

		con.BulkInsert(categories);

		var authors = new List<Author>()
		{
			new(){ Id = 21, FirstName = "Burhan", LastName = "Orucov" },
			new(){ Id = 22, FirstName = "Aydin", LastName = "Akberov" },
			new(){ Id = 23, FirstName = "Kamran", LastName = "Karimzada" },
		};


		con.BulkInsert(authors);
	}
}