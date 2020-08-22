using System;
using Xunit;

namespace GeneratorsLibrary.Tests
{
    public class StringUtilitiesTest
    {
        [Fact]
        public void CamelCaseToUnderscore_Test1_EmptyName()
        {
            //arrange
            string name = "";
            //act
            string resultName = StringUtilities.CamelCaseToUnderscore(name);
            //assert
            Assert.Equal("", resultName);
        }
        [Fact]
        public void CamelCaseToUnderscore_Test2()
        {
            //arrange
            string name = "length";
            //act
            string resultName = StringUtilities.CamelCaseToUnderscore(name);
            //assert
            Assert.Equal("length", resultName);
        }
        [Fact]
        public void CamelCaseToUnderscore_Test3()
        {
            //arrange
            string name = "Pipe";
            //act
            string resultName = StringUtilities.CamelCaseToUnderscore(name);
            //assert
            Assert.Equal("pipe", resultName);
        }
        [Fact]
        public void CamelCaseToUnderscore_Test4()
        {
            //arrange
            string name = "PipeLength";
            //act
            string resultName = StringUtilities.CamelCaseToUnderscore(name);
            //assert
            Assert.Equal("pipe_length", resultName);
        }
        [Fact]
        public void CamelCaseToUnderscore_Test5()
        {
            //arrange
            string name = "Pipe_Length";
            //act
            string resultName = StringUtilities.CamelCaseToUnderscore(name);
            //assert
            Assert.Equal("pipe_length", resultName);
        }
        [Fact]
        public void CamelCaseToUnderscore_Test6()
        {
            //arrange
            string name = "pipe_length";
            //act
            string resultName = StringUtilities.CamelCaseToUnderscore(name);
            //assert
            Assert.Equal("pipe_length", resultName);
        }
        [Fact]
        public void CamelCaseToUnderscore_Test7()
        {
            //arrange
            string name = "_Pipe";
            //act
            string resultName = StringUtilities.CamelCaseToUnderscore(name);
            //assert
            Assert.Equal("_pipe", resultName);
        }
    }
}
