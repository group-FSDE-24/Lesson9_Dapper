namespace Lesson9_Dapper.Models;

public class Book
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public int Pages { get; set; }
	public int YearPress { get; set; }
	public int Id_Themes { get; set; }
	public int Id_Category { get; set; }
	public int Id_Author { get; set; }
	public int Id_Press { get; set; }
	public string? Comment { get; set; }
	public int Quantity { get; set; }
}
