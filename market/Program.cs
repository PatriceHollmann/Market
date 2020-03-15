using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Market
{
    public class Staff
    {
        public Staff(string code, double price, string name, int quantity)
        {
            Code = code;
            Price = price;
            Name = name;
            Quantity = quantity;
        }
        public string Code { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
    public class Pet : Staff
    {
        public string Type { get; set; }
        public Pet(string code, double price, string name, int quantity, string type) : base(code, price, name, quantity)
        {
            this.Type = type;
        }
    }
    public class Shkaf : Staff
    {
        public string Color { get; set; }
        public Shkaf(string code, double price, string name, int quantity,string color):base(code,price,name,quantity)
        {
            this.Color = color;
        }
    }
    public class Sells<T> where T:Staff
    {
        List<T> Deposition = new List<T>();
        public void Buy(string NeedCode, int NeedQuantity)
        {
                var y = Deposition.SingleOrDefault(x => x.Code.Equals(NeedCode));
                if (y != null)
                {
                    if (y.Quantity < NeedQuantity)
                    {
                        Console.WriteLine("Количество превышает складские запасы");
                    }
                    else
                    {
                        y.Quantity -= NeedQuantity;
                    }
                }  
                else 
                {
                    Console.WriteLine("Указанный код отсутствует в базе");
                }
        }
        public void Add(T staff)
        {
           
            if (Deposition.Any(x => x.Code.Equals(staff.Code))) 
            {
                Console.WriteLine("Данный код уже присутствует в базе");
            }
            else
            {
                Deposition.Add(staff);
            }
        }
        public IEnumerable DeleteStaff(string NeedCode)
        {
            for (int i = Deposition.Count-1; i >=0 ; i--)
            {
                var item = Deposition[i];
                if (item.Code.Equals(NeedCode))
                {
                    Deposition.RemoveAt(i);
                    yield return item;
                }
            }
        }
}
    public class FurnMarket:Sells<Shkaf>
    {
        
    }
    public class PetShop: Sells<Pet>
    {
     
    }
    public static class ArrayUtil
    {
        public static T RemoveItem<T>(this List<T> array, Func<T,bool> func)
        {
            for (int i = 0; i < array.Count; i++)
            {
                var item = array[i];
                if (func(item))
                {
                    array.Remove(item);
                    return item;
                }
            }
            return default(T);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Sells<Shkaf> sls = new Sells<Shkaf>();
            List<FurnMarket> Deposition = new List<FurnMarket>();
            Shkaf shk1 = new Shkaf("QWERTY", 254000, "Петрович", 2, "blue");
            sls.Add(shk1);
        }
    }
}
