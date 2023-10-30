using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;

namespace TestWebApi.Modules.UtilitiesTest
{

    public class Nullable<T> where T : struct{

        private object _value;

        public Nullable()
        {
            
        }
        public Nullable(T value)
        {
            _value = value;
        }

        public bool HasValue{
            get {return _value !=  null;}
        }

        public T GetValueOrDefault()
        {
            if(HasValue)
                return (T)_value;
            
            return default(T);
        }
    }
    public class DiscountCalculator<TProduct> where TProduct: Product{
        public float CalculateDiscount(TProduct product)
        {
            return product.Price;
        }
    }

    public class Product{
        public string Title {get;set;}
        public float Price {get;set;}
    }

    public class Book: Product{
        public string Isbn {get;set;}
    }

    //Where T: IComparable
    //Where T: Product
    //Where T: struct
    //Where T: class
    //Where T: new()

    public class Utilities<T> where T: IComparable, new()
    {

        public int Max(int a, int b){
            return a > b? a:b;
        }
        
        public void DoSomenthing(T value){
            var obj = new T();
        }

        public T Max(T a, T b)
        {
            return a.CompareTo(b)>0 ? a: b;
        }
        
    }
}