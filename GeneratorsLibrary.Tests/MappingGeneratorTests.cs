using System;
using Xunit;

namespace GeneratorsLibrary.Tests
{
    public class MappingGeneratorTests
    {
        [Fact]
        public void GenerateMappings_NotSettedCode_NullResult()
        {
            //arrange
            MappingGenerator generator = new MappingGenerator("Tests", "tests");
            //act
            var result = generator.GenerateMappings();
            //assert
            Assert.Null(result);
        }
        [Fact]
        public void GenerateMappings_EmptyNamespace_NotNullResult()
        {
            //arrange
            string code = @"
                namespace Test
                {
                    
                }";
            MappingGenerator generator = new MappingGenerator("Tests", "tests", code);
            //act
            var result = generator.GenerateMappings();
            //assert
            Assert.NotNull(result);
        }
        [Fact]
        public void GenerateMappings_EmptyNamespace_EmptyResult()
        {
            //arrange
            string code = @"
                namespace Test
                {
                    
                }";
            MappingGenerator generator = new MappingGenerator("Tests", "tests", code);
            //act
            var result = generator.GenerateMappings();
            //assert
            Assert.Empty(result);
        }
        [Fact]
        public void GenerateMappings_ClassWithoutSetters_EmptyResult()
        {
            //arrange
            string code = @"
                namespace Test
                {
                    class TestClass : DomainObject<long>{
                        public int Prop1 {get;}
                        public int Prop1 => 1;
                    }
                }";
            MappingGenerator generator = new MappingGenerator("Tests", "tests", code);
            //act
            var result = generator.GenerateMappings();
            //assert
            Assert.Empty(result);
        }
        [Fact]
        public void GenerateMappings_OneEmptyClass_EmptyResult()
        {
            //arrange
            string code = @"
                namespace Test
                {
                    class TestClass : DomainObject<long>{
                    }
                }";
            MappingGenerator generator = new MappingGenerator("Tests", "tests", code);
            //act
            var result = generator.GenerateMappings();
            //assert
            Assert.Empty(result);
        }
        [Fact]
        public void GenerateMappings_IntNotAutoProperty_HasResult()
        {
            //arrange
            string code = @"
                namespace Test
                {
                    class TestClass : DomainObject<long>{
                        private int _prop1;
                        public int Prop1 
                        {
                            get
                            {
                                return _prop1;
                            }
                            set
                            {
                                _prop1 = value;
                            }
                    }
                }";
            MappingGenerator generator = new MappingGenerator("Tests", "tests", code);
            //act
            var result = generator.GenerateMappings();
            //assert
            Assert.Single(result);
        }
        [Fact]
        public void GenerateMappings_IntAutoProperty_HasResult()
        {
            //arrange
            string code = @"
                namespace Test
                {
                    class TestClass : DomainObject<long>{
                        public int Prop1 {get;set;}
                    }
                }";
            MappingGenerator generator = new MappingGenerator("Tests", "tests", code);
            //act
            var result = generator.GenerateMappings();
            //assert
            Assert.Single(result);
        }
        [Fact]
        public void GenerateMappings_IntAutoProperty_TrueFileName1()
        {
            //arrange
            string code = @"
                namespace Test
                {
                    class TestClass : DomainObject<long>{
                        public int Prop1 {get;set;}
                    }
                }";
            MappingGenerator generator = new MappingGenerator("Tests", "tests", code);
            //act
            var result = generator.GenerateMappings();
            //assert
            Assert.Equal("TestClass.hbm.xml", result[0].Key);
        }
        [Fact]
        public void GenerateMappings_IntAutoProperty_TrueFileName2()
        {
            //arrange
            string code = @"
                namespace Test
                {
                    class Test_0Class : DomainObject<long>{
                        public int Prop1 {get;set;}
                    }
                }";
            MappingGenerator generator = new MappingGenerator("Tests", "tests", code);
            //act
            var result = generator.GenerateMappings();
            //assert
            Assert.Equal("Test_0Class.hbm.xml", result[0].Key);
        }
        [Fact]
        public void GenerateMappings_IntAutoPropertyIgnored_EmptyResult()
        {
            //arrange
            string code = @"
                namespace Test
                {
                    class TestClass : DomainObject<long>{
                        public int Prop1 {get;set;}
                    }
                }";
            MappingGenerator generator = new MappingGenerator("Tests", "tests", code);
            generator.NotMappedPropertyNames.Add("Prop1");
            //act
            var result = generator.GenerateMappings();
            //assert
            Assert.Empty(result);
        }
        [Fact]
        public void GenerateMappings_NotDomainObject_EmptyResult()
        {
            //arrange
            string code = @"
                namespace Test
                {
                    class TestClass{
                        public int Prop1 {get;set;}
                    }
                }";
            MappingGenerator generator = new MappingGenerator("Tests", "tests", code);
            //act
            var result = generator.GenerateMappings();
            //assert
            Assert.Empty(result);
        }
//        [Fact]
//        public void GenerateMappings_IntAutoProperty_TrueMapping()
//        {
//            //arrange
//            string code = @"
//                namespace Test
//                {
//                    class TestClass : DomainObject<long>{
//                        public int Prop1 {get;set;}
//                    }
//                }";
//            MappingGenerator generator = new MappingGenerator("Tests", "tests", code);
//            //act
//            var result = generator.GenerateMappings();
//            //assert
//            Assert.Equal(@"<?xml version=""1.0"" encoding=""utf-8""?>
//<hibernate-mapping xmlns=""urn:nhibernate-mapping-2.2"">
//\t<class lazy=""false"" name =""Tests.TestClass, Tests"" table=""tests_test_class"" >
//        <property column = ""description"" name =""Prop1"" type =""int""/>
//    </class>
//</hibernate-mapping>", result[0].Value);
//        }
    }
}
