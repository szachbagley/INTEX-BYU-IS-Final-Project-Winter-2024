using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore.Storage;

namespace Intex_Group3_6.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        public void AddItem(Product proj, int quantity)
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

        public void RemoveLine(Product proj) => Lines.RemoveAll(x => x.Product.productId == proj.productId);

        public void Clear() => Lines.Clear();

        public decimal CalculateTotal() => (decimal)Lines.Sum(x => x.Product.price * x.Quantity);
        
        public class CartLine
        {
            public int CartLineId { get; set; }

            public Product Product { get; set; }

            public float Quantity { get; set; }
        }
    }
}
