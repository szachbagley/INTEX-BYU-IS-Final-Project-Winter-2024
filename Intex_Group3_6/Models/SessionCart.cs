using System.Text.Json.Serialization;
using Intex_Group3_6.Infrastructure;

namespace Intex_Group3_6.Models
{
    // SessionCart extends the functionality of a generic Cart class.
    public class SessionCart : Cart
    {
        // Static method to obtain an instance of SessionCart from session state or create a new one.
        public static Cart GetCart(IServiceProvider services)
        {
            // Access the HTTP session from the HttpContext via the IHttpContextAccessor provided by DI.
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()
                .HttpContext?.Session;

            // Retrieve the cart from session storage using a custom extension method 'GetJson' or create a new SessionCart if none exists.
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();

            // Set the Session property of the cart for further operations.
            cart.Session = session;

            return cart;
        }

        // Decorates the Session property to be ignored by JSON serialization processes to prevent circular references or other serialization issues.
        [JsonIgnore]
        public ISession? Session { get; set; }

        // Overrides the AddItem method from the base Cart class.
        public override void AddItem(Product proj, int quantity)
        {
            // Calls the AddItem on the base class to handle the addition logic.
            base.AddItem(proj, quantity);
            // Updates the session state with the new state of the cart after adding an item.
            Session?.SetJson("Cart", this);
        }

        // Overrides the RemoveLine method from the base Cart class.
        public override void RemoveLine(Product product)
        {
            // Calls the RemoveLine on the base class to handle the removal logic.
            base.RemoveLine(product);
            // Updates the session state with the new state of the cart after removing an item.
            Session?.SetJson("Cart", this);
        }

        // Overrides the Clear method from the base Cart class to remove all items.
        public override void Clear()
        {
            // Calls the Clear on the base class to handle the clearing logic.
            base.Clear();
            // Removes the 'Cart' item from the session state.
            Session?.Remove("Cart");
        }
    }
}