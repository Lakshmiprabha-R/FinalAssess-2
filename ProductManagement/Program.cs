using Product;
using System;
using System.Data;
using System.Data.SqlClient;


namespace Product
{
    internal class Product

    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Data Source=US-FGRQ8S3;Initial Catalog=productManagement;User Id =sa;password=Lakshmiprabha@2001");
            Product product = new Product();
            string produ = "";
            do
            {
                Console.WriteLine(" ProductManagement");
                Console.WriteLine("1.AddNewProduct");
                Console.WriteLine("2.Get Product");
                Console.WriteLine("3.GetAllProducts");
                Console.WriteLine("4.UpdateProduct");
                Console.WriteLine("5.DeleteProduct");
                Console.WriteLine("Enter your choice");
                int choice = Convert.ToInt32(Console.ReadLine());



                switch (choice)
                {
                    case 1:
                        {
                            product.AddNewProduct(con);
                            break;
                        }
                    case 2:
                        {
                            product.GetProduct(con);
                            break;
                        }
                    case 3:
                        {
                            product.GetAllProducts(con);
                            break;
                        }
                    case 4:
                        {
                            product.UpdateProduct(con);
                            break;
                        }
                    case 5:
                        {
                            product.DeleteProduct(con);
                            break;
                        }
                }
                Console.WriteLine("Do u want to continue[y/n]");
                produ = Console.ReadLine();
            } while (produ.ToLower() == "y");

        }

        public void AddNewProduct(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select * from productmanage", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "producttable");
            var table = ds.Tables["producttable"].NewRow();

            Console.WriteLine("Enter ProductName: ");
            string productname = Console.ReadLine();


            Console.WriteLine("Enter ProductDescription: ");
            table["productdescription"] = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Enter ProductQuantity: ");
            table["productquantity"] = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Enter Productprice: ");
            table["productprice"] = Convert.ToInt32(Console.ReadLine());

            ds.Tables["producttable"].Rows.Add(table);

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds, "producttable");
            Console.WriteLine("Products created successfully");

        }
        public void GetProduct(SqlConnection con)
        {
            Console.WriteLine("Enter  productid: ");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"select * from productmanage  where productid = {id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "producttable");
            for (int i = 0; i < ds.Tables["producttable"].Rows.Count; i++)
            {
                Console.WriteLine("productid--title--description--productquantity--productprice");
                for (int j = 0; j < ds.Tables["producttable"].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables["producttable"].Rows[i][j]} -- ");
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Total Notes are: {ds.Tables["producttable"].Rows.Count}");
        }

        public void GetAllProducts(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("select * from productmanage", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "producttable");
            for (int i = 0; i < ds.Tables["producttable"].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables["producttable"].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables["producttable"].Rows[i][j]} --");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine($"Total Products are: {ds.Tables["producttable"].Rows.Count}");
        }

        public void UpdateProduct(SqlConnection con)
        {
            Console.WriteLine("Enter the productid: ");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"select * from productmanage where productid ={id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);


            Console.WriteLine("Enter the productname to update: ");
            string productname = Console.ReadLine();

            Console.WriteLine("Enter the productdescription to update: ");
            string productdescription = Console.ReadLine();

            Console.WriteLine("Enter the product Quatity to update:");
            int quantity = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the price:");
            int price = Convert.ToInt32(Console.ReadLine());
            var row = ds.Tables[0].Rows[0];

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Products updated successfully");

        }

        public void DeleteProduct(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter($"select * from productmanage", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            Console.WriteLine("Enter the row want to delete:");
            int row = Convert.ToInt32(Console.ReadLine());

            ds.Tables[0].Rows[row].Delete();

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("ProductsTable deleted successfully");
        }
    }
}