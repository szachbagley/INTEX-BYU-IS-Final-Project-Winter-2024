using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;

namespace Intex_Group3_6.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        public virtual void AddItem(Product proj, int quantity)
        {
            CartLine? line = Lines
                .Where(x => x.Product.productId == proj.productId)
                .FirstOrDefault(); 

            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = proj,
                    Quantity = quantity 

                });

            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product) => Lines.RemoveAll(x => x.Product.productId == product.productId);



        public virtual void Clear() => Lines.Clear();

        public decimal CalculateTotal() => (decimal)Lines.Sum(x => x.Product.price * x.Quantity);
        
        public class CartLine
        {
            public int CartLineId { get; set; }

            public Product Product { get; set; }

            public int Quantity { get; set; }

            //[Key]
            //public int TransactionId { get; set; }
            //public int ProductId { get; set; }
            //public required int quantity { get; set; }
            //public int? rating { get; set; }
        }
    }
}
