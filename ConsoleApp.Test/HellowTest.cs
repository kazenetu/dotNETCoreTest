using System;
using Xunit;

namespace ConsoleApp.Test
{
    /// <summary>
    /// Hellowテスト
    /// </summary>
    public class HellowTest
    {
        /// <summary>
        /// Test対象
        /// </summary>
        private readonly Hellow testTarget;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HellowTest(){
            testTarget = new Hellow("ABC");
        }
        
        [Fact]
        /// <summary>
        /// GetFormatNameテスト
        /// </summary>
        public void GetFormatNameTest()
        {
            var result = testTarget.GetFormatName();
            Assert.Equal(result, "こんにちは ABC!");
        }
    }
}
