using eCommerce.Entities;
using eCommerce.Entities.CustomEntities;
using System;
using System.Collections.Generic;

namespace eCommerce.Shared.Helpers
{
    public static class SessionHelper
    {
        private const string CART = "CART";
        private const string CART_ITEMS = "CART_ITEMS";
        private const string PROMO = "PROMO";
        private const string PROMO_CODE = "PROMO_CODE";
        private const string DARK_MODE = "DARK_MODE";

        public static Cart Cart
        {
            get => SessionManager.Get<Cart>(CART) ?? new Cart();
            set => SessionManager.Set(CART, value);
        }

        public static List<CartItem> CartItems
        {
            get => SessionManager.Get<List<CartItem>>(CART_ITEMS) ?? new List<CartItem>();
            set => SessionManager.Set(CART_ITEMS, value);
        }

        public static Promo Promo
        {
            get => SessionManager.Get<Promo>(PROMO);
            set => SessionManager.Set(PROMO, value);
        }

        public static string PromoCode
        {
            get => SessionManager.Get<string>(PROMO_CODE) ?? string.Empty;
            set => SessionManager.Set(PROMO_CODE, value);
        }

        public static void ClearCart()
        {
            SessionManager.ClearKey(CART);
            SessionManager.ClearKey(CART_ITEMS);
            SessionManager.ClearKey(PROMO_CODE);
            SessionManager.ClearKey(PROMO);
        }

        public static string DarkMode
        {
            get => SessionManager.Get<string>(DARK_MODE) ?? "false";
            set => SessionManager.Set(DARK_MODE, value);
        }
    }
}
