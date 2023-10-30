using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestWebApi.Modules.UtilitiesTest
{
    public class UtilitiesTest
    {
        [Fact]
        public void Test1()
        {
            var number = new Nullable<int>();
            System.Console.WriteLine("Has Value ?"+ number.HasValue);
            System.Console.WriteLine("Value: "+ number.GetValueOrDefault());
            Assert.True(true);
        }
    }
}