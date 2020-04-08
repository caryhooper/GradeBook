using System;
using Xunit;
using GradeBook;

namespace GradeBook.Tests
{

    public delegate string WriteLogDelegate(string logMessage); //describes any method that outputs a string and uses a string.

    public class TypeTests
    {
        int count = 0;
        
        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage; // initialize delegate
            log += ReturnMessage;
            log += IncrementCount; // point delegate to method.  We could possibly both write to console and file
            
            var result = log("Hello!");
            Assert.Equal("Hello!",result);
            Assert.Equal(3, count);
        }

        string ReturnMessage(string message)
        {
            count += 1;
            return message;
        }
        string IncrementCount(string message)
        {
            count += 1;
            return message;
        }

        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            string name = "Cary";
            var upper = MakeUppercase(name);

            Assert.Equal("Cary", name);
            Assert.Equal("CARY", upper);
        }
       public string MakeUppercase(string parameter)
        {
            return parameter.ToUpper(); //returns a copy of the string
            
        }

        [Fact]
        public void NumberEqualsInteger()
        {
            var x = GetInt();
            SetInt(ref x);
            Assert.Equal(42, x);
        }
        private void SetInt(ref int z)
        {
            z = 42;
        }

        private int GetInt()
        {
            return 3;
        }
        
        [Fact]
        public void CSharpCanPassByRef()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(ref book1, "New Name"); //places copy of book1 and creates new book object  // using out instead of ref, you're forced to initialize the out param.

            Assert.Equal("New Name", book1.Name);
        }

        private void GetBookSetName(ref InMemoryBook book, string name) //passed by value not reference
        {
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            Assert.Equal("New Name",book1.Name);
        }

        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        [Fact] //VS finds method with [Fact] next to it, then it runs these pass/fail test methods.
        public void GetBookReturnsDifferentObjects()
        {
            //break up unit test into:
            //arrange
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }

        [Fact] 
        public void TwoVariablesCanReferenceSameObject()
        {
            //break up unit test into:
            //arrange
            var book1 = GetBook("Book 1");
            var book2 = book1;

            Assert.Same(book1,book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
            //act

            
            //assert

        
    }
}
