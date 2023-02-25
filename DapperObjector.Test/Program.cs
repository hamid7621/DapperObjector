// See https://aka.ms/new-console-template for more information
using Dapper;
using DapperObjector;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");






var testModel = new TestModel()
{
    
    CreateDate= DateTime.Now,
    DeleteDate= null,
    IsDeleted = false,  
    Title ="test3"
    
};


using (var db = new SqlConnection(""))
{
    //var query = db.Insert(testModel);
    var query = db.Update(testModel,"Id=1");
    
}


Console.WriteLine("Bye, World!");
Console.ReadLine();

