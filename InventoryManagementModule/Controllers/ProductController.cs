
using InventoryManagementModule.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace InventoryManagementModule.Controllers
{
    public class ProductController : Controller
    {
        string cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=InventoryDB;Integrated Security=True";

        // GET: Product - Display all products
        public ActionResult Index()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Products";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            products.Add(new Product
                            {
                                ProductId = Convert.ToInt32(dr["ProductId"]),
                                ProductName = dr["ProductName"].ToString(),
                                ProductQuantity = Convert.ToInt32(dr["ProductQuantity"]),
                                ProductPrice = Convert.ToInt32(dr["ProductPrice"])
                            });
                        }
                    }
                }
            }

            return View(products);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
       
        public ActionResult Create(Product p)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"INSERT INTO Products
    (ProductName, ProductQuantity, ProductPrice)
    VALUES (@ProductName, @ProductQuantity, @ProductPrice)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductName", p.ProductName);
                    cmd.Parameters.AddWithValue("@ProductQuantity", p.ProductQuantity);
                    cmd.Parameters.AddWithValue("@ProductPrice", p.ProductPrice);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }


        // GET: Product/Edit
        public ActionResult Edit(int id)
        {
            Product p = null;

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Products WHERE ProductId = @ProductId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", id);

                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            p = new Product
                            {
                                ProductId = Convert.ToInt32(dr["ProductId"]),
                                ProductName = dr["ProductName"].ToString(),
                                ProductQuantity = Convert.ToInt32(dr["ProductQuantity"]),
                                ProductPrice = Convert.ToInt32(dr["ProductPrice"])
                            };
                        }
                    }
                }
            }

            if (p == null)
                return HttpNotFound();

            return View(p);
        }

        // POST: Product/Edit
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"UPDATE Products SET
                    ProductName = @ProductName,
                    ProductQuantity = @ProductQuantity,
                    ProductPrice = @ProductPrice
                    WHERE ProductId = @ProductId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", p.ProductId);
                    cmd.Parameters.AddWithValue("@ProductName", p.ProductName);
                    cmd.Parameters.AddWithValue("@ProductQuantity", p.ProductQuantity);
                    cmd.Parameters.AddWithValue("@ProductPrice", p.ProductPrice);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        // GET: Product/Delete
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "DELETE FROM Products WHERE ProductId = @ProductId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductId", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
